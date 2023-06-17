module Edu.Server.Tests.Index.Api.V1.Blob.Tests

open FSharp.Control
open System.Net
open Edu.Server.Tests.TestApi
open Xunit

let bearerToken = $"Bearer {TestDependencies.AccessToken}"

[<Fact>]
let ``GET /api/v1/blob should return 401, without an Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/blob")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/ should return 401, without an Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/blob/")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/images/loading.jpg should return 401, without an Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/blob/images/loading.jpg")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/asd should return 401, without an Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    let! response = api.GetAsync("/api/v1/blob/asd")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob should return 401, having a bad Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/blob")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/ should return 401, having a bad Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/blob/")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/images/loading.jpg should return 401, having a bad Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/blob/images/loading.jpg")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/asd should return 401, having a bad Authorization header`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", "foo")
    let! response = api.GetAsync("/api/v1/blob/asd")
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob should return 400`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", bearerToken)
    let! response = api.GetAsync("/api/v1/blob")
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/ should return 400`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", bearerToken)
    let! response = api.GetAsync("/api/v1/blob/")
    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob?url=images/loading.jpg should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", bearerToken)
    let! response = api.GetAsync("/api/v1/blob?url=images/loading.jpg")
    Assert.Equal(HttpStatusCode.OK, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob?url=/images/loading.jpg should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", bearerToken)
    let! response = api.GetAsync("/api/v1/blob?url=/images/loading.jpg")
    Assert.Equal(HttpStatusCode.OK, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/?url=images/loading.jpg should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", bearerToken)
    let! response = api.GetAsync("/api/v1/blob/?url=images/loading.jpg")
    Assert.Equal(HttpStatusCode.OK, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/?url=/images/loading.jpg should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", bearerToken)
    let! response = api.GetAsync("/api/v1/blob/?url=/images/loading.jpg")
    Assert.Equal(HttpStatusCode.OK, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob?url=asd should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", bearerToken)
    let! response = api.GetAsync("/api/v1/blob?url=asd")
    Assert.Equal(HttpStatusCode.OK, response.StatusCode)
  }

[<Fact>]
let ``GET /api/v1/blob/?url=asd should succeed`` () =
  task {
    let api = TestDependencies.Server.CreateClient()
    api.DefaultRequestHeaders.Add("Authorization", bearerToken)
    let! response = api.GetAsync("/api/v1/blob/?url=asd")
    Assert.Equal(HttpStatusCode.OK, response.StatusCode)
  }
