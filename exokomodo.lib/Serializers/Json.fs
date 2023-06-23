module ExoKomodo.Lib.Serializers.Json

open Giraffe
open Microsoft.FSharp.Reflection
open Newtonsoft.Json
open Newtonsoft.Json.Serialization
open System
open System.IO
open System.Threading.Tasks

type OptionConverter() =
    // SOURCE: http://gorodinski.com/blog/2013/01/05/json-dot-net-type-converters-for-f-option-list-tuple/
    inherit JsonConverter()

    override __.CanConvert(t) = 
      t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<option<_>>

    override __.WriteJson(writer, value, serializer) =
      let value = 
        if value = null then null
        else 
          let _, fields = FSharpValue.GetUnionFields(value, value.GetType())
          fields.[0]  
      serializer.Serialize(writer, value)

    override __.ReadJson(reader, t, existingValue, serializer) =        
        let innerType = t.GetGenericArguments().[0]
        let innerType = 
            if innerType.IsValueType then (typedefof<Nullable<_>>).MakeGenericType([|innerType|])
            else innerType        
        let value = serializer.Deserialize(reader, innerType)
        let cases = FSharpType.GetUnionCases(t)
        if value = null then FSharpValue.MakeUnion(cases.[0], [||])
        else FSharpValue.MakeUnion(cases.[1], [|value|])

type Serializer() =
  let giraffeSerializer = Serializer.BuildGiraffeSerializer()

  member public __.Deserialize<'T> (data : string) : 'T =
    giraffeSerializer.Deserialize<'T> data

  member public __.Serialize<'T> (object : 'T) : string =
    giraffeSerializer.SerializeToString<'T> object

  static member private BuildGiraffeSerializer () : Json.ISerializer =
    let settings = JsonSerializerSettings(
      ContractResolver = CamelCasePropertyNamesContractResolver()
    )
    settings.Converters.Add(new OptionConverter())
    new NewtonsoftJson.Serializer(settings)

  interface Json.ISerializer with
    member __.Deserialize<'T> (data : array<byte>) : 'T =
      giraffeSerializer.Deserialize<'T>(data)

    member __.Deserialize<'T> (data : string) : 'T =
      giraffeSerializer.Deserialize<'T>(data)

    member __.DeserializeAsync<'T> (data : Stream) : Task<'T> =
      giraffeSerializer.DeserializeAsync<'T>(data)

    member __.SerializeToBytes<'T> (object : 'T) : array<byte> =
      giraffeSerializer.SerializeToBytes<'T>(object)

    member __.SerializeToStreamAsync<'T> (object : 'T) (stream : Stream) : Task =
      giraffeSerializer.SerializeToStreamAsync<'T> object stream

    member __.SerializeToString<'T> (object : 'T) : string =
      giraffeSerializer.SerializeToString<'T> object

  interface Jose.IJsonMapper with
    member this.Parse<'T>(data: string): 'T =
      this.Deserialize<'T> data

    member this.Serialize(object: obj): string = 
      this.Serialize object


// TODO: Write a dictionary converter - https://github.com/dvsekhvalnov/jose-jwt/blob/cb426b83e1e2010cdb7b9c062532ce1b27905f71/jose-jwt/json/NewtonsoftMapper.cs#L9
