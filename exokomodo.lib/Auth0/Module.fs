module ExoKomodo.Lib.Auth0

open Jose
open Serializers
open System.Net.Http
open System.Text
open System.Collections.Generic

[<CLIMutable>]
type Auth0JwtHeader =
  { [<Newtonsoft.Json.JsonProperty("typ")>]
    Type : string
    [<Newtonsoft.Json.JsonProperty("cty")>]
    ContentType : string
    [<Newtonsoft.Json.JsonProperty("alg")>]
    Algorithm : string
    [<Newtonsoft.Json.JsonProperty("kid")>]
    KeyId : string
    [<Newtonsoft.Json.JsonProperty("x5u")>]
    X5u : string
    [<Newtonsoft.Json.JsonProperty("x5c")>]
    X5c : string
    [<Newtonsoft.Json.JsonProperty("x5t")>]
    X5t : string }

[<CLIMutable>]
type Auth0AccessTokenResponse =
  { [<Newtonsoft.Json.JsonProperty("access_token")>]
    AccessToken : string
    [<Newtonsoft.Json.JsonProperty("token_type")>]
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
  { [<Newtonsoft.Json.JsonProperty("grant_type")>]
    GrantType : string
    [<Newtonsoft.Json.JsonProperty("client_id")>]
    ClientId : string
    [<Newtonsoft.Json.JsonProperty("client_secret")>]
    ClientSecret : string
    [<Newtonsoft.Json.JsonProperty("audience")>]
    Audience : string }

    static member FromEduClientCredentials (clientId : string) (clientSecret : string) : Auth0ClientParams =
      { GrantType = "client_credentials"
        ClientId = clientId
        ClientSecret = clientSecret
        Audience = "https://services.edu.exokomodo.com" }

[<CLIMutable>]
type Auth0UserInfo =
  { [<Newtonsoft.Json.JsonProperty("email")>]
    Email : option<string>
    [<Newtonsoft.Json.JsonProperty("email_verified")>]
    IsEmailVerified : option<bool>
    [<Newtonsoft.Json.JsonProperty("nickname")>]
    Nickname : string
    [<Newtonsoft.Json.JsonProperty("name")>]
    Name : string
    [<Newtonsoft.Json.JsonProperty("picture")>]
    Picture : string
    [<Newtonsoft.Json.JsonProperty("sub")>]
    Sub : string
    [<Newtonsoft.Json.JsonProperty("updated_at")>]
    UpdatedAt : string }

let getMachineToMachineAccessTokenAsync(unauthenticatedAuth0HttpClient : HttpClient) (serializer : JsonSerializer) (clientParams : Auth0ClientParams) : Async<option<Auth0AccessTokenResponse>> =
  let jsonContent = new StringContent(
    serializer.Serialize(clientParams),
    Encoding.UTF8,
    "application/json"
  )
  async {
    try
      let! response =
        unauthenticatedAuth0HttpClient.PostAsync(
          "oauth/token",
          jsonContent
        ) |> Async.AwaitTask
      let content = 
        response.Content.ReadAsStringAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously
      if response.IsSuccessStatusCode then
        return serializer.Deserialize<Auth0AccessTokenResponse>(content) |> Some
      else
        printfn
          "HTTP request for access token failed with status code %s: %s"
          (response.StatusCode.ToString())
          content
        return None
    with
      | :? HttpRequestException as ex ->
        printfn "Failed HTTP request for access token: %s" ex.Message
        return None
  }

let getUserInfoAsync (client : HttpClient) (serializer : Serializers.JsonSerializer) : Async<option<Auth0UserInfo>> =
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

let private jsonMapperSettings = JWT.DefaultSettings.RegisterMapper(Serializers.JsonSerializer())

let getWellKnownKeysAsync (client : HttpClient) : Async<JwkSet> =
  async {
    let! response = client.GetAsync("/.well-known/jwks.json") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    return JwkSet.FromJson(content, JsonSerializer())
  }

let validateAccessTokenWithKeySet (keySet : JwkSet) (bearer : string) : option<Auth0Client> =
  let header = Jose.JWT.Headers<Auth0JwtHeader>(bearer, jsonMapperSettings)
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
