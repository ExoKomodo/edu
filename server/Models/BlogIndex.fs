module Models.BlogIndex

open Models
open System.Collections.Generic

type T =
  { Blogs: Dictionary<string, BlogMetadata.T> }
