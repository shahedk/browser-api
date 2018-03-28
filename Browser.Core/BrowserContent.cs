using System.IO;

namespace Browser.Core
{
    public class BrowserContent
    {
        public string UniqueId { get; private set; }
        public string ScreenShotPath { get; private set; }
        public string RawHtmlPath { get; private set; }

        public string ConsoleOutput { get; set; }
        public string ParsedData { get; set; }
        public string Error { get; set; }


        public BrowserContent(string uniqueId, bool screenShotTaken, bool rawHtmlSaved)
        {
            UniqueId = uniqueId;

            if (screenShotTaken)
            {
                ScreenShotPath = Path.Combine(Constants.ScreenShotFolderName, uniqueId + ".png") ;
            }

            if (rawHtmlSaved)
            {
                RawHtmlPath = Path.Combine(Constants.RawHtmlFolderPath, uniqueId + ".html");
            }
        }
    }
}
