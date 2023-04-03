module Api.V1.Blog

open Giraffe
open Giraffe.EndpointRouting
open Helpers
open Microsoft.AspNetCore.Http
open Models
open System.Collections.Generic
open System.IO
open System.Text.Json
open System.Text.Json.Serialization

let blogIndex = 
  JsonSerializer.Deserialize<BlogIndex.T>(
    File.ReadAllText("./Data/blogs/index.json")
  )

let getAsHtml (id: string) : HttpHandler =
  let path = $"Data/blogs/{id}.html"
  let exists = blogIndex.Blogs.ContainsKey(id)
  match File.Exists(path), exists with
  | true, true -> htmlFile path
  | _ -> RequestErrors.NOT_FOUND $"Blog not found with id {id}"

let getAsJson (id: string) : HttpHandler =
  let path = $"Data/blogs/{id}.html"
  let exists = blogIndex.Blogs.ContainsKey(id)
  match File.Exists(path), exists with
  | true, true ->
    json
      {
        Blog.T.Id = id 
        Blog.T.Content = File.ReadAllText(path)
        Blog.T.Metadata = blogIndex.Blogs[id]
      }
  | _ -> RequestErrors.NOT_FOUND $"Blog not found with id {id}"

let get (id: string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let contentType =
      match ctx.TryGetRequestHeader "Content-Type" with
      | None -> "application/json"
      | Some value -> value

    match contentType with
    | StringPrefix "application/json" _ -> getAsJson id next ctx
    | StringPrefix "text/html" _ -> getAsHtml id next ctx
    | _ -> RequestErrors.BAD_REQUEST $"Unsupported content type: {contentType}" next ctx

let getAll : HttpHandler =
  json blogIndex
