module Lib.Serializers

open Giraffe
open System.Text.Json.Serialization
open System.IO
open System.Threading.Tasks

type JsonSerializer() =
  let giraffeSerializer = JsonSerializer.BuildGiraffeSerializer()

  member public this.Deserialize<'T> (data : string) : 'T =
    giraffeSerializer.Deserialize<'T> data

  member public this.Serialize (object : obj) : string =
    giraffeSerializer.SerializeToString object

  static member private BuildGiraffeSerializer () : Json.ISerializer =
    let serializationOptions = SystemTextJson.Serializer.DefaultOptions
    serializationOptions
      .Converters
      .Add(JsonFSharpConverter(JsonUnionEncoding.FSharpLuLike))
    new SystemTextJson.Serializer(serializationOptions)

  interface Json.ISerializer with
    member this.Deserialize<'T> (data : array<byte>) : 'T =
      giraffeSerializer.Deserialize<'T>(data)

    member this.Deserialize<'T> (data : string) : 'T =
      giraffeSerializer.Deserialize<'T>(data)

    member this.DeserializeAsync<'T> (data : Stream) : Task<'T> =
      giraffeSerializer.DeserializeAsync<'T>(data)

    member this.SerializeToBytes<'T> (object : 'T) : array<byte> =
      giraffeSerializer.SerializeToBytes<'T>(object)

    member this.SerializeToStreamAsync<'T> (object : 'T) (stream : Stream) : Task =
      giraffeSerializer.SerializeToStreamAsync<'T> object stream

    member this.SerializeToString<'T> (object : 'T) : string =
      giraffeSerializer.SerializeToString<'T> object
