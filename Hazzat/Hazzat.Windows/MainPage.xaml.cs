using Hazzat.Abstract;
using Hazzat.Windows8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl))]
[assembly: Dependency(typeof(ColorScheme))]
namespace Hazzat.Windows8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new Hazzat.App());
        }
    }
    public class BaseUrl : IWebAssets
    {
        public string Get()
        {
            return "ms-appx-web:///Assets";
        }
    }
    public class ColorScheme : IColorRender
    {
        public String GetAccent()
        {
            Windows.UI.Color color = (Windows.UI.Color)Windows.UI.Xaml.Application.Current.Resources["TextSelectionHighlightColorThemeBrush"];

            return $"{color.R},{color.B},{color.G}";

        }

        public string GetDefault()
        {
            Windows.UI.Color color;
            if (Windows.UI.Xaml.Application.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                color = Windows.UI.Color.FromArgb(222, 225, 225, 225);  //#DEFFFFFF
            }
            else
            {
                color = Windows.UI.Color.FromArgb(222, 0, 0, 0);    //#DE000000
            }

            return $"{color.R},{color.B},{color.G}";
        }

        public String GetBackground()
        {
            Windows.UI.Color color;
            if (Windows.UI.Xaml.Application.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                color = Windows.UI.Color.FromArgb(255, 29, 29, 29); //#FF1D1D1D
            }
            else
            {
                color = Windows.UI.Color.FromArgb(255, 255, 255, 255);  //#DEFFFFFF
            }

            return $"{color.R},{color.B},{color.G}";
        }
    }
}
