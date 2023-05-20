module Api.V1.Blob

open Amazon.S3
open Constants
open Giraffe
open Microsoft.AspNetCore.Http
open Amazon.S3.Model
open System
open System.IO
open Microsoft.Extensions.Logging

let get (s3Client : AmazonS3Client) (id : string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let request = new GetObjectRequest(
      BucketName = s3Bucket,
      Key = id
    )
    let response = s3Client.GetObjectAsync(request) |> Async.AwaitTask |> Async.RunSynchronously
    let stream = response.ResponseStream
    let data = stream.AsyncRead (stream.Length |> int) |> Async.RunSynchronously
    let reader = new StreamReader(new MemoryStream(data))
    text (reader.ReadToEnd()) next ctx

let getPresignedUrl (s3Client : AmazonS3Client) (id : string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let request = new GetPreSignedUrlRequest(
      BucketName = s3Bucket,
      Key = id,
      Expires = DateTime.UtcNow.AddHours(1)
    )
    let response = s3Client.GetPreSignedURL(request)
    let logger = ctx.GetLogger()
    logger.Log(LogLevel.Debug, response)
    text response next ctx
