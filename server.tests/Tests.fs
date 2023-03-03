module Tests

open FSharp.Control
open TestApi
open Xunit

[<Fact>]
let ``/ should return "Hello world"`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/")
    let! responseContent = response.Content.ReadAsStringAsync()
    Assert.Equal("Hello world", responseContent)
  }

let ``/ping should return "pong"`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/ping")
    let! responseContent = response.Content.ReadAsStringAsync()
    Assert.Equal("pong", responseContent)
  }
