module Lib.Giraffe.Handlers

open Giraffe
open Lib.ActivePatterns
open Microsoft.AspNetCore.Http
open System.Net.Http

let justContinue : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) -> next ctx

let notLoggedIn =
  RequestErrors.UNAUTHORIZED
    "Basic"
    "Some Realm"
    "You must be logged in."

let mustBeLoggedIn : HttpFunc -> HttpContext -> HttpFuncResult =
  requiresAuthentication notLoggedIn


