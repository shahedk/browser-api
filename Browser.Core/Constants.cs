using System.Runtime.InteropServices;

namespace Browser.Core
{
    public class Constants
    {
        private const string OutputFolderName = "App_Data";

        public const string RawHtmlFolderName = "html";
        public const string ScreenShotFolderName = "screenshot";
        public const string RunnerScriptPath = "ScriptRunner.js";

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
                    return OutputFolderName + "\\";
                }
                else
                {
                    return OutputFolderName + "/";
                }
            }
        }
        public static string OutputTmpFolderPath
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return OutputFolderName + "\\";
                }
                else
                {
                    return OutputFolderName + "/";
                }
            }
        }
    }
}
