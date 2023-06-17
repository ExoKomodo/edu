module ExoKomodo.Lib.Giraffe.Handlers

open Giraffe
open Microsoft.AspNetCore.Http
open System.Net.Http
open ExoKomodo.Lib.ActivePatterns
open ExoKomodo.Lib.Auth0
open System.Net.Http.Headers

let justContinue : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) -> next ctx

let notLoggedIn : HttpFunc -> HttpContext -> HttpFuncResult =
  RequestErrors.UNAUTHORIZED
    "Basic"
    "Some Realm"
    "You must be logged in."

type HandlerContextParameters =
  static member BearerToken : string = "ExoLibGiraffeBearerToken"
  static member Auth0Client : string = "ExoLibGiraffeAuth0Client"

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
          | _ ->
            ctx.Items[HandlerContextParameters.BearerToken] <- token
            ctx.Items[HandlerContextParameters.Auth0Client] <- auth0Client
            justContinue
        | _ -> notLoggedIn
    result next ctx
