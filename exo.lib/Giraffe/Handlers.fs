module Exo.Lib.Giraffe.Handlers

open Giraffe
open Microsoft.AspNetCore.Http
open System.Net.Http
open Exo.Lib.ActivePatterns
open Exo.Lib.Auth0
open System.Net.Http.Headers

let justContinue : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) -> next ctx

let notLoggedIn : HttpFunc -> HttpContext -> HttpFuncResult =
  RequestErrors.UNAUTHORIZED
    "Basic"
    "Some Realm"
    "You must be logged in."

let mustBeLoggedIn (auth0HttpClient : HttpClient) (validator : HttpClient -> string -> option<Auth0Client>) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let result =
      match ctx.TryGetRequestHeader "Authorization" with
      | None -> notLoggedIn
      | Some authorizationHeader ->
        match authorizationHeader with
        | StringPrefix "Bearer " token ->
          auth0HttpClient.DefaultRequestHeaders.Authorization <- new AuthenticationHeaderValue("Bearer", token)
          let auth0Client = validator auth0HttpClient token
          match auth0Client with
          | None -> notLoggedIn
          | _ -> justContinue
        | _ -> notLoggedIn
    result next ctx
