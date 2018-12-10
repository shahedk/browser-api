using System.IO;
using Xunit;

namespace Browser.Core.Test
{
    public class PhantomJsBrowserTest
    {
        [Fact]
        public void GetRawHtml()
        {
            var browser = new PhantomJsBrowser();
            var settings = new BrowserSettings()
            {
                TakeScreenShot =  false,
                SaveRawHtml = true,
                ParserScriptName = "EmptyScript.js",
                PageUrl = "https://shahed.ca"
            };


            var content = browser.GetContent(settings);

            Assert.True(string.IsNullOrEmpty(content.Error));

            var executingFolder = Directory.GetCurrentDirectory();
            var path = Path.Combine(executingFolder, content.RawHtmlPath);
            var imgFile = new FileInfo(path);

            Assert.Equal(imgFile.Name, $"{settings.UniqueId}.html");
        }

        [Fact]
        public void GetScreenShot()
        {
            var browser = new PhantomJsBrowser();
            var settings = new BrowserSettings()
            {
                TakeScreenShot = true,
                SaveRawHtml = false,
                ParserScriptName = "EmptyScript.js",
                PageUrl = "https://shahed.me"
            };


            var content = browser.GetContent(settings);

            Assert.True(string.IsNullOrEmpty(content.Error));

            var executingFolder = Directory.GetCurrentDirectory(); 
            var path = Path.Combine(executingFolder, content.ScreenShotPath);
            var imgFile = new FileInfo(path);

            Assert.Equal(imgFile.Name, $"{settings.UniqueId}.png");
        }
    }
}
