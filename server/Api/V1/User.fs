module Api.V1.User

open Giraffe
open Helpers
open Microsoft.AspNetCore.Http
open System
open System.Net.Http

let getInfo : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let client = new HttpClient(
      BaseAddress = new Uri($"{Constants.defaultScheme}{Constants.auth0BaseUrl}") 
    )
    client.DefaultRequestHeaders.Add("Host", Constants.auth0BaseUrl)
    match ctx.TryGetRequestHeader "Authorization" with
    | Some value ->
      client.DefaultRequestHeaders.Add("Authorization", value)
      json
        (getUserInfoAsync
          client (ctx.GetJsonSerializer()) |> Async.RunSynchronously)
        next
        ctx
    | None -> notLoggedIn next ctx
