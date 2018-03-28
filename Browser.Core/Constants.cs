using System.Runtime.InteropServices;

namespace Browser.Core
{
    public class Constants
    {
        public abstract class HttpHeaders
        {
            public const string ClientId = "clientid";
            public const string ApiKey = "apikey";
        }

        public enum BrowserTypes
        {
            WebBrowser = 1,
            OfflineBrowser = 2
        }

        public const string RawHtmlFolderName = "html";
        public const string ScreenShotFolderName = "screenshot";

        private const string _outputFolderPath = "App_Output";

        public static string RawHtmlFolderPath
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return OutputFolderPath + RawHtmlFolderName + "\\";
                }
                else
                {
                    return OutputFolderPath + RawHtmlFolderName + "/";
                }
            }
        }
        public static string OutputFolderPath
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return _outputFolderPath + "\\";
                }
                else
                {
                    return _outputFolderPath + "/";
                }
            }
        }

        public static string OutputTmpFolderPath
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return _outputFolderPath + "\\";
                }
                else
                {
                    return _outputFolderPath + "/";
                }
            }
        }

        public const string CachePageContentScriptPath = "CachePageContent.js";
        public const string RunnerScriptPath = "ScriptRunner.js";
        public const string OfflineBrowserScriptTemplatePath = "OfflineScriptTemplate.js";
        public const string NodeJsScriptTemplatePath = "NodeJsScriptTemplate.js";
    }
}
