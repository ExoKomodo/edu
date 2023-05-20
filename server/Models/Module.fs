module Models

open Amazon
open Amazon.S3
open Constants
open System.Collections.Generic
open System.Text.Json.Serialization
open MongoDB.Driver
open System.Net.Http
open System
open System.Threading
open System.Net

[<CLIMutable>]
type BlogMetadata =
  { Description : string
    Title : string }

[<CLIMutable>]
type Blog =
  { Id : string
    Content : string
    Metadata : BlogMetadata }

[<CLIMutable>]
type BlogIndex =
  { Blogs : Dictionary<string, BlogMetadata> }

[<CLIMutable>]
type CourseMetadata =
  { Description : string
    Name : string }

[<CLIMutable>]
type Course =
  { Id : string
    Content : string
    Metadata : CourseMetadata }

[<CLIMutable>]
type CourseIndex =
  { Courses : Dictionary<string, CourseMetadata> }

type Dependencies =
  { Database : IMongoDatabase
    CourseCollection : IMongoCollection<Course>
    Auth0HttpClient : HttpClient
    S3Client : AmazonS3Client }
    
    static member InitializeMongo() =
      printfn "Initializing Mongo..."
      let connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
      match connectionString with
      | null ->
        printfn "You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable"
        exit 1
      | _ ->
        let client = new MongoClient(connectionString)
        let database = client.GetDatabase("admin")
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

    static member Open() =
      let database = Dependencies.InitializeMongo()
      let s3Client = Dependencies.ConnectToS3()
      { Database = database
        CourseCollection = database.GetCollection<Course>("courses")
        Auth0HttpClient = new HttpClient(
          BaseAddress = new Uri($"{auth0UrlScheme}{auth0BaseUrl}")
        )
        S3Client = s3Client }

[<CLIMutable>]
type UserInfo =
  { [<JsonPropertyName("email")>]
    Email : string
    [<JsonPropertyName("email_verified")>]
    IsEmailVerified : bool
    [<JsonPropertyName("nickname")>]
    Nickname : string
    [<JsonPropertyName("name")>]
    Name : string
    [<JsonPropertyName("picture")>]
    Picture : string
    [<JsonPropertyName("sub")>]
    Sub : string
    [<JsonPropertyName("updated_at")>]
    UpdatedAt : string }
