module Helpers

open Constants
open Giraffe
open Models
open Microsoft.AspNetCore.Http
open System.Net.Http
open System.Text.Json

let (|StringPrefix|_|) (prefix: string) (str: string) =
  if str.StartsWith(prefix) then
    str.Substring(prefix.Length) |> Some
  else
    None

let notLoggedIn =
  RequestErrors.UNAUTHORIZED
    "Basic"
    "Some Realm"
    "You must be logged in."

let mustBeLoggedIn: HttpFunc -> HttpContext -> HttpFuncResult = requiresAuthentication notLoggedIn

let getUserInfoAsync (client: HttpClient) (serializer: Json.ISerializer) : Async<UserInfo> =
  async {
    let! response = client.GetAsync($"{Constants.defaultScheme}{Constants.auth0BaseUrl}/userinfo") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    return serializer.Deserialize<UserInfo> content
  }

