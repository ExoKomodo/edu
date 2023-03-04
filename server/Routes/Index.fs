module Routes.Index

open Giraffe

let get : HttpHandler = text "Hello world"
