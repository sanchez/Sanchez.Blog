using System.IO;
using LibSassHost;
using Microsoft.AspNetCore.Mvc;

namespace Sanchez.Blog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SassController : Controller
    {
        [Route("{fileName}")]
        public ContentResult SassConverter(string fileName)
        {
            var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sass", $"{fileName}.scss");
            var result = SassCompiler.CompileFile(fullFilePath, options: new CompilationOptions()
            {
                SourceMap = false,
                OutputStyle = OutputStyle.Compact,
                SourceComments = false,
            });

            return Content(result.CompiledContent, "text/css");
        }
    }
}