module Api.V1.Course

open Giraffe
open Helpers
open Microsoft.AspNetCore.Http
open Models
open MongoDB.Driver

let private _getCollection (database: IMongoDatabase) =
  database.GetCollection<Course>("courses")

let private _getCourses (database: IMongoDatabase) =
  (_getCollection database).Find(Builders<Course>.Filter.Empty).ToEnumerable()
  |> Seq.cast<Course>

let private _getCourse (database: IMongoDatabase) (id: string) =
  let filter = Builders<Course>.Filter.Eq("Id", id)
  (_getCollection database).Find(filter).FirstOrDefault()

let private _getInFormat (formatter: Course -> HttpFunc -> HttpContext -> HttpFuncResult) (database: IMongoDatabase) (id: string) : HttpHandler =
  let course = _getCourse database id
  match box course with
  | null -> RequestErrors.NOT_FOUND $"Course not found with id {id}"
  | _ -> formatter course

let private _getAsXml (database: IMongoDatabase) (id: string) : HttpHandler = _getInFormat xml database id

let private _getAsJson (database: IMongoDatabase) (id: string) : HttpHandler = _getInFormat json database id

let get (database: IMongoDatabase) (id: string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let token = decodeJwtFromHeader ctx
    match token with
    | Some decodedToken -> 
      let accept =
        match ctx.TryGetRequestHeader "Accept" with
        | None -> "application/json"
        | Some value -> value

      match accept with
      | StringPrefix "application/xml" _ | StringPrefix "text/xml" _ -> _getAsXml database id next ctx
      | _ -> _getAsJson database id next ctx
    | None -> RequestErrors.FORBIDDEN "Not allowed bro!" next ctx

let getAllMetadata (database: IMongoDatabase) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let token = decodeJwtFromHeader ctx
    match token with
    | Some decodedToken ->
      (
        (_getCourses database)
        |> Seq.map (fun course -> course.Id, course.Metadata)
        |> dict
        |> json) next ctx
    | None -> RequestErrors.FORBIDDEN "Not allowed bro!" next ctx
