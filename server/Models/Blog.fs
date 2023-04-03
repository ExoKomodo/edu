module Models.Blog

open Models

type T =
  { Id: string
    Content: string
    Metadata: BlogMetadata.T }
