module Exo.Lib.ActivePatterns

let (|StringPrefix|_|) (prefix : string) (str : string) =
  if str.StartsWith(prefix) then
    str.Substring(prefix.Length) |> Some
  else
    None
