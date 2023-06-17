module Helpers

open Models
open System.Net.Http
open System.Collections.Generic
open Jose
open System.Text
open Lib.Jwt
open Lib.Serializers

let admins = [
  "exokomodo@gmail.com";
  "brandonapol@cedarville.edu";
]
let paidUsers = [
  "jamesaorson@gmail.com";
]

let getMachineToMachineAccessToken (unauthenticatedAuth0HttpClient : HttpClient) (serializer : JsonSerializer) (clientId : string) (clientSecret : string) : option<Auth0AccessTokenResponse> =
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

let validateAccessTokenWithKeySet (keySet : JwkSet) (bearer : string) : option<Auth0Client> =
  let header = Jose.JWT.Headers<Models.JsonWebTokenHeader>(bearer, JWT.DefaultSettings)
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
