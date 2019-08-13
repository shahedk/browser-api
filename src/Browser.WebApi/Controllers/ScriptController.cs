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

        [HttpPost]
        public BrowserContent Post([FromBody] DataModel model)
        {
            var content = _browserService.GetContent(model.Url, model.Script, model.Height, model.Width);

            return content;
        }

        public class DataModel
        {
            public string Url { get; set; }
            public string Script { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

        }
    }
}
