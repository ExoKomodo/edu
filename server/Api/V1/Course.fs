module Api.V1.Course

open Giraffe
open Helpers
open Microsoft.AspNetCore.Http
open Models
open MongoDB.Driver
open System.Threading

let private _createCourse (collection : IMongoCollection<Course>) (course : Course) : HttpHandler =
  collection.InsertOne(course, null, new CancellationToken())
  
  json course

let private _deleteCourse (collection : IMongoCollection<Course>) (id : string) =
  let filter = Builders<Course>.Filter.Eq("Id", id)
  collection.DeleteOne(filter) |> ignore

let private _getCourses (collection : IMongoCollection<Course>) =
  collection.Find(Builders<Course>.Filter.Empty).ToEnumerable()
  |> Seq.cast<Course>

let private _getCourse (collection : IMongoCollection<Course>) (id : string) =
  let filter = Builders<Course>.Filter.Eq("Id", id)
  collection.Find(filter).FirstOrDefault()

let private _getInFormat (formatter : Course -> HttpFunc -> HttpContext -> HttpFuncResult) (collection : IMongoCollection<Course>) (id : string) : HttpHandler =
  let course = _getCourse collection id
  match box course with
  | null -> RequestErrors.NOT_FOUND $"Course not found with id {id}"
  | _ -> formatter course

let private _getAsXml (collection : IMongoCollection<Course>) (id : string) : HttpHandler = _getInFormat xml collection id

let private _getAsJson (collection : IMongoCollection<Course>) (id : string) : HttpHandler = _getInFormat json collection id

let private _updateCourse (collection : IMongoCollection<Course>) (course : Course) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let filter = Builders<Course>.Filter.Eq("Id", course.Id)
    let mutable update = Builders<Course>.Update.Set(
      (fun _course -> _course.Content),
      course.Content
    )
    update <- update.Set(
      (fun _course -> _course.Metadata),
      course.Metadata
    )
    collection.UpdateOne(filter, update, null, new CancellationToken()) |> ignore
    printfn "%O" course
    json course next ctx

let delete (collection : IMongoCollection<Course>) (id : string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    _deleteCourse collection id
    json id next ctx

let get (collection : IMongoCollection<Course>) (id : string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let accept =
      match ctx.TryGetRequestHeader "Accept" with
      | None -> "application/json"
      | Some value -> value

    let result =
      match accept with
      | StringPrefix "application/xml" _ | StringPrefix "text/xml" _ -> _getAsXml collection id
      | _ -> _getAsJson collection id
    result next ctx

let post (collection : IMongoCollection<Course>) (course : Course) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    _createCourse collection course next ctx

let put (collection : IMongoCollection<Course>) (course : Course) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    _updateCourse collection course next ctx

let getAllMetadata (collection : IMongoCollection<Course>) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    ( (_getCourses collection)
      |> Seq.map (fun course -> course.Id, course.Metadata)
      |> dict
      |> json) next ctx