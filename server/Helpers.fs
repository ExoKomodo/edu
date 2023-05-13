module Helpers

open Giraffe
open Microsoft.AspNetCore.Http

let (|StringPrefix|_|) (prefix: string) (str: string) =
  if str.StartsWith(prefix) then
    str.Substring(prefix.Length) |> Some
  else
    None

let notLoggedIn =
    RequestErrors.UNAUTHORIZED
        "Basic"
        "Some Realm"
        "You must be logged in."

let mustBeLoggedIn: HttpFunc -> HttpContext -> HttpFuncResult = requiresAuthentication notLoggedIn
