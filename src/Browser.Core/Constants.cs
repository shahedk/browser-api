using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Browser.Core
{
    public class Constants
    {
        public const string OutputFolderName = "App_Data";

        public const string ScriptFolderName = "script";
        public const string RawHtmlFolderName = "html";
        public const string ScreenShotFolderName = "screenshot";
        public const string RunnerScriptPath = "ScriptRunner.js";

        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        
        private static string _executingFolderPath = null;
        public static string ExecutingFolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(_executingFolderPath))
                {
                    _executingFolderPath = AssemblyDirectory;
                }

                return _executingFolderPath;
            }
        }
        
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
                    return Path.Combine( ExecutingFolderPath , OutputFolderName) + "\\";
                }
                else
                {
                    return Path.Combine( ExecutingFolderPath , OutputFolderName) + "/";
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
