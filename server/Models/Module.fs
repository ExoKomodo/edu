module Models

open System.Collections.Generic
open System.Text.Json.Serialization

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

[<CLIMutable>]
type CourseMetadata =
  { Description: string
    Name: string }

[<CLIMutable>]
type Course =
  { Id: string
    Content: string
    Metadata: CourseMetadata }

[<CLIMutable>]
type CourseIndex =
  { Courses: Dictionary<string, CourseMetadata> }

[<CLIMutable>]
type UserInfo =
  { [<JsonPropertyName("email")>]
    Email: string
    [<JsonPropertyName("email_verified")>]
    IsEmailVerified: bool
    [<JsonPropertyName("nickname")>]
    Nickname: string
    [<JsonPropertyName("name")>]
    Name: string
    [<JsonPropertyName("picture")>]
    Picture: string
    [<JsonPropertyName("sub")>]
    Sub: string
    [<JsonPropertyName("updated_at")>]
    UpdatedAt: string }
