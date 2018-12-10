using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Browser.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Browser.WebApi.Controllers
{
    [Route("/Screenshot")]
    public class ScreenShotController : Controller
    {
        private readonly IBrowserService _browserService;

        public ScreenShotController( IBrowserService browserService)
        {
            _browserService = browserService;
        }

        //[Route("/Screenshot")]
        [HttpPost]
        public BrowserContent Post(string url, string script = "", int width=1200, int height=900)
        {
            var content = _browserService.TakeScreenShot(url, script, height, width);
            return content;
        }

        
        [HttpGet]
        public ActionResult Get(string fileName)
        {
            try
            {
                var filePath = Path.Combine(Constants.OutputFolderPath, fileName);
                return PhysicalFile( filePath, "image/png", "screenshot.png");
            }
            catch
            {
                return NotFound("File not found");
            }
            
        }
    }
}
