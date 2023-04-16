module Api.V1.Index

open Giraffe

let get : HttpHandler = htmlFile "./Api/V1/Routes.html"
