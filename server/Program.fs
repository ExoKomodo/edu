open Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Models
open System.Net.Http
open System.Text.Json.Serialization

let dependencies = Dependencies.Open()

let webApp = (choose [
    GET >=>
      routex "(/?)" >=> Index.get
      subRoute "/api" (choose [
          GET >=>
            routex "(/?)" >=> Api.Index.get
          subRoute "/v1" (choose [
            routex "/blog(/?)(.*)" >=> (choose [
              routex  "/blog(/?)" >=> Api.V1.Blog.getAll
              routef  "/blog/%s" Api.V1.Blog.get
            ])
            routex "/course(/?)(.*)" >=>
              (Helpers.mustBePaidUsersOrHigher dependencies.Auth0HttpClient) >=>
              (choose [
                GET >=>
                  (choose [
                    routex  "/course(/?)" >=> Api.V1.Course.getAllMetadata dependencies.CourseCollection
                    routef  "/course/%s"  (Api.V1.Course.get dependencies.CourseCollection)
                  ])
                DELETE >=>
                  routef  "/course/%s" (Api.V1.Course.delete dependencies.CourseCollection)
                POST >=>
                  routex "/course(/?)" >=>
                  bindJson<Course> (Api.V1.Course.post dependencies.CourseCollection)
                PUT >=>
                  routex "/course(/?)" >=>
                  bindJson<Course> (Api.V1.Course.put dependencies.CourseCollection)
              ])
            routex "/user(/?)(.*)" >=>
              (choose [
                routex  "/user/info(/?)" >=> Api.V1.User.getInfo dependencies.Auth0HttpClient
              ])
            routex "/blob(/?)(.*)" >=>
              (Helpers.mustBePaidUsersOrHigher dependencies.Auth0HttpClient) >=>
              (choose [
                GET >=>
                  (choose [
                    // TODO: Grab route from query arg
                    routex  "/blob(/?)" >=> (Api.V1.Blob.getPresignedUrl dependencies.S3Client)
                  ])
              ])
            GET >=>
              routex "(/?)" >=> Api.V1.Index.get
          ])])])

let configureCors (builder : CorsPolicyBuilder) =
  builder
    .WithOrigins(
      // NOTE: Development client
      "http://localhost:5173",
      "http://127.0.0.1:5173",
      // NOTE: Development server
      "http://localhost:5000",
      // NOTE: Production client
      "https://edu.exokomodo.com",
      // NOTE: Production server
      "https://services.edu.exokomodo.com"
    )
    .AllowAnyMethod()
    .AllowAnyHeader() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    let filter (level : LogLevel) = level.Equals LogLevel.Error
    builder.AddFilter(filter)
           .AddConsole()
           .AddDebug()
    |> ignore

let configureServices (services : IServiceCollection) =
  services
    .AddAuthentication(
      fun options ->
        options.DefaultAuthenticateScheme <- JwtBearerDefaults.AuthenticationScheme
        options.DefaultChallengeScheme <- JwtBearerDefaults.AuthenticationScheme
    )
    .AddJwtBearer(
      fun options ->
        options.Authority <- $"https://exokomodo.us.auth0.com/"
        options.Audience <- "https://services.edu.exokomodo.com"
    )
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
// configureLogging builder.Logging

let app = builder.Build()
// NOTE: Order matters. CORS must be configured before starting Giraffe.
app.UseAuthentication() |> ignore
app.UseCors configureCors |> ignore
app.UseGiraffe webApp
app.Run()

type Program() = class end
