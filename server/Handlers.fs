module Edu.Server.Handlers

open Giraffe
open ExoKomodo.Lib.ActivePatterns
open ExoKomodo.Lib.Auth0
open ExoKomodo.Lib.Giraffe.Handlers
open ExoKomodo.Lib.Serializers
open Microsoft.AspNetCore.Http
open System.Net.Http
open System.Net.Http.Headers

// TODO: Add access control lists to external data definition rather than in code
let admins = [
  "exokomodo@gmail.com";
  "brandonapol@cedarville.edu";
]

let paidEmailUsers = [
  "jamesaorson@gmail.com";
]

let paidAppleSubs = [
  "000029.21872332bd244cca8b5cf65af46d5e00.2035"; // jamesaorson@gmail.com
]
let paidAuth0Subs = [
  "645e74e43d813747ca2578ee"; // exokomodo@gmail.com
]
let paidGoogleSubs = [
  "107263294567498975062"; // exokomodo@gmail.com
  "113937910186417093745"; // jamesaorson@gmail.com
]
let paidGithubSubs = [
  "17893076"; // jamesaorson@gmail.com
]
let paidMicrosoftSubs = [
  "9e29fe3ca9a421c1"; // jamesaorson@gmail.com
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
            match getUserInfoAsync auth0HttpClient (ctx.GetJsonSerializer() :?> Json.Serializer) |> Async.RunSynchronously with
            | None -> notLoggedIn
            | Some userInfo ->
              match userInfo.Email with
              | Some email ->
                let isAllowed = List.contains true [
                  List.contains email admins;
                  List.contains email paidEmailUsers;
                ]
                if isAllowed then
                  justContinue
                else
                  RequestErrors.FORBIDDEN "Not an admin or paid user, logging in via email"
              | None ->
                let subResults =
                  match userInfo.Sub with
                  | StringPrefix "apple|" subId -> Some(subId, paidAppleSubs)
                  | StringPrefix "auth0|" subId -> Some(subId, paidAuth0Subs)
                  | StringPrefix "github|" subId -> Some(subId, paidGithubSubs)
                  | StringPrefix "google-oauth2|" subId -> Some(subId, paidGoogleSubs)
                  | StringPrefix "windowslive|" subId -> Some(subId, paidMicrosoftSubs)
                  | _ -> None
                match subResults with
                | Some (subId, paidSubs) ->
                  if List.contains subId paidSubs then
                    justContinue
                  else
                    RequestErrors.FORBIDDEN "Not a paid user"
                | None -> RequestErrors.FORBIDDEN $"Not a supported auth method with sub={userInfo.Sub}"
        | _ -> notLoggedIn
    result next ctx
