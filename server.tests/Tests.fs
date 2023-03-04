module Tests

open FSharp.Control
open TestApi
open Xunit

[<Fact>]
let ``/api/v1 should return "Hello world"`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1")
    let! responseContent = response.Content.ReadAsStringAsync()
    Assert.Equal("Hello world", responseContent)
  }

let ``/api/v1/ should return "Hello world"`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/")
    let! responseContent = response.Content.ReadAsStringAsync()
    Assert.Equal("Hello world", responseContent)
  }

let ``/api/v1/ping should return "pong"`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/ping")
    let! responseContent = response.Content.ReadAsStringAsync()
    Assert.Equal("pong", responseContent)
  }
