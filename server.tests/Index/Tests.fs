module Index.Tests

open FSharp.Control
open System.Net
open TestApi
open Xunit

[<Fact>]
let ``GET / should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``GET /asd should return 404`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
  }
