namespace Browser.Core
{
    public class BrowserSettings
    {
        public string RunnerScriptPath { get; set; } = Constants.RunnerScriptPath;

        // Script Parameters
        public string OfflineCachedHtmlPath { get; set; }
        public string PageUrl { get; set; }
        public int WindowHeight { get; set; } = 800;
        public int WindowWidth { get; set; } = 1200;

        public string ParserScriptName { get; set; }

        public bool TakeScreenShot { get; set; } = true;
        public bool SaveRawHtml { get; set; } = true;
        public string UniqueId { get; set; }

        public string ScriptFolderPath { get; set; } = Constants.OutputTmpFolderPath;
        public string ScreenShotFolderName { get; set; } = Constants.ScreenShotFolderName;
        public string RawHtmlFolderName { get; set; } = Constants.RawHtmlFolderName;
        public string TempFolderPath { get; set; } = Constants.OutputTmpFolderPath;
        public bool IsDebugMode { get; set; } = true;

        public string DataDir { get; set; }

        public string ParsedDataFilePath => UniqueId + ".txt";
    }
}
