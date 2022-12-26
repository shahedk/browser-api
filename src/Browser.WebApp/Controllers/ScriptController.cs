using Browser.Core;
using Browser.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Browser.WebApp.Controllers
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
        public BrowserContent Post([FromBody] BrowserDataModel model)
        {
            var content = _browserService.GetContent(model.Url, model.Script, model.Height, model.Width);

            return content;
        }

    }
}
