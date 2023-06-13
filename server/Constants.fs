module Constants

[<Literal>]
let auth0BaseUrl = "exokomodo.us.auth0.com"
[<Literal>]
let auth0UrlScheme = "https://"
[<Literal>]
let auth0ClientId = "d0nbGyYvhTxPjyL1eaa3K4ojLDUNt1LX"
[<Literal>]
let auth0ServerClientId = "NWnLqtU89JR6drCGaabYygA2zUUQ7woG"

[<Literal>]
let s3Region = "sfo3"
let s3Endpoint = $"https://{s3Region}.digitaloceanspaces.com"
let s3CdnEndpoint = $"https://{s3Region}.cdn.digitaloceanspaces.com"
[<Literal>]
let s3Bucket = $"edu.exokomodo"
