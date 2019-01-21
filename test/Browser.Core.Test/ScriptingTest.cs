using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Browser.Core.Test
{
    public class ScriptingTest
    {
        [Fact]
        public void ScrapData_With_jQuery()
        {
            var browser = new PhantomJsBrowser();
            var settings = new BrowserSettings()
            {
                TakeScreenShot = false,
                SaveRawHtml = true,
                PageUrl = "https://shahed.ca"
            };
            settings.ParserScriptName = settings.UniqueId + ".js";


            var scriptPath = Path.Combine("Test_Data", "Scripts", "page-info.js");
            var script = File.ReadAllText(scriptPath);


            var scriptDestPath = Path.Combine(Constants.OutputFolderPath, Constants.ScriptFolderName, settings.ParserScriptName);
            File.WriteAllText(scriptDestPath, script);

            var content = browser.GetContent(settings);

            var scanData = JsonConvert.DeserializeObject<JObject>(content.ParsedData);

            var actual = scanData["data"][0]["title"];
            var expected = "Test title";

            Assert.Equal(expected, actual);

            Assert.True(string.IsNullOrEmpty(content.Error));

            
        }
    }
}
