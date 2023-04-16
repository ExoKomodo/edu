module Models

open System.Collections.Generic
open System.Xml.Serialization

[<CLIMutable>]
type BlogMetadata =
  { Description: string
    Title: string }

[<CLIMutable>]
type Blog =
  { Id: string
    Content: string
    Metadata: BlogMetadata }

[<CLIMutable>]
type BlogIndex =
  { Blogs: Dictionary<string, BlogMetadata> }
