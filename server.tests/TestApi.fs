module TestApi
  
open Microsoft.AspNetCore.Mvc.Testing
open Program
open Microsoft.AspNetCore.TestHost

let runTestApi () =
  (new WebApplicationFactory<Program>()).Server

type Dependencies () =
  static let server =
    printfn "We are getting called more than once, probably on Open"
    runTestApi()
  
  static member Server = server
