module Helpers

open Giraffe
open Models
open Microsoft.AspNetCore.Http
open System.Net.Http
open System.Collections.Generic
open Jose
open System.Text

let getWellKnownKeysAsync (auth0HttpClient : HttpClient) =
  async {
    let! response = auth0HttpClient.GetAsync("/.well-known/jwks.json") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    return JwkSet.FromJson(content, JWT.DefaultSettings.JsonMapper)
  }

let admins = [
  "exokomodo@gmail.com";
  "brandonapol@cedarville.edu";
]
let paidUsers = [
  "jamesaorson@gmail.com";
]

let getMachineToMachineAccessToken (unauthenticatedAuth0HttpClient : HttpClient) (serializer : Serializers.JsonSerializer) (clientId : string) (clientSecret : string) : option<Auth0AccessTokenResponse> =
  let jsonContent = new StringContent(
    serializer.Serialize(
      { GrantType = "client_credentials"
        ClientId = clientId
        ClientSecret = clientSecret
        Audience = "https://services.edu.exokomodo.com" }
    ),
    Encoding.UTF8,
    "application/json"
  )
  try
    let response = (
      unauthenticatedAuth0HttpClient.PostAsync(
        "oauth/token",
        jsonContent
      ) |> Async.AwaitTask |> Async.RunSynchronously
    )
    if response.IsSuccessStatusCode then
      serializer.Deserialize<Auth0AccessTokenResponse>(
        response.Content.ReadAsStringAsync()
          |> Async.AwaitTask
          |> Async.RunSynchronously
      ) |> Some
    else
      printfn "HTTP request for access token failed with status code %s" (response.StatusCode.ToString())
      None
  with
    | :? HttpRequestException as ex ->
      printfn "Failed HTTP request for access token: %s" ex.Message
      None

let getUserInfoAsync (auth0HttpClient : HttpClient) (serializer : Json.ISerializer) : Async<option<UserInfo>> =
  async {
    let! response = auth0HttpClient.GetAsync("/userinfo") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    try
      return serializer.Deserialize<UserInfo> content |> Some
    with
      | :? System.Text.Json.JsonException as ex ->
        printfn "%s" ex.Message
        return None
  }

let validateAccessTokenWithKeySet (keySet : JwkSet) (bearer : string) : string =
  let header = Jose.JWT.Headers<Models.JsonWebTokenHeader>(bearer, JWT.DefaultSettings)
  printfn "Bearer header decoded: %s" header.KeyId
  let kid = header.KeyId
  let key =
    List.find
      (fun (x : Jwk) -> x.KeyId = kid)
      (Seq.toList keySet.Keys)
  let decoded = Jose.JWT.Decode<IDictionary<string, obj>> (bearer, key)
  let clientId = decoded["azp"] :?> string
  // TODO: Return jwt object that is readable, or parse this into a custom auth object
  bearer

let validateAccessToken (auth0HttpClient : HttpClient) (bearer : string) : string =
  validateAccessTokenWithKeySet
    (getWellKnownKeysAsync auth0HttpClient |> Async.RunSynchronously)
    bearer
