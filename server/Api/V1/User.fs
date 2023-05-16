module Api.V1.User

open Giraffe
open Helpers
open Microsoft.AspNetCore.Http
open System.Net.Http

let getInfo (auth0HttpClient : HttpClient) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    match ctx.TryGetRequestHeader "Authorization" with
    | Some value ->
      auth0HttpClient.DefaultRequestHeaders.Add("Authorization", value)
      json
        (getUserInfoAsync
          auth0HttpClient (ctx.GetJsonSerializer()) |> Async.RunSynchronously)
        next
        ctx
    | None -> notLoggedIn next ctx
