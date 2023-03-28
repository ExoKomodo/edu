open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.Extensions.DependencyInjection
open Routes
open System.Text.Json.Serialization

let webApp =
  subRoute "/api"
    (choose [
      subRoute "/v1"
        (choose [
          GET >=> choose [
            routex "(/?)" >=> Index.get
            route  "/ping" >=> Ping.get
            route  "/blog" >=> Blog.getAll
            routef  "/blog/%s" Blog.get
          ]
        ]) ])

let configureCors (builder : CorsPolicyBuilder) =
  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader() |> ignore

let configureServices (services : IServiceCollection) =
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
app.UseGiraffe webApp
app.UseCors configureCors |> ignore
app.Run()

type Program() = class end
