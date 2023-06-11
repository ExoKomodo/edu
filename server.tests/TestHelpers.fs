module TestHelpers

open Giraffe
open Models
open System.Net.Http
open System.Text

let getAccessTokenAsync (unauthenticatedAuth0HttpClient : HttpClient) (serializer : Json.ISerializer) (clientId : string) (clientSecret : string) : Async<option<Auth0AccessTokenResponse>> =
  printfn "Getting access token..."
  async {
    let jsonContent = new StringContent(
      serializer.SerializeToString(
        { GrantType = "client_credentials"
          ClientId = clientId
          ClientSecret = clientSecret
          Audience = "https://services.edu.exokomodo.com" }
      ),
      Encoding.UTF8,
      "application/json"
    )
    printfn "%s" (jsonContent.ReadAsStringAsync() |> Async.AwaitTask |> Async.RunSynchronously)
    let! response = (
      unauthenticatedAuth0HttpClient.PostAsync(
        "oauth/token",
        jsonContent
      ) |> Async.AwaitTask
    )
    if response.IsSuccessStatusCode then
      let content = response.Content.ReadAsStringAsync() |> Async.AwaitTask |> Async.RunSynchronously
      return serializer.Deserialize(
        response.Content.ReadAsStringAsync() |> Async.AwaitTask |> Async.RunSynchronously
      ) |> Some
    else
      printfn "Response bad: %s" (response.StatusCode.ToString())
      return None
  }
