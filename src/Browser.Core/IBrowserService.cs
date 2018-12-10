using System;
using System.Collections.Generic;
using System.Text;

namespace Browser.Core
{
    public interface IBrowserService
    {
        BrowserContent GetRawHtml(string url,
            string script,
            int height = 900,
            int width = 1200);

        BrowserContent GetRawHtml(string url,
            int height = 900,
            int width = 1200);

        BrowserContent TakeScreenShot(string url,
            string script,
            int height = 900,
            int width = 1200);

        BrowserContent TakeScreenShot(string url,
            int height = 900,
            int width = 1200);

        BrowserContent GetContent(string url, 
            int height = 900, 
            int width = 1200, 
            bool takeScreenshot = false,
            bool saveRawHtml = false);

        BrowserContent GetContent(string url, 
            string script, 
            int height = 900, 
            int width = 1200, 
            bool takeScreenshot = false,
            bool saveRawHtml = false);

    }
}
