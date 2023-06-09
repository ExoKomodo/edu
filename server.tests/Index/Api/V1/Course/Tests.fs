module Index.Api.V1.Tests

open FSharp.Control
open System.Net
open TestApi
open Xunit

[<Fact>]
let ``GET /api/v1/course should return 401`` () =
  task {
    let api = Dependencies.Client
    let! response = api.GetAsync("/api/v1/course")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/ should return 401`` () =
  task {
    let api = Dependencies.Client
    let! response = api.GetAsync("/api/v1/course/")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/intro should return 401`` () =
  task {
    let api = Dependencies.Client
    let! response = api.GetAsync("/api/v1/course/intro")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/asd should return 401`` () =
  task {
    let api = Dependencies.Client
    let! response = api.GetAsync("/api/v1/course/asd")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }