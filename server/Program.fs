open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Routes
open System.Text.Json
open System.Text.Json.Serialization

let webApp =
  subRoute "/api"
    (choose [
      subRoute "/v1"
        (choose [
          GET >=> choose [
            routex "(/?)" >=> Index.get
            route  "/ping" >=> Ping.get
          ]
        ]) ])

let configureServices (services : IServiceCollection) =
  services
    .AddGiraffe()
  |> ignore

  let serializationOptions = SystemTextJson.Serializer.DefaultOptions
  serializationOptions.Converters.Add(JsonFSharpConverter(JsonUnionEncoding.FSharpLuLike))
  services.AddSingleton<Json.ISerializer>(SystemTextJson.Serializer(serializationOptions)) |> ignore

let builder = WebApplication.CreateBuilder()

configureServices builder.Services

let app = builder.Build()
app.UseGiraffe webApp
app.Run()

type Program() = class end
