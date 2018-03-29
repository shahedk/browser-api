using System.IO;

namespace Browser.Core
{
    public class BrowserService : IBrowserService
    {
        public BrowserContent GetRawHtml(string url, string script, int height = 900, int width = 1200)
        {
            return GetContent(url, script, height, width, false, true);
        }

        public BrowserContent GetRawHtml(string url, int height = 900, int width = 1200)
        {
            return GetContent(url, height, width, false, true);
        }

        public BrowserContent TakeScreenShot(string url, string script, int height = 900, int width = 1200)
        {
            return GetContent(url, script, height, width, true);
        }

        public BrowserContent TakeScreenShot(string url, int height = 900, int width = 1200)
        {
            return GetContent(url, height, width, true);
        }

        public BrowserContent GetContent(string url, int height = 900, int width = 1200, bool takeScreenshot = false,
            bool saveRawHtml = false)
        {
            return GetContent(url, null, height, width, takeScreenshot, saveRawHtml);
        }

        public BrowserContent GetContent(string url, string script, int height = 900, int width = 1200, bool takeScreenshot = false,
            bool saveRawHtml = false)
        {

            var browser = new PhantomJsBrowser();
            var settings = new BrowserSettings()
            {
                TakeScreenShot = false,
                SaveRawHtml = true,
                PageUrl = "https://shahed.me"
            };

            // When no script is provided by user, use the default script
            if (string.IsNullOrEmpty(script) || string.IsNullOrWhiteSpace(script))
            {
                settings.ParserScriptName = "EmptyScript.js";
            }
            else
            {
                settings.ParserScriptName = settings.UniqueId + ".js";
            }
            

            // Create script file and save script content there
            var executingFolder = Directory.GetCurrentDirectory();
            var scriptDestPath = Path.Combine(executingFolder, Constants.OutputFolderName, Constants.ScriptFolderName, settings.ParserScriptName);
            File.WriteAllText(scriptDestPath, script);

            // Load page into browser, execute script and get content
            var content = browser.GetContent(settings);

            return content;
        }
    }
}
