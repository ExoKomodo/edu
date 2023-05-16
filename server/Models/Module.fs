module Models

open Constants
open System.Collections.Generic
open System.Text.Json.Serialization
open MongoDB.Driver
open System.Net.Http
open System

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
  { database: IMongoDatabase
    courseCollection: IMongoCollection<Course>
    auth0HttpClient: HttpClient }
    
    static member InitializeMongo() =
      let connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
      match connectionString with
      | null ->
        printfn "You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable"
        exit 1
      | _ ->
        let client = new MongoClient(connectionString)
        client.GetDatabase("admin")

    static member Open() =
      let database = Dependencies.InitializeMongo()
      { database = database
        courseCollection = database.GetCollection<Course>("courses")
        auth0HttpClient = new HttpClient(
          BaseAddress = new Uri($"{auth0UrlScheme}{auth0BaseUrl}")
        ) }

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
