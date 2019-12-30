namespace Sanchez.Blog.Core.Models

type Blog (metadata: Map<string, string>, path: string) =
    member this.Title =
        metadata
        |> Map.tryFind "title"
        |> Option.defaultValue "Invalid Title!"
        
    member this.Path =
        if path.EndsWith ".md" || path.EndsWith ".mdx" then
            path.Substring(0, (path.LastIndexOf '.'))
        else path