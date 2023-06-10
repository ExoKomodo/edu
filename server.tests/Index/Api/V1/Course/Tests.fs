module Index.Api.V1.Course.Tests

open FSharp.Control
open System.Net
open TestApi
open Xunit

[<Fact>]
let ``GET /api/v1/course should return 401, without an Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/course")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/ should return 401, without an Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/course/")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/intro should return 401, without an Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/course/intro")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/asd should return 401, without an Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/course/asd")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course should return 401, with a bad Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/course")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/ should return 401, with a bad Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/course/")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/intro should return 401, with a bad Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/course/intro")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/course/asd should return 401, with a bad Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/course/asd")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }
