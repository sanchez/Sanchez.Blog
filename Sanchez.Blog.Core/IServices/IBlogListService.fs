namespace Sanchez.Blog.Core.IServices
open System.Threading.Tasks

type IBlogListService =
    abstract member GetFeaturedBlogs : unit -> string list Task