namespace Sanchez.Blog.Core.IServices

open Sanchez.Blog.Core.Models
open System.Threading.Tasks

type IBlogListService =
    abstract member GetFeaturedBlogs : unit -> Blog list Task
    abstract member GetRecentBlogs : unit -> Blog list Task
    abstract member ResolveBlogPath : string -> string