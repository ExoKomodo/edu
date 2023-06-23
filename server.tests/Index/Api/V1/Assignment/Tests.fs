module Edu.Server.Tests.Index.Api.V1.Assignment.Tests

open FSharp.Control
open System.Net
open Edu.Server.Tests.TestApi
open Xunit

[<Fact>]
let ``GET /api/v1/assignment should return 401, without an Authorization header``
    ()
    =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/assignment")
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/assignment/ should return 401, without an Authorization header``
    ()
    =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/assignment/")
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/assignment/intro should return 401, without an Authorization header``
    ()
    =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/assignment/intro")
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/assignment/asd should return 401, without an Authorization header``
    ()
    =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/assignment/asd")
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/assignment should return 401, having a bad Authorization header``
    ()
    =
    task {
        let api = TestDependencies.Server.CreateClient()
        api.DefaultRequestHeaders.Add("Authorization", "foo")
        let! response = api.GetAsync("/api/v1/assignment")
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/assignment/ should return 401, having a bad Authorization header``
    ()
    =
    task {
        let api = TestDependencies.Server.CreateClient()
        api.DefaultRequestHeaders.Add("Authorization", "foo")
        let! response = api.GetAsync("/api/v1/assignment/")
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/assignment/intro should return 401, having a bad Authorization header``
    ()
    =
    task {
        let api = TestDependencies.Server.CreateClient()
        api.DefaultRequestHeaders.Add("Authorization", "foo")
        let! response = api.GetAsync("/api/v1/assignment/intro")
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/assignment/asd should return 401, having a bad Authorization header``
    ()
    =
    task {
        let api = TestDependencies.Server.CreateClient()
        api.DefaultRequestHeaders.Add("Authorization", "foo")
        let! response = api.GetAsync("/api/v1/assignment/asd")
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode)
    }
