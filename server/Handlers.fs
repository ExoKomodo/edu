module Edu.Server.Handlers

open Giraffe
open Exo.Lib.ActivePatterns
open Exo.Lib.Auth0
open Exo.Lib.Giraffe.Handlers
open Exo.Lib.Serializers
open Microsoft.AspNetCore.Http
open System.Net.Http
open System.Net.Http.Headers

// TODO: Add access control lists to external data definition rather than in code
let admins = [
  "exokomodo@gmail.com";
  "brandonapol@cedarville.edu";
]
let paidUsers = [
  "jamesaorson@gmail.com";
]

let canAccessPaidContent (auth0HttpClient : HttpClient) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let result =
      match ctx.TryGetRequestHeader "Authorization" with
      | None -> notLoggedIn
      | Some authorizationHeader ->
        match authorizationHeader with
        | StringPrefix "Bearer " token ->
          auth0HttpClient.DefaultRequestHeaders.Authorization <- new AuthenticationHeaderValue("Bearer", token)
          let auth0Client = validateAccessToken auth0HttpClient token
          match auth0Client with
          | None -> notLoggedIn
          | Some Machine -> justContinue
          | Some User -> 
            match getUserInfoAsync auth0HttpClient (ctx.GetJsonSerializer() :?> JsonSerializer) |> Async.RunSynchronously with
            | None -> notLoggedIn
            | Some userInfo ->
              let isAdmin = List.contains userInfo.Email admins
              let isPaidUser = List.contains userInfo.Email paidUsers
              match isAdmin, isPaidUser with
              | false, false -> RequestErrors.FORBIDDEN "Not a paid user or higher permissions"
              | _, _ -> justContinue
        | _ -> notLoggedIn
    result next ctx
