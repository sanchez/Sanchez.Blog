namespace Sanchez.Blog.Services.Services

open System.IO
open Sanchez.Blog.Core.IServices

type BlogListService () =
    
    let rec loadBlogsFromDirectory (path: string) (prefix: string) =
        async {
            let files =
                Directory.GetFiles(path, "*.md")
                |> Array.map Path.GetFileName
                |> Array.map (fun x -> prefix + "/" + x)
                |> Array.toList
            let folders = Directory.GetDirectories(path)
            
            let! childrenFiles =
                folders
                |> Array.map (fun x -> loadBlogsFromDirectory x (prefix + "/" + (Path.GetFileName x)))
                |> Async.Parallel
                
            return
                [files]
                |> List.append (childrenFiles |> Array.toList)
                |> List.reduce List.append
        }
    
    interface IBlogListService with
        member this.GetFeaturedBlogs () =
            async {
                let fullBlogPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "blogs")
                return! loadBlogsFromDirectory fullBlogPath "blogs"
            }
            |> Async.StartAsTask