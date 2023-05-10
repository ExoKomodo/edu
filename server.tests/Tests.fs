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
let ``/asd should return 404`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
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
let ``/api/asd should return 404`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
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
let ``/api/v1/asd should return 404`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
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
let ``/api/v1/blog/asd should return 404`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/blog/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
  }

[<Fact>]
let ``/api/v1/course should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/course")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1/course/ should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/course/")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1/course/intro should succeed`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/course/intro")
    Assert.True(response.IsSuccessStatusCode)
  }

[<Fact>]
let ``/api/v1/course/asd should return 404`` () =
  task {
    let api = runTestApi().CreateClient()
    let! response = api.GetAsync("/api/v1/course/asd")
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode)
  }
