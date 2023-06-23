module Edu.Server.Tests.TestApi

open ExoKomodo.Lib.Auth0
open ExoKomodo.Lib.Serializers
open Microsoft.AspNetCore.Mvc.Testing
open Program
open System
open System.Net.Http

let runTestApi () =
  (new WebApplicationFactory<Program>()).Server

let unauthenticatedAuth0HttpClient = new HttpClient(
  BaseAddress = new Uri("https://exokomodo.us.auth0.com")
)

let serializer = Json.Serializer()

type TestDependencies () =
  static let server =
    runTestApi()
  
  static let clientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID")
  static let clientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET")
  static let clientParams = Auth0ClientParams.FromEduClientCredentials clientId clientSecret
  static let accessToken =
    match getMachineToMachineAccessTokenAsync unauthenticatedAuth0HttpClient serializer clientParams |> Async.RunSynchronously with
    | Some token -> token.AccessToken
    | None -> raise (HttpRequestException("Failed to get access token"))
  static let jsonSerializer = Json.Serializer()

  static member AccessToken = accessToken
  static member ClientId = clientId
  static member ClientSecret = clientSecret
  static member JsonSerializer = jsonSerializer
  static member Server = server
