using Hazzat.Abstract;
using Hazzat.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409


[assembly: Dependency(typeof(BaseUrl))]
[assembly: Dependency(typeof(ColorScheme))]
namespace Hazzat.UWP
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

            Windows.UI.Color color = (Windows.UI.Color)Windows.UI.Xaml.Application.Current.Resources["SystemAccentColor"];

            return $"{color.R},{color.B},{color.G}";

        }

        public string GetDefault()
        {
            Windows.UI.Color color = (Windows.UI.Color)Windows.UI.Xaml.Application.Current.Resources["SystemBaseHighColor"];
            return $"{color.R},{color.B},{color.G}";
        }

        public String GetBackground()
        {
            Windows.UI.Color color = (Windows.UI.Color)Windows.UI.Xaml.Application.Current.Resources["SystemAltHighColor"];
            return $"{color.R},{color.B},{color.G}";
        }
    }
}
