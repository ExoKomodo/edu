module Edu.Server.Tests.Index.Api.Tests

open FSharp.Control
open System.Net
open Edu.Server.Tests.TestApi
open Xunit

[<Fact>]
let ``GET /api should succeed`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api")
        Assert.Equal(HttpStatusCode.OK, response.StatusCode)
    }

[<Fact>]
let ``GET /api/ should succeed`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/")
        Assert.Equal(HttpStatusCode.OK, response.StatusCode)
    }

[<Fact>]
let ``GET /api/asd should return 404`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/asd")
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
    }
