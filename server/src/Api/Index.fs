module Edu.Server.Api.Index

open Giraffe

let get : HttpHandler = htmlFile "./Api/Routes.html"
