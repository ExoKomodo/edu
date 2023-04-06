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
  JsonSerializer.Deserialize<BlogIndex>(
    File.ReadAllText("./Data/blogs/index.json")
  )

let getAsXml (id: string) : HttpHandler =
  let path = $"Data/blogs/{id}.html"
  let exists = blogIndex.Blogs.ContainsKey(id)
  match File.Exists(path), exists with
  | true, true ->
    xml
      { Blog.Id = id 
        Blog.Content = File.ReadAllText(path)
        Blog.Metadata = blogIndex.Blogs[id] }
  | _ -> RequestErrors.NOT_FOUND $"Blog not found with id {id}"

let getAsJson (id: string) : HttpHandler =
  let path = $"Data/blogs/{id}.html"
  let exists = blogIndex.Blogs.ContainsKey(id)
  match File.Exists(path), exists with
  | true, true ->
    json
      { Blog.Id = id 
        Blog.Content = File.ReadAllText(path)
        Blog.Metadata = blogIndex.Blogs[id] }
  | _ -> RequestErrors.NOT_FOUND $"Blog not found with id {id}"

let get (id: string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let accept =
      match ctx.TryGetRequestHeader "Accept" with
      | None -> "application/json"
      | Some value -> value

    match accept with
    | StringPrefix "application/xml" _ | StringPrefix "text/xml" _ -> getAsXml id next ctx
    | _ -> getAsJson id next ctx

let getAll : HttpHandler =
  json blogIndex
