module Routes.Ping

open Giraffe

let get() = text "pong"
