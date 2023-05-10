module Models

open System.Collections.Generic

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
