module Api.V1.Ping

open Giraffe

let get : HttpHandler = text "pong"
