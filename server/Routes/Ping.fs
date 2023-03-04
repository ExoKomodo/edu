module Routes.Ping

open Giraffe

let get : HttpHandler = text "pong"
