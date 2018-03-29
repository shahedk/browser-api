using System;
using System.IO;
using Xunit;

namespace Browser.Core.Test
{
    public class ScriptingTest
    {
        [Fact]
        public void ScrapData_With_jQuery()
        {
            //var executingFolder = Directory.GetCurrentDirectory();


            var browser = new PhantomJsBrowser();
            var settings = new BrowserSettings()
            {
                TakeScreenShot = false,
                SaveRawHtml = true,
                PageUrl = "https://shahed.me"
            };
            settings.ParserScriptName = settings.UniqueId + ".js";


            var scriptPath = Path.Combine("Test_Data", "Scripts", "page-info.js");
            var script = File.ReadAllText(scriptPath);


            var scriptDestPath = Path.Combine(Constants.OutputFolderPath, Constants.ScriptFolderName, settings.ParserScriptName);
            File.WriteAllText(scriptDestPath, script);

            var content = browser.GetContent(settings);

            Assert.True(string.IsNullOrEmpty(content.Error));

            
        }
    }
}
