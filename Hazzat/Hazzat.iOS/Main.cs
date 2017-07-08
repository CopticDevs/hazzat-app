using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Hazzat.Abstract;
using static Hazzat.iOS.Application;

[assembly: Dependency(typeof(BaseUrl_iOS))]
[assembly: Dependency(typeof(ColorScheme))]
namespace Hazzat.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }

        public class BaseUrl_iOS : IWebAssets
        {
            public string Get()
            {
                return NSBundle.MainBundle.BundlePath;
            }
        }

        public class ColorScheme : IColorRender
        {
            public String GetAccent()
            {
                return $"0,122,255";
            }

            public string GetDefault()
            {
                return $"0,0,0";
            }

            public String GetBackground()
            {
                return $"255,255,255";
            }
        }

    }
}
