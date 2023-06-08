module Api.V1.Course

open Giraffe
open Microsoft.AspNetCore.Http
open Models
open MongoDB.Driver
open System.Threading

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

let put (collection : IMongoCollection<Course>) (course : Course) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    _updateCourse collection course next ctx
