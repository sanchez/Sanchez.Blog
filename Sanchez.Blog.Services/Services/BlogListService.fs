namespace Sanchez.Blog.Services.Services

open System.IO
open Sanchez.Blog.Core.IServices
open Sanchez.Markdown.Parser
open Sanchez.Blog.Core.Models

type BlogListService () =
    
    let loadBlogData (path: string) =
        async {
            let! data = File.ReadAllTextAsync path |> Async.AwaitTask
            return MarkdownParser.ParseMetadata data
        }
    
    let rec loadBlogsFromDirectory (path: string) (prefix: string) =
        async {
            let! files =
                Directory.GetFiles(path, "*.md")
                |> Array.map (fun x ->
                    async {
                        let! metadata = loadBlogData x
                        let fileName = prefix + "/" + Path.GetFileName (x)
                        return Blog (metadata, fileName)
                    })
                |> Async.Parallel
            
            let folders = Directory.GetDirectories(path)
            
            let! childrenFiles =
                folders
                |> Array.map (fun x -> loadBlogsFromDirectory x (prefix + "/" + (Path.GetFileName x)))
                |> Async.Parallel
                
            return
                [files
                 |> Array.toList]
                |> List.append (childrenFiles |> Array.toList)
                |> List.reduce List.append
        }
    
    interface IBlogListService with
        member this.GetFeaturedBlogs () =
            async {
                let fullBlogPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "blogs")
                let! blogs = loadBlogsFromDirectory fullBlogPath "blog"
                
                return query {
                    for blog in blogs do
                        where blog.IsFeatured
                        sortByDescending blog.Date
                        take 10
                }
                |> Seq.toList
            }
            |> Async.StartAsTask
            
        member this.GetRecentBlogs () =
            async {
                let fullBlogPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "blogs")
                let! blogs = loadBlogsFromDirectory fullBlogPath "blog"
                
                return query {
                    for blog in blogs do
                        sortByDescending blog.Date
                        take 10
                }
                |> Seq.toList
            }
            |> Async.StartAsTask
            
        member this.ResolveBlogPath blogName =
            let completeName = blogName + ".md"
            let fullBlogPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "blogs")
            Path.Combine(fullBlogPath, completeName)