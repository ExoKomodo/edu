module Helpers

open Giraffe
open Models
open Microsoft.AspNetCore.Http
open System.Net.Http
open System.Net.Http.Headers
open System.Text.Json.Serialization
open System.Collections.Generic

let (|StringPrefix|_|) (prefix : string) (str : string) =
  if str.StartsWith(prefix) then
    str.Substring(prefix.Length) |> Some
  else
    None

let admins = ["exokomodo@gmail.com"; "brandonapol@cedarville.edu"]
let paidUsers = ["jamesaorson@gmail.com"]

let getSerializer () =
  let serializationOptions = SystemTextJson.Serializer.DefaultOptions
  serializationOptions.Converters.Add(JsonFSharpConverter(JsonUnionEncoding.FSharpLuLike))
  new SystemTextJson.Serializer(serializationOptions)

let getUserInfoAsync (auth0HttpClient : HttpClient) (serializer : Json.ISerializer) : Async<option<UserInfo>> =
  async {
    let! response = auth0HttpClient.GetAsync("/userinfo") |> Async.AwaitTask
    printfn "Hello"
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    printfn "%s" content
    try
      return serializer.Deserialize<UserInfo> content |> Some
    with
      | :? System.Text.Json.JsonException as ex ->
        printfn "%s" ex.Message
        return None
  }

let justContinue : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) -> next ctx

let notLoggedIn =
  RequestErrors.UNAUTHORIZED
    "Basic"
    "Some Realm"
    "You must be logged in."

let mustBeLoggedIn : HttpFunc -> HttpContext -> HttpFuncResult =
  requiresAuthentication notLoggedIn

let mustBePaidUsersOrHigher (auth0HttpClient : HttpClient): HttpHandler =
  // TODO: Parse the jwt yourself for the email, and use mustBeLoggedIn
 fun (next : HttpFunc) (ctx : HttpContext) ->
    let result =
      match ctx.TryGetRequestHeader "Authorization" with
      | Some authorizationHeader ->
        match authorizationHeader with
        | StringPrefix "Bearer " token ->
          auth0HttpClient.DefaultRequestHeaders.Authorization <- new AuthenticationHeaderValue("Bearer", token)
          match getUserInfoAsync auth0HttpClient (ctx.GetJsonSerializer()) |> Async.RunSynchronously with
          | Some userInfo ->
            let isAdmin = List.contains userInfo.Email admins
            let isPaidUser = List.contains userInfo.Email paidUsers
            match isAdmin, isPaidUser with
            | false, false -> 
              RequestErrors.FORBIDDEN $"Not a paid user or higher permissions"
            | _, _ -> justContinue
          | None -> notLoggedIn
        | _ -> notLoggedIn
      | None -> notLoggedIn
    result next ctx
