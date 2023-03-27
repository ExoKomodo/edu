module Routes.Blog

open Models.Blog
open Giraffe

let blogs = [
  {
    Id = "1"
    Description = "A short first post"
    Title = "I am a blog"
    Content = "Lorem ipsum 1"
  }
  {
    Id = "2"
    Description = "A longer second post"
    Title = "A blog but again"
    Content = "Lorem ipsum 2"
  }
  {
    Id = "3"
    Description = "I cannot believe I am still doing this"
    Title = "One for each person of the trinity"
    Content = "Lorem ipsum 3"
  }
]
let defaultBlog = {
  Id = "-420"
  Description = "Oh no"
  Title = "Oh no"
  Content = "Whoopsie! You got a non-existent blog!"
}


let get (id: string) : HttpHandler =
  match blogs |> List.tryFind (fun x -> x.Id = id) with
  | Some blog -> json blog
  | None -> RequestErrors.NOT_FOUND $"Blog not found with id {id}"

let getAll : HttpHandler =
  json blogs
