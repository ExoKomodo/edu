module Serializers

open Giraffe
open System
open System.Text.Json.Serialization
open System.IO
open System.Threading.Tasks

[<CLIMutable>]
type JsonWebTokenHeader =
  { [<JsonPropertyName("typ")>]
    Type : string
    [<JsonPropertyName("cty")>]
    ContentType : string
    [<JsonPropertyName("alg")>]
    Algorithm : string
    [<JsonPropertyName("kid")>]
    KeyId : string
    [<JsonPropertyName("x5u")>]
    X5u : string
    [<JsonPropertyName("x5c")>]
    X5c : string
    [<JsonPropertyName("x5t")>]
    X5t : string }

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

  interface JWT.IJsonSerializer with
    member this.Serialize (obj : obj) : string =
      this.Serializer.SerializeToString(obj)

    member this.Deserialize(_ : Type, data : string) : obj =
      this.Serializer.Deserialize(data)
