# Template Blazor Client

TODO: Description

## Important Links
- [Github](https://github.com/ExoKomodo/template-repo-blazor-client)
- [Client - Production](https://example.exokomodo.com)
- [Dotnet Installation Page](https://dotnet.microsoft.com/download)

## Setup
Download [.NET](https://dotnet.microsoft.com/download) and install the appropriate .NET version.

Setup the project
```bash
dotnet restore
```
Run the front-end client in a terminal window of its own (2 options):
```bash
dotnet watch run --project src/Client
```
```bash
cd src/Client
dotnet watch run
```

## Special Blazor File Types
- Razor Pages [`.razor`]
    - Superset of HTML
    - Supports inline code directives and HTML generation
    - Supports component-based front-end patterns
    - Can include all the C# or CSS code for a page as well
- Code-Behind fIles [`.razor.cs`] (Optional)
    - A page in Blazor is represented by one class in the end compilation, so a Code-Behind file can contain a partial class for the Razor page, containing the code that would have been present in a `@code` block
    - Allows definition/redefinition of page lifecycle hooks such as `OnInitialized`, `OnAfterRender`, etc. 
- Scoped CSS file [`.razor.css`] (Optional)
    - Contains CSS that is scoped to the page/component that it represents
    - Isolates the CSS definitions for a page/component to the associated page/component
    - Should be preferred over using `.css` files whenever possible, importing `.css` files that are needed
