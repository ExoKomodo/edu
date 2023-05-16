module Helpers

open Giraffe
open Models
open Microsoft.AspNetCore.Http
open System.Net.Http

let (|StringPrefix|_|) (prefix : string) (str : string) =
  if str.StartsWith(prefix) then
    str.Substring(prefix.Length) |> Some
  else
    None

let admins = ["exokomodo@gmail.com"]
let paidUsers = ["jamesaorson@gmail.com"]

let getUserInfoAsync (auth0HttpClient : HttpClient) (serializer : Json.ISerializer) : Async<UserInfo> =
  async {
    let! response = auth0HttpClient.GetAsync("/userinfo") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    return serializer.Deserialize<UserInfo> content
  }

let notLoggedIn =
  RequestErrors.UNAUTHORIZED
    "Basic"
    "Some Realm"
    "You must be logged in."

let mustBeLoggedIn : HttpFunc -> HttpContext -> HttpFuncResult =
  requiresAuthentication notLoggedIn

let mustBePaidUsersOrHigher (auth0HttpClient : HttpClient): HttpHandler =
 fun (next : HttpFunc) (ctx : HttpContext) ->
    match ctx.TryGetRequestHeader "Authorization" with
    | Some value ->
      auth0HttpClient.DefaultRequestHeaders.Add("Authorization", value)
      let userInfo = getUserInfoAsync auth0HttpClient (ctx.GetJsonSerializer()) |> Async.RunSynchronously
      let isAdmin = List.contains userInfo.Email admins
      let isPaidUser = List.contains userInfo.Email paidUsers
      match isAdmin, isPaidUser with
      | false, false -> 
        RequestErrors.FORBIDDEN $"Not allowed to view courses" next ctx
      | _, _ ->
        next ctx
    | None -> notLoggedIn next ctx
