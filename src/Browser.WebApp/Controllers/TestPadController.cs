using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Browser.WebApp.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public class TestPadController : Controller
    {
        [Route("/")]
        public ActionResult Index()
        {
            return RedirectToAction("Screenshot");
        }

        
        [Route("/TestPad/Screenshot")]
        public ActionResult Screenshot()
        {
            return View();
        }
        
        
        [Route("/TestPad/RawHtml")]
        public ActionResult RawHtml()
        {
            return View();
        }
        
        [Route("/TestPad/Scripting")]
        public ActionResult Scripting()
        {
            return View();
        }
    }
}