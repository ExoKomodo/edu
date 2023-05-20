module Api.V1.Blob

open Amazon.S3
open Constants
open Giraffe
open Microsoft.AspNetCore.Http
open Amazon.S3.Model
open System
open Microsoft.Extensions.Logging
open Models

let private _bindUrlForBlob (ctx : HttpContext) : string =
  let args = ctx.BindQueryString<BlobQueryArgs>()

  let rec _strip (url: string): string =
    if url.StartsWith("/") then
      _strip (url.Substring(1))
    else
      url
  
  _strip args.url

let getPresignedUrl (s3Client : AmazonS3Client) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let request = new GetPreSignedUrlRequest(
      BucketName = s3Bucket,
      Key = _bindUrlForBlob ctx,
      Expires = DateTime.UtcNow.AddHours(1)
    )
    let response = s3Client.GetPreSignedURL(request)
    let logger = ctx.GetLogger()
    logger.Log(LogLevel.Debug, response)
    text response next ctx
