module TestApi

open Giraffe
open Microsoft.AspNetCore.Mvc.Testing
open Program
open System
open System.Net.Http

let runTestApi () =
  (new WebApplicationFactory<Program>()).Server

let unauthenticatedAuth0HttpClient = new HttpClient(
  BaseAddress = new Uri("https://exokomodo.us.auth0.com")
)

let serializer = Helpers.getSerializer()

type TestDependencies () =
  static let server =
    runTestApi()
  
  static let clientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID")
  static let clientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET")
  static let accessTokenAsync =
    async {
      let! response = TestHelpers.getAccessTokenAsync unauthenticatedAuth0HttpClient serializer clientId clientSecret
      return
        match response with
        | Some token -> token.AccessToken
        | None -> raise (HttpRequestException("Failed to get access token"))
    }

  static member AccessToken = accessTokenAsync |> Async.RunSynchronously
  static member ClientId = clientId
  static member ClientSecret = clientSecret
  static member Server = server
