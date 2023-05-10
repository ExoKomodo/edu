module Api.V1.Course

open Giraffe
open Giraffe.EndpointRouting
open Helpers
open Microsoft.AspNetCore.Http
open Models
open System.Collections.Generic
open System.IO
open System.Text.Json
open System.Text.Json.Serialization

let courseIndex = 
  JsonSerializer.Deserialize<CourseIndex>(
    File.ReadAllText("./Data/courses/index.json")
  )

let getAsXml (id: string) : HttpHandler =
  let path = $"Data/courses/{id}.html"
  let exists = courseIndex.Courses.ContainsKey(id)
  match File.Exists(path), exists with
  | true, true ->
    xml
      { Course.Id = id 
        Course.Content = File.ReadAllText(path)
        Course.Metadata = courseIndex.Courses[id] }
  | _ -> RequestErrors.NOT_FOUND $"Course not found with id {id}"

let getAsJson (id: string) : HttpHandler =
  let path = $"Data/courses/{id}.html"
  let exists = courseIndex.Courses.ContainsKey(id)
  match File.Exists(path), exists with
  | true, true ->
    json
      { Course.Id = id 
        Course.Content = File.ReadAllText(path)
        Course.Metadata = courseIndex.Courses[id] }
  | _ -> RequestErrors.NOT_FOUND $"Course not found with id {id}"

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
  json courseIndex
