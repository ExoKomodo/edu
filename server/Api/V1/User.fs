module Api.V1.User

open Giraffe
open Lib.ActivePatterns
open Lib.Auth0
open Lib.Giraffe.Handlers
open Lib.Serializers
open Microsoft.AspNetCore.Http
open System.Net.Http
open System.Net.Http.Headers

let getInfo (auth0HttpClient : HttpClient) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    match ctx.TryGetRequestHeader "Authorization" with
    | Some value ->
      match value with
      | StringPrefix "Bearer " token ->
        auth0HttpClient.DefaultRequestHeaders.Authorization <- new AuthenticationHeaderValue("Bearer", token)
        json
          (getUserInfoAsync
            auth0HttpClient (ctx.GetJsonSerializer() :?> JsonSerializer) |> Async.RunSynchronously)
          next
          ctx
      | _ -> notLoggedIn next ctx
    | None -> notLoggedIn next ctx
