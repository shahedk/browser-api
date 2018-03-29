using System;
using System.Collections.Generic;
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

        [HttpGet("{url}")]
        public FileResult Get(string url, string script = "", int width=1200, int height=900)
        {
            var content = _browserService.TakeScreenShot(url, script, height, width);

            return PhysicalFile(content.ScreenShotPath, "image/png", "screenshot.png");
        }
    }
}
