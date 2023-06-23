module Edu.Server.Tests.Index.Api.V1.Blog.Tests

open FSharp.Control
open System.Net
open Edu.Server.Tests.TestApi
open Xunit

[<Fact>]
let ``GET /api/v1/blog should succeed`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/blog")
        Assert.Equal(HttpStatusCode.OK, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/blog/ should succeed`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/blog/")
        Assert.Equal(HttpStatusCode.OK, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/blog/1 should succeed`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/blog/1")
        Assert.Equal(HttpStatusCode.OK, response.StatusCode)
    }

[<Fact>]
let ``GET /api/v1/blog/asd should return 404`` () =
    task {
        let api = TestDependencies.Server.CreateClient()
        let! response = api.GetAsync("/api/v1/blog/asd")
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
    }
