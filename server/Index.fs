module Index

open Giraffe

let get : HttpHandler = htmlFile "./Routes.html"
