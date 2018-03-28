using System;
using System.Collections.Generic;
using System.Text;

namespace Browser.Core
{
    public interface IBrowser
    {
        BrowserContent GetContent(BrowserSettings settings);
    }
}
