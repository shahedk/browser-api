using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Browser.Core
{
    public class PhantomJsBrowser : IBrowser
    {
        public string Name { get; } = "PhantomJS";

        public BrowserContent GetContent(BrowserSettings settings)
        {
            var content = new BrowserContent(settings.UniqueId, settings.TakeScreenShot, settings.SaveRawHtml);

            try
            {
                content.ConsoleOutput = LoadContent(settings);

                var dataFilePath = Constants.OutputFolderPath + settings.ParsedDataFilePath;

                content.ParsedData = File.ReadAllText(dataFilePath);
                File.Delete(dataFilePath);
            }
            catch (Exception ex)
            {
                content.Error = ex.Message;
            }

            return content;
        }

        #region Private Methods
        private string LoadContent(BrowserSettings browserSettings)
        {
            var args = BuildArguments(browserSettings);

            var fileName = Constants.OutputFolderPath + GetExeName();
            var startInfo = new ProcessStartInfo()
            {
                FileName = fileName,
                WorkingDirectory = Constants.OutputFolderPath,
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = args,
                RedirectStandardOutput = true
            };

            var process = Process.Start(startInfo);

            var builder = new StringBuilder();
            while (process != null && !process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();

                if (browserSettings.IsDebugMode)
                {
                    Console.WriteLine(line);
                }

                if (line != null && line.Contains("$$ScriptRunner::ExitBrowser$$"))
                {
                    break;
                }

                builder.Append(line);
            }

            var content = builder.ToString();
            if (content.StartsWith("ERROR:"))
            {
                throw new Exception(content);
            }
            else if (content.Contains("Status: fail"))
            {
                throw new Exception(content);
            }

            return content;
        }

        private string GetExeName()
        {
            var exe = "win\\phantomjs.exe";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                exe = "win\\phantomjs.exe";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                exe = "linux/phantomjs";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                exe = "osx/phantomjs";
            }

            return exe;
        }

        private static string BuildArguments(BrowserSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.RunnerScriptPath))
            {
                throw new Exception("Could not find RunnerScript!");
            }

            var args = $"\"{settings.RunnerScriptPath.ToArgFriendlyString()}\" " +    //0
                       $"\"{settings.PageUrl.ToArgFriendlyString()}\" " +             //1
                       $"\"{settings.WindowWidth}\" " +         //2
                       $"\"{settings.WindowHeight}\" " +        //3
                       $"\"{settings.ParserScriptName}\" " +    //4
                       $"\"{settings.TakeScreenShot}\" " +      //5
                       $"\"{settings.SaveRawHtml}\" " +         //6
                       $"\"{settings.UniqueId}\" " +            //7
                       $"\"{Constants.ScreenShotFolderName.ToArgFriendlyString()}\" " +//8
                       $"\"{Constants.RawHtmlFolderName.ToArgFriendlyString()}\" " +   //9
                       $"\"{settings.ParsedDataFilePath.ToArgFriendlyString()}\" " +  //10
                       $"\"{Constants.OutputTmpFolderPath.ToArgFriendlyString()}\" " +    //11
                       $"\"{settings.IsDebugMode}\" ";          //12

            if (settings.IsDebugMode)
            {
                Console.WriteLine(args);
            }

            return args;
        }

        #endregion
    }
}
