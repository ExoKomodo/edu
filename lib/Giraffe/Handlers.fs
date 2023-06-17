module Lib.Giraffe.Handlers

open Giraffe
open Microsoft.AspNetCore.Http

let justContinue : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) -> next ctx

let notLoggedIn : HttpFunc -> HttpContext -> HttpFuncResult =
  RequestErrors.UNAUTHORIZED
    "Basic"
    "Some Realm"
    "You must be logged in."
