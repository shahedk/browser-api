using System;

namespace Browser.Core
{
    public class BrowserSettings
    {
        public string RunnerScriptPath { get; set; } = Constants.RunnerScriptPath;

        // Script Parameters
        public string PageUrl { get; set; }
        public int WindowHeight { get; set; } = 800;
        public int WindowWidth { get; set; } = 1200;

        public string ParserScriptName { get; set; } = "EmptyScript.js";

        public bool TakeScreenShot { get; set; } = true;
        public bool SaveRawHtml { get; set; } = true;

        public string UniqueId { get; set; } = Guid.NewGuid().ToString().Replace("-", "");

        public bool IsDebugMode { get; set; } = true;

        public string ParsedDataFilePath => UniqueId + ".txt";
    }
}
