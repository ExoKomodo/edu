module Edu.Server.Models

open Amazon.S3
open Edu.Server.Constants
open MongoDB.Driver
open System
open System.Collections.Generic
open System.Net
open System.Net.Http
open System.Threading

type ExoId = string

type IDatabaseModelMetadata = 
  abstract Description : string
  abstract Name : string

type IDatabaseModel = 
  abstract Id : ExoId
  abstract Metadata : IDatabaseModelMetadata

[<CLIMutable>]
type CourseMetadata =
  { Description : string
    Name : string }
  interface IDatabaseModelMetadata with
    member x.Description = x.Description
    member x.Name = x.Name

[<CLIMutable>]
type Course =
  { Id : ExoId
    Content : string
    Metadata : CourseMetadata }
  interface IDatabaseModel with
    member x.Id = x.Id
    member x.Metadata = x.Metadata

[<CLIMutable>]
type CourseIndex =
  { Courses : Dictionary<string, CourseMetadata> }

[<CLIMutable>]
type SectionMetadata =
  { Description : string
    Name : string
    CourseId : ExoId }
  interface IDatabaseModelMetadata with
    member x.Description = x.Description
    member x.Name = x.Name

[<CLIMutable>]
type Section =
  { Id : ExoId
    Content : string
    Difficulty : uint
    Metadata : SectionMetadata }
  interface IDatabaseModel with
    member x.Id = x.Id
    member x.Metadata = x.Metadata

[<CLIMutable>]
type AssignmentMetadata =
  { Description : string
    Name : string
    RequiredSectionIds : ExoId[]
    CourseId : ExoId }
  interface IDatabaseModelMetadata with
    member x.Description = x.Description
    member x.Name = x.Name

[<CLIMutable>]
type Assignment =
  { Id : ExoId
    ProblemExplanation : string
    Metadata : AssignmentMetadata }
  interface IDatabaseModel with
    member x.Id = x.Id
    member x.Metadata = x.Metadata

[<CLIMutable>]
type BlobQueryArgs =
  { url: string }

[<CLIMutable>]
type BlogMetadata =
  { Description : string
    Title : string }

[<CLIMutable>]
type Blog =
  { Id : ExoId
    Content : string
    Metadata : BlogMetadata }

[<CLIMutable>]
type BlogIndex =
  { Blogs : Dictionary<string, BlogMetadata> }

type Dependencies =
  { Database : IMongoDatabase
    AssignmentCollection : IMongoCollection<Assignment>
    CourseCollection : IMongoCollection<Course>
    SectionCollection : IMongoCollection<Section>
    Auth0HttpClient : HttpClient
    S3Client : AmazonS3Client
    UpdateAssignment : Assignment -> UpdateDefinition<Assignment>
    UpdateCourse : Course -> UpdateDefinition<Course>
    UpdateSection : Section -> UpdateDefinition<Section> }
    
    static member InitializeMongo() =
      printfn "Initializing Mongo..."
      let connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
      match connectionString with
      | null ->
        printfn "You must set your 'MONGODB_URI' environment variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable"
        exit 1
      | _ ->
        let databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE")
        match databaseName with
        | null ->
          printfn "You must set your 'MONGODB_DATABASE' environment variable."
          exit 1
        | _ ->
          let client = new MongoClient(connectionString)
          let database = client.GetDatabase(databaseName)
          printfn "Initialized Mongo!"
          database

    static member TestS3Connection(client : AmazonS3Client): bool =
      let buckets = client.ListBucketsAsync(new CancellationToken()) |> Async.AwaitTask |> Async.RunSynchronously
      // NOTE: 200 <= status < 300
      HttpStatusCode.OK <= buckets.HttpStatusCode && buckets.HttpStatusCode < HttpStatusCode.Ambiguous

    static member ConnectToS3() =
      printfn "Connecting to S3..."
      let config = new AmazonS3Config(
        ServiceURL = s3Endpoint
      )
      let s3Client = new AmazonS3Client(config)
      assert Dependencies.TestS3Connection s3Client
      printfn "Connected to S3!"
      s3Client
    
    static member GenerateUpdateAssignment (assignment : Assignment) =
      let mutable update = Builders<Assignment>.Update.Set(
        (fun _assignment -> _assignment.ProblemExplanation),
        assignment.ProblemExplanation
      )
      update <- update.Set(
        (fun _assignment -> _assignment.Metadata),
        assignment.Metadata
      )
      update
    
    static member GenerateUpdateCourse (course : Course) =
      let mutable update = Builders<Course>.Update.Set(
        (fun _course -> _course.Content),
        course.Content
      )
      update <- update.Set(
        (fun _course -> _course.Metadata),
        course.Metadata
      )
      update
    
    static member GenerateUpdateSection (section : Section) =
      let mutable update = Builders<Section>.Update.Set(
        (fun _section -> _section.Difficulty),
        section.Difficulty
      )
      update <- update.Set(
        (fun _section -> _section.Metadata),
        section.Metadata
      )
      update

    static member Open() =
      let database = Dependencies.InitializeMongo()
      let s3Client = Dependencies.ConnectToS3()
      { Database = database
        AssignmentCollection = database.GetCollection<Assignment>("assignments")
        CourseCollection = database.GetCollection<Course>("courses")
        SectionCollection = database.GetCollection<Section>("sections")
        Auth0HttpClient = new HttpClient(
          BaseAddress = new Uri($"{auth0UrlScheme}{auth0BaseUrl}")
        )
        S3Client = s3Client
        UpdateAssignment = Dependencies.GenerateUpdateAssignment
        UpdateCourse = Dependencies.GenerateUpdateCourse
        UpdateSection = Dependencies.GenerateUpdateSection }
