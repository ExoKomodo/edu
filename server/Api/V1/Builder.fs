module Api.V1.Builder

open Giraffe
open Helpers
open Microsoft.AspNetCore.Http
open Models
open MongoDB.Driver
open System.Threading

let inline createModel<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (model : ^T) : HttpHandler =
  collection.InsertOne(model, null, new CancellationToken())
  
  json model

let inline deleteModel<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (id : string) =
  let filter = Builders<^T>.Filter.Eq("Id", id)
  collection.DeleteOne(filter) |> ignore

let inline getModels<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) =
  collection.Find(Builders<^T>.Filter.Empty).ToEnumerable()
  |> Seq.cast<^T>

let inline getModel<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (id : string) =
  let filter = Builders<^T>.Filter.Eq("Id", id)
  collection.Find(filter).FirstOrDefault()

let inline getInFormat<^T when ^T :> IDatabaseModel> (formatter : ^T -> HttpFunc -> HttpContext -> HttpFuncResult) (collection : IMongoCollection<^T>) (id : string) : HttpHandler =
  let model = getModel collection id
  match box model with
  | null -> RequestErrors.NOT_FOUND $"Model not found with id {id}"
  | _ -> formatter model

let inline getAsXml<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (id : string) : HttpHandler = getInFormat xml collection id

let inline getAsJson<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (id : string) : HttpHandler = getInFormat json collection id

let inline delete<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (id : string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    deleteModel collection id
    json id next ctx

let inline get<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (id : string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let accept =
      match ctx.TryGetRequestHeader "Accept" with
      | None -> "application/json"
      | Some value -> value

    let result =
      match accept with
      | StringPrefix "application/xml" _ | StringPrefix "text/xml" _ -> getAsXml collection id
      | _ -> getAsJson collection id
    result next ctx

let inline post<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (model : ^T) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    createModel collection model next ctx

let inline getAllMetadata<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    ( (getModels collection)
      |> Seq.map (fun model -> model.Id, model.Metadata)
      |> dict
      |> json) next ctx
