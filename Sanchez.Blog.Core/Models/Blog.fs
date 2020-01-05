namespace Sanchez.Blog.Core.Models

open System

type Blog (metadata: Map<string, string>, path: string) =
    let date = metadata |> Map.tryFind "date"
    
    let ConvertToDateTime (str: string) =
        match DateTime.TryParse str with
        | true, r -> Some r
        | _ -> None
    
    member this.Title =
        metadata
        |> Map.tryFind "title"
        |> Option.defaultValue "Invalid Title!"
        
    member this.Date =
        match date with
        | Some strDate -> ConvertToDateTime strDate
        | None -> None
        
    member this.IsFeatured =
        metadata
        |> Map.tryFind "featured"
        |> (fun x ->
            match x with
            | Some featured -> featured = "true"
            | None -> false)
        
    member this.Path =
        if path.EndsWith ".md" || path.EndsWith ".mdx" then
            path.Substring(0, (path.LastIndexOf '.'))
        else path