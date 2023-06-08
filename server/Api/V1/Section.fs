module Api.V1.Section

open Giraffe
open Microsoft.AspNetCore.Http
open Models
open MongoDB.Driver
open System.Threading

let private _updateSection (collection : IMongoCollection<Section>) (section : Section) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let filter = Builders<Section>.Filter.Eq("Id", section.Id)
    let mutable update = Builders<Section>.Update.Set(
      (fun _section -> _section.Difficulty),
      section.Difficulty
    )
    update <- update.Set(
      (fun _section -> _section.Metadata),
      section.Metadata
    )
    collection.UpdateOne(filter, update, null, new CancellationToken()) |> ignore
    printfn "%O" section
    json section next ctx

let put (collection : IMongoCollection<Section>) (section : Section) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    _updateSection collection section next ctx
