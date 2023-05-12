module Helpers

open Giraffe
open Microsoft.AspNetCore.Http

let (|StringPrefix|_|) (prefix: string) (str: string) =
  if str.StartsWith(prefix) then
    str.Substring(prefix.Length) |> Some
  else
    None

let decodeJwtFromHeader (ctx : HttpContext) : option<string> =
  let tokenHeaderOpt = ctx.TryGetRequestHeader "Authorization"
  match tokenHeaderOpt with
  | Some tokenHeader ->
    match tokenHeader with
    | StringPrefix "Bearer " token -> Some token
    | _ -> None
  | None -> None
