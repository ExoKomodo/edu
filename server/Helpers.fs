module Helpers

open Giraffe
open Models
open Microsoft.AspNetCore.Http
open System.Net.Http
open System.Net.Http.Headers
open System.Text.Json.Serialization
open System.Collections.Generic
open Jose

let (|StringPrefix|_|) (prefix : string) (str : string) =
  if str.StartsWith(prefix) then
    str.Substring(prefix.Length) |> Some
  else
    None

let admins = [
  "exokomodo@gmail.com";
  "brandonapol@cedarville.edu";
]
let paidUsers = [
  "jamesaorson@gmail.com";
]

let getUserInfoAsync (auth0HttpClient : HttpClient) (serializer : Json.ISerializer) : Async<option<UserInfo>> =
  async {
    let! response = auth0HttpClient.GetAsync("/userinfo") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
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

let getWellKnownKeysAsync (auth0HttpClient : HttpClient) =
  async {
    let! response = auth0HttpClient.GetAsync("/.well-known/jwks.json") |> Async.AwaitTask
    let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
    return JwkSet.FromJson(content, JWT.DefaultSettings.JsonMapper)
  }

let validateAccessToken (auth0HttpClient : HttpClient) (bearer : string) : string =
  // NOTE: Machine to machine
  // Bearer - eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkVxLVAtTE5oODZ2TkZ6VUNCSnZjVSJ9.eyJpc3MiOiJodHRwczovL2V4b2tvbW9kby51cy5hdXRoMC5jb20vIiwic3ViIjoiTlduTHF0VTg5SlI2ZHJDR2FhYll5Z0EyelVVUTd3b0dAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vc2VydmljZXMuZWR1LmV4b2tvbW9kby5jb20iLCJpYXQiOjE2ODY0OTYwNTcsImV4cCI6MTY4NjU4MjQ1NywiYXpwIjoiTlduTHF0VTg5SlI2ZHJDR2FhYll5Z0EyelVVUTd3b0ciLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.YY_v7KxfY84v1CiY9TM8fTszqedikzELHQpFR5eDxhFXl2gvQ_RnSUN-GLe87UT4lN4AjjuaOFLIDXYuTz-6db8CmKMqUeMDWaiRAgfXqydbmWU1ymtdmCN-gXsNfYu-916LaUOrTn0h5YuC_TZHFK7c2cuZ2C-1iSNdi4duCeyveeTxwjXSHobh5-K5dvl0IkWcu2G2fpqSJY_MlQQCENmYp_TOouPtx36KaSPmlVuFW041bQE-swLQ2jw1PlJPtwIeaO5S0YYH537uY0UG52mify1CtJnvRwMi8r88V64HMOl6Y_qNMwRbSzGJnHX5RaWchWWhoMhj4uY0uwTYlA
  // Decoded payload - {
  //   "iss": "https://exokomodo.us.auth0.com/",
  //   "sub": "NWnLqtU89JR6drCGaabYygA2zUUQ7woG@clients",
  //   "aud": "https://services.edu.exokomodo.com",
  //   "iat": 1686496057,
  //   "exp": 1686582457,
  //   "azp": "NWnLqtU89JR6drCGaabYygA2zUUQ7woG",
  //   "gty": "client-credentials"
  // }
  // NOTE: Client
  // Bearer - eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkVxLVAtTE5oODZ2TkZ6VUNCSnZjVSJ9.eyJpc3MiOiJodHRwczovL2V4b2tvbW9kby51cy5hdXRoMC5jb20vIiwic3ViIjoiYXV0aDB8NjQ1ZTc0ZTQzZDgxMzc0N2NhMjU3OGVlIiwiYXVkIjpbImh0dHBzOi8vc2VydmljZXMuZWR1LmV4b2tvbW9kby5jb20iLCJodHRwczovL2V4b2tvbW9kby51cy5hdXRoMC5jb20vdXNlcmluZm8iXSwiaWF0IjoxNjg2NDk2MTkxLCJleHAiOjE2ODY1ODI1OTEsImF6cCI6ImQwbmJHeVl2aFR4UGp5TDFlYWEzSzRvakxEVU50MUxYIiwic2NvcGUiOiJvcGVuaWQgcHJvZmlsZSBlbWFpbCJ9.lo065m0inr2bmWaPvuDo4h-Zk-5Gun7vuKvH3AHCoKjFggNd3VDNfOmapgeYNHga4ZjciWjoFJNZqsypgj3p9w8G6Tx-wopewRZ304dUXAJy1nQTo08OZ6I-0Jb3n2UBIRdqe5WFvjjDsIptZaLDtZjK04hEsMJlpa6PNuZSE8PEZh3uL_e8Ncv5Mktk5TIkdYgITnph3Q5hST1Q1ZhxSEsmBbXSdvQ5UfCqeJ07uZHLWjYt3HT6PJpgnQWsChYt0i8pAyAtqyv1FwWXJFSs66o8VUgdIS49X80x5R6hpw0IDNN2czLWVKvY-rMNFsna-xWwmoqXsP3Em6Xe1pZFIA
  // Decoded payload - {
  //   "iss": "https://exokomodo.us.auth0.com/",
  //   "sub": "auth0|645e74e43d813747ca2578ee",
  //   "aud": [
  //     "https://services.edu.exokomodo.com",
  //     "https://exokomodo.us.auth0.com/userinfo"
  //   ],
  //   "iat": 1686496191,
  //   "exp": 1686582591,
  //   "azp": "d0nbGyYvhTxPjyL1eaa3K4ojLDUNt1LX",
  //   "scope": "openid profile email"
  // }
  let header = Jose.JWT.Headers<Models.JsonWebTokenHeader>(bearer, JWT.DefaultSettings)
  printfn "Bearer header decoded: %s" header.KeyId
  let kid = header.KeyId
  let keySet = getWellKnownKeysAsync auth0HttpClient |> Async.RunSynchronously
  let key =
    List.find
      (fun (x : Jwk) -> x.KeyId = kid)
      (Seq.toList keySet.Keys)
  let decoded = Jose.JWT.Decode<IDictionary<string, obj>> (bearer, key)
  let clientId = decoded["azp"] :?> string
  // TODO: Return jwt object that is readable, or parse this into a custom auth object
  bearer

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
