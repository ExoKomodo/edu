module Api.V1.Assignment

open Giraffe
open Microsoft.AspNetCore.Http
open Models
open MongoDB.Driver
open System.Threading

let private _updateAssignment (collection : IMongoCollection<Assignment>) (assignment : Assignment) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let filter = Builders<Assignment>.Filter.Eq("Id", assignment.Id)
    let mutable update = Builders<Assignment>.Update.Set(
      (fun _assignment -> _assignment.ProblemDescription),
      assignment.ProblemDescription
    )
    update <- update.Set(
      (fun _assignment -> _assignment.Metadata),
      assignment.Metadata
    )
    collection.UpdateOne(filter, update, null, new CancellationToken()) |> ignore
    printfn "%O" assignment
    json assignment next ctx

let put (collection : IMongoCollection<Assignment>) (assignment : Assignment) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    _updateAssignment collection assignment next ctx
