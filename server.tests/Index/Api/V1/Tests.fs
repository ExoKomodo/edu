module Edu.Server.Tests.Index.Api.V1.Tests

open FSharp.Control
open System.Net
open Edu.Server.Tests.TestApi
open Xunit

[<Fact>]
let ``GET /api/v1 should succeed`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1")
        Assert.Equal(HttpStatusCode.OK, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/ should succeed`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/")
        Assert.Equal(HttpStatusCode.OK, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/asd should return 401`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/asd")
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
    }
