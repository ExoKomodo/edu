module Edu.Server.Api.V1.Blob

open Amazon.S3
open Edu.Server.Constants
open Giraffe
open Microsoft.AspNetCore.Http
open Amazon.S3.Model
open System
open Edu.Server.Models

let private _bindUrlForBlob (ctx : HttpContext) : string =
  let args = ctx.BindQueryString<BlobQueryArgs>()

  let rec _strip (url : string): string =
    if url.StartsWith("/") then
      _strip (url.Substring(1))
    else
      url

  if args.url = null then
    null
  else
    _strip args.url

let getPresignedUrl (s3Client : AmazonS3Client) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let url = _bindUrlForBlob ctx
    if url = null then
      RequestErrors.BAD_REQUEST "Url for blob was not provided" next ctx
    else
      let request = new GetPreSignedUrlRequest(
        BucketName = s3Bucket,
        Key = url,
        Expires = DateTime.UtcNow.AddHours(1)
      )
      // NOTE: GetPreSignedURL is a local calculation and does not require the the path exists.
      // This will return a url for an object URL that is not present.
      let response = s3Client.GetPreSignedURL(request)
      text response next ctx
