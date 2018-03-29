using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Browser.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Browser.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/Script")]
    public class ScriptController : Controller
    {
        private readonly IBrowserService _browserService;

        public ScriptController(IBrowserService browserService)
        {
            _browserService = browserService;
        }

        [HttpPost("{url}")]
        public BrowserContent Post(string url, [FromBody]string script = "", int width = 1200, int height = 900)
        {
            var content = _browserService.GetContent(url, script, height, width);

            return content;
        }
    }
}
