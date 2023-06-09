module Index.Api.V1.Tests

open FSharp.Control
open System.Net
open TestApi
open Xunit

[<Fact>]
let ``GET /api/v1/blog should succeed`` () =
  task {
    let api = Dependencies.Client
    let! response = api.GetAsync("/api/v1/blog")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``GET /api/v1/blog/ should succeed`` () =
  task {
    let api = Dependencies.Client
    let! response = api.GetAsync("/api/v1/blog/")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``GET /api/v1/blog/1 should succeed`` () =
  task {
    let api = Dependencies.Client
    let! response = api.GetAsync("/api/v1/blog/1")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``GET /api/v1/blog/asd should return 404`` () =
  task {
    let api = Dependencies.Client
    let! response = api.GetAsync("/api/v1/blog/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
  }
