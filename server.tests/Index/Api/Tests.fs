module Index.Api.Tests

open FSharp.Control
open System.Net
open TestApi
open Xunit

[<Fact>]
let ``GET /api should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``GET /api/ should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``GET /api/asd should return 404`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
  }
