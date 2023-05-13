open Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.Extensions.DependencyInjection
open MongoDB.Driver
open System.Text.Json.Serialization
open System

// NOTE: Initialize Mongo

let initializeMongo () =
  let connectionString = Environment.GetEnvironmentVariable("MONGODB_URI")
  match connectionString with
  | null ->
    printfn "You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable"
    exit 1
  | _ ->
    let client = new MongoClient(connectionString)
    let database = client.GetDatabase("admin")
    database

let database = initializeMongo()

// TODO: Need to add headers in a middleware that verifies the JWT
let decodeJwt (token: string) =
  if token = "" then
    ""
  else
    token
    // let json = JwtBuilder.Create().MustVerifySignature().Decode(token)
  
    // printfn "%O" json;
    // json

let webApp =
  (choose
  [
    GET >=>
      routex "(/?)" >=> Index.get
    subRoute "/api" (choose
      [
        GET >=>
          routex "(/?)" >=> Api.Index.get
        subRoute "/v1" (choose
          [
            GET >=> (choose
              [
                routex "(/?)" >=> Api.V1.Index.get
                routex  "/blog(/?)" >=> Api.V1.Blog.getAll
                routef  "/blog/%s" Api.V1.Blog.get
                Helpers.mustBeLoggedIn >=> (choose
                  [
                    routex  "/course(/?)" >=> Api.V1.Course.getAllMetadata database
                    routef  "/course/%s" (Api.V1.Course.get database)
                  ]
                )
              ]
            )
          ]
        )
      ]
    )
  ]
)

let configureCors (builder : CorsPolicyBuilder) =
  builder
    .WithOrigins(
      // NOTE: Development client
      "http://localhost:5173",
      // NOTE: Development server
      "http://localhost:5000",
      // NOTE: Production client
      "https://edu.exokomodo.com",
      // NOTE: Production server
      "https://services.edu.exokomodo.com"
    )
    .AllowAnyMethod()
    .AllowAnyHeader() |> ignore

let configureServices (services : IServiceCollection) =
  // var builder = WebApplication.CreateBuilder(args);
  // var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
  // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  // .AddJwtBearer(options =>
  // {
  //     options.Authority = domain;
  //     options.Audience = builder.Configuration["Auth0:Audience"];
  //     options.TokenValidationParameters = new TokenValidationParameters
  //     {
  //         NameClaimType = ClaimTypes.NameIdentifier
  //     };
  // });
  let auth0Audience = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID")
  services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
      fun options ->
        options.Authority <- $"https://exokomodo.us.auth0.com/"
        options.Audience <- auth0Audience
    )
  |> ignore
  services
    .AddAuthorization()
  |> ignore
  services
    .AddCors()
    .AddGiraffe()
  |> ignore

  let serializationOptions = SystemTextJson.Serializer.DefaultOptions
  serializationOptions.Converters.Add(JsonFSharpConverter(JsonUnionEncoding.FSharpLuLike))
  services.AddSingleton<Json.ISerializer>(SystemTextJson.Serializer(serializationOptions)) |> ignore

let builder = WebApplication.CreateBuilder()

configureServices builder.Services

let app = builder.Build()
// NOTE: Order matters. CORS must be configured before starting Giraffe.
app.UseAuthentication() |> ignore
app.UseAuthorization() |> ignore
app.UseCors configureCors |> ignore
app.UseGiraffe webApp
app.Run()

type Program() = class end
