module Api.V1.Video

open Amazon.S3
open Constants
open Giraffe
open Microsoft.AspNetCore.Http
open Amazon.S3.Model
open System

let getPresignedUrl (s3Client : AmazonS3Client) (id : string) : HttpHandler =
  fun (next : HttpFunc) (ctx : HttpContext) ->
    let request = new GetPreSignedUrlRequest(
      BucketName = s3Bucket,
      Key = id,
      Expires = DateTime.UtcNow.AddHours(1)
    )
    let response = s3Client.GetPreSignedURL(request)
    text response next ctx
