module Lib.Jwt

open System.Net.Http
open System.Text
open Jose
open Serializers

[<CLIMutable>]
type UserInfo =
  { [<Json.Serialization.JsonPropertyName("email")>]
    Email : string
    [<Json.Serialization.JsonPropertyName("email_verified")>]
    IsEmailVerified : bool
    [<Json.Serialization.JsonPropertyName("nickname")>]
    Nickname : string
    [<Json.Serialization.JsonPropertyName("name")>]
    Name : string
    [<Json.Serialization.JsonPropertyName("picture")>]
    Picture : string
    [<Json.Serialization.JsonPropertyName("sub")>]
    Sub : string
    [<Json.Serialization.JsonPropertyName("updated_at")>]
    UpdatedAt : string }

let getUserInfoAsync (client : HttpClient) (serializer : JsonSerializer) : Async<option<UserInfo>> =
  async {
    let! response = client.GetAsync("/userinfo") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    try
      return serializer.Deserialize<UserInfo> content |> Some
    with
      | :? Json.JsonException as ex ->
        printfn "%s" ex.Message
        return None
  }

let getWellKnownKeysAsync (client : HttpClient) =
  async {
    let! response = client.GetAsync("/.well-known/jwks.json") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    return JwkSet.FromJson(content, JWT.DefaultSettings.JsonMapper)
  }
