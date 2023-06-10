module Index.Api.V1.Blob.Tests

open FSharp.Control
open System.Net
open TestApi
open Xunit

[<Fact>]
let ``GET /api/v1/blob should return 401, without an Authorization header`` () =
  task {
    let api = Dependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/blob")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/ should return 401, without an Authorization header`` () =
  task {
    let api = Dependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/blob/")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/images/loading.jpg should return 401, without an Authorization header`` () =
  task {
    let api = Dependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/blob/images/loading.jpg")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/asd should return 401, without an Authorization header`` () =
  task {
    let api = Dependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/blob/asd")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob should return 401, having a bad Authorization header`` () =
  task {
    let api = Dependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/blob")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/ should return 401, having a bad Authorization header`` () =
  task {
    let api = Dependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/blob/")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/images/loading.jpg should return 401, having a bad Authorization header`` () =
  task {
    let api = Dependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/blob/images/loading.jpg")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/asd should return 401, having a bad Authorization header`` () =
  task {
    let api = Dependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/blob/asd")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }
