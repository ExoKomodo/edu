module Tests

open FSharp.Control
open System.Net
open TestApi
open Xunit

[<Fact>]
let ``/ should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/ should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1 should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1/ should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1/blog should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/blog")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1/blog/ should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/blog/")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1/blog/1 should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/blog/1")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1/blog/0 should return 404`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/blog/0")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
  }

[<Fact>]
let ``/api/v1/bloo should return 404`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/bloo")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
  }
