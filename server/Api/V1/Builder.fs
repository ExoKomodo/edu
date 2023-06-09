module Api.V1.Builder

open Giraffe
open Helpers
open Microsoft.AspNetCore.Http
open Models
open MongoDB.Driver
open System.Threading

// NOTE: Builder helps build CRUD routes using type generics

// // Usage example:
// type Dependencies =
//   { AssignmentCollection : IMongoCollection<Assignment>
//     UpdateAssignment : Assignment -> UpdateDefinition<Assignment> }

//     static member GenerateUpdateAssignment (assignment : Assignment) =
//       let mutable update = Builders<Assignment>.Update.Set(
//         (fun _assignment -> _assignment.ProblemDescription),
//         assignment.ProblemDescription
//       )
//       update <- update.Set(
//         (fun _assignment -> _assignment.Metadata),
//         assignment.Metadata
//       )
//       update

//     static member Open() =
//       let database = Dependencies.InitializeMongo()
//       let s3Client = Dependencies.ConnectToS3()
//       { Database = database
//         AssignmentCollection = database.GetCollection<Assignment>("assignments")
//         CourseCollection = database.GetCollection<Course>("courses")
//         SectionCollection = database.GetCollection<Section>("sections")
//         Auth0HttpClient = new HttpClient(
//           BaseAddress = new Uri($"{auth0UrlScheme}{auth0BaseUrl}")
//         )
//         S3Client = s3Client
//         UpdateAssignment = Dependencies.GenerateUpdateAssignment
//         UpdateCourse = Dependencies.GenerateUpdateCourse
//         UpdateSection = Dependencies.GenerateUpdateSection }

// let dependencies = Dependencies.Open()

// let webApp =
//   routex "/assignment(/?)(.*)" >=>
//     (choose [
//       GET >=>
//         (choose [
//           routex  "/assignment(/?)" >=> Api.V1.Builder.getAllMetadata<Assignment> dependencies.AssignmentCollection
//           routef  "/assignment/%s"  (Api.V1.Builder.get<Assignment> dependencies.AssignmentCollection)
//         ])
//       DELETE >=>
//         routef  "/assignment/%s" (Api.V1.Builder.delete<Assignment> dependencies.AssignmentCollection)
//       POST >=>
//         routex "/assignment(/?)" >=>
//         bindJson<Assignment> (Api.V1.Builder.post<Assignment> dependencies.AssignmentCollection)
//       PUT >=>
//         routex "/assignment(/?)" >=>
//         bindJson<Assignment> (Api.V1.Builder.put<Assignment> dependencies.AssignmentCollection dependencies.UpdateAssignment)
//     ])

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

let inline updateModel<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (model : ^T) (update : ^T -> UpdateDefinition<^T>) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let filter = Builders<^T>.Filter.Eq("Id", model.Id)
    collection.UpdateOne(filter, update model, null, new CancellationToken()) |> ignore
    printfn "%O" model
    json model next ctx

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

let inline put<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) (update : ^T -> UpdateDefinition<^T>) (model : ^T) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    updateModel collection model update next ctx

let inline getAllMetadata<^T when ^T :> IDatabaseModel> (collection : IMongoCollection<^T>) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    ( (getModels collection)
      |> Seq.map (fun model -> model.Id, model.Metadata)
      |> dict
      |> json) next ctx
