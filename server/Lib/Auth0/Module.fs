module Lib.Auth0

open Jose
open Serializers
open System.Net.Http
open System.Text
open System.Text.Json.Serialization
open System.Collections.Generic

[<CLIMutable>]
type Auth0JwtHeader =
  { [<JsonPropertyName("typ")>]
    Type : string
    [<JsonPropertyName("cty")>]
    ContentType : string
    [<JsonPropertyName("alg")>]
    Algorithm : string
    [<JsonPropertyName("kid")>]
    KeyId : string
    [<JsonPropertyName("x5u")>]
    X5u : string
    [<JsonPropertyName("x5c")>]
    X5c : string
    [<JsonPropertyName("x5t")>]
    X5t : string }

[<CLIMutable>]
type Auth0AccessTokenResponse =
  { [<JsonPropertyName("access_token")>]
    AccessToken : string
    [<JsonPropertyName("token_type")>]
    TokenType : string }

type Auth0Client =
  | Machine
  | User

  static member FromClientId (clientId : string) : option<Auth0Client> =
    match clientId with
    | "NWnLqtU89JR6drCGaabYygA2zUUQ7woG" -> Some Machine
    | "d0nbGyYvhTxPjyL1eaa3K4ojLDUNt1LX" -> Some User
    | _ -> None

[<CLIMutable>]
type Auth0ClientParams =
  { [<JsonPropertyName("grant_type")>]
    GrantType : string
    [<JsonPropertyName("client_id")>]
    ClientId : string
    [<JsonPropertyName("client_secret")>]
    ClientSecret : string
    [<JsonPropertyName("audience")>]
    Audience : string }

    static member FromEduClientCredentials (clientId : string) (clientSecret : string) : Auth0ClientParams =
      { GrantType = "client_credentials"
        ClientId = clientId
        ClientSecret = clientSecret
        Audience = "https://services.edu.exokomodo.com" }

[<CLIMutable>]
type Auth0UserInfo =
  { [<JsonPropertyName("email")>]
    Email : string
    [<JsonPropertyName("email_verified")>]
    IsEmailVerified : bool
    [<JsonPropertyName("nickname")>]
    Nickname : string
    [<JsonPropertyName("name")>]
    Name : string
    [<JsonPropertyName("picture")>]
    Picture : string
    [<JsonPropertyName("sub")>]
    Sub : string
    [<JsonPropertyName("updated_at")>]
    UpdatedAt : string }

let getMachineToMachineAccessTokenAsync(unauthenticatedAuth0HttpClient : HttpClient) (serializer : JsonSerializer) (clientParams : Auth0ClientParams) : Async<option<Auth0AccessTokenResponse>> =
  let jsonContent = new StringContent(
    serializer.Serialize(
      clientParams,
      Encoding.UTF8,
      "application/json"
    )
  )
  async {
    try
      let! response =
        unauthenticatedAuth0HttpClient.PostAsync(
          "oauth/token",
          jsonContent
        ) |> Async.AwaitTask
      if response.IsSuccessStatusCode then
        return serializer.Deserialize<Auth0AccessTokenResponse>(
          response.Content.ReadAsStringAsync()
            |> Async.AwaitTask
            |> Async.RunSynchronously
        ) |> Some
      else
        printfn "HTTP request for access token failed with status code %s" (response.StatusCode.ToString())
        return None
    with
      | :? HttpRequestException as ex ->
        printfn "Failed HTTP request for access token: %s" ex.Message
        return None
  }

let getUserInfoAsync (client : HttpClient) (serializer : JsonSerializer) : Async<option<Auth0UserInfo>> =
  async {
    let! response = client.GetAsync("/userinfo") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    try
      return serializer.Deserialize<Auth0UserInfo> content |> Some
    with
      | :? Json.JsonException as ex ->
        printfn "%s" ex.Message
        return None
  }

let getWellKnownKeysAsync (client : HttpClient) : Async<JwkSet> =
  async {
    let! response = client.GetAsync("/.well-known/jwks.json") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    return JwkSet.FromJson(content, JWT.DefaultSettings.JsonMapper)
  }

let validateAccessTokenWithKeySet (keySet : JwkSet) (bearer : string) : option<Auth0Client> =
  let header = Jose.JWT.Headers<Auth0JwtHeader>(bearer, JWT.DefaultSettings)
  let kid = header.KeyId
  let key =
    List.find
      (fun (x : Jwk) -> x.KeyId = kid)
      (Seq.toList keySet.Keys)
  let decoded = Jose.JWT.Decode<IDictionary<string, obj>> (bearer, key)
  // NOTE: At this point, the JWT is valid.
  Auth0Client.FromClientId (decoded["azp"] :?> string)

let validateAccessToken (auth0HttpClient : HttpClient) (bearer : string) : option<Auth0Client> =
  validateAccessTokenWithKeySet
    (getWellKnownKeysAsync auth0HttpClient |> Async.RunSynchronously)
    bearer