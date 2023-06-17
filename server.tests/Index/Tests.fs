module Edu.Server.Tests.Index.Tests

open FSharp.Control
open System.Net
open Edu.Server.Tests.TestApi
open Xunit

[<Fact>]
let ``GET / should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/")
    Assert.Equal(HttpStatusCode.OK, response.StatusCode)
  }

[<Fact>]
let ``GET /asd should return 404`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
  }
