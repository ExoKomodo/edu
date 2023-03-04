open Giraffe
open Microsoft.AspNetCore.Builder
open Routes

let webApp =
  subRoute "/api"
    (choose [
      subRoute "/v1"
        (choose
          GET >=> choose [
            routex "(/?)" >=> Index.get
            route  "/ping" >=> Ping.get
          ]) ])

let app = WebApplication.CreateBuilder().Build()
app.UseGiraffe webApp
app.Run()

type Program() = class end
