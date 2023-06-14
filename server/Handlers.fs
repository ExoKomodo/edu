module Handlers

open Giraffe
open Helpers
open Models
open Microsoft.AspNetCore.Http
open System.Net.Http
open System.Net.Http.Headers
open Lib.ActivePatterns
open Lib.Giraffe.Handlers

let canAccessPaidContent (auth0HttpClient : HttpClient): HttpHandler =
  mustBeLoggedIn >=> fun (next : HttpFunc) (ctx : HttpContext) ->
    let result =
      match ctx.TryGetRequestHeader "Authorization" with
      | Some authorizationHeader ->
        match authorizationHeader with
        | StringPrefix "Bearer " token ->
          auth0HttpClient.DefaultRequestHeaders.Authorization <- new AuthenticationHeaderValue("Bearer", token)
          validateAccessToken auth0HttpClient token |> ignore
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
