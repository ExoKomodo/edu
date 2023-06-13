module Serializers

open Giraffe
open System
open System.Text.Json.Serialization
open System.IO
open System.Threading.Tasks

type JsonSerializer() =
  static member private Build () =
    let serializationOptions = SystemTextJson.Serializer.DefaultOptions
    serializationOptions
      .Converters
      .Add(JsonFSharpConverter(JsonUnionEncoding.FSharpLuLike))
    new SystemTextJson.Serializer(serializationOptions)

  member this.Serializer : Json.ISerializer = JsonSerializer.Build()
  
  interface Json.ISerializer with
    member this.Deserialize<'T> (data : array<byte>) : 'T =
      this.Serializer.Deserialize<'T>(data)
    member this.Deserialize<'T> (data : string) : 'T =
      this.Serializer.Deserialize<'T>(data)
    member this.DeserializeAsync<'T> (data : Stream) : Task<'T> =
      this.Serializer.DeserializeAsync<'T>(data)
    member this.SerializeToBytes<'T> (object : 'T) : array<byte> =
      this.Serializer.SerializeToBytes<'T>(object)
    member this.SerializeToStreamAsync<'T> (object : 'T) (stream : Stream) : Task =
      this.Serializer.SerializeToStreamAsync<'T> object stream
    member this.SerializeToString<'T> (object : 'T) : string =
      this.Serializer.SerializeToString<'T> object
