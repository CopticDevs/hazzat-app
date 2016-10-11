using Hazzat.Views;
using HazzatService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;



namespace Hazzat
{
    public class App : Application
    {
        private static Dictionary<string, Dictionary<string, List<string>>> AppDataCache { get; set; }    // Designed for BySeasons ViewModel
        public static bool isDataCacheBuilding { get; set; } = false;


        private static ByNameMainViewModel nameViewModel = null;
        internal static ByNameMainViewModel NameViewModel
        {
            get
            {
                if (nameViewModel == null)
                {
                    nameViewModel = new ByNameMainViewModel();
                }
                return nameViewModel;
            }
        }

        public static string Serialize(Dictionary<string, Dictionary<string, List<string>>> viewModel)
        {
            XmlSerializer serialize = new XmlSerializer(typeof(Dictionary<string, Dictionary<string, List<string>>>));

            using (var stringWriter = new StringWriter())
            {
                serialize.Serialize(stringWriter, viewModel);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
        public static Dictionary<string, Dictionary<string, List<string>>> Deserialize(string viewModel)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Dictionary<string, Dictionary<string, List<string>>>));

            using (var stringReader = new StringReader(viewModel))
            {
                Dictionary<string, Dictionary<string, List<string>>> model = (Dictionary<string, Dictionary<string, List<string>>>)serializer.Deserialize(stringReader);
                return model;
            }
        }


        public App()
        {
            Resources = new ResourceDictionary();

            //Define global app styles here
            Resources.Add(new Style(typeof(ContentPage))
            {
                Setters = {
                    new Setter
                    {
                        Property = Page.PaddingProperty,
                        Value = Device.OnPlatform(new Thickness(10), new Thickness(10), new Thickness(10))
                    }
                }
            });
            Resources.Add("accent", Color.Accent);
            Resources.Add("default", Color.Default);

            //Perform Season Calculations Here

            MainPage = new MasterDetailMenu(); //Set to current Season
        }

        protected override void OnStart()
        {
            if (Properties.ContainsKey("AppDataCache"))
            {
                try
                {
                    Deserialize(Properties["AppDataCache"] as string);
                }
                catch
                {
                    Task.Run(BuildDataCache());
                }
            }
            else
            {
                Task.Run(BuildDataCache());
            }
        }

        private Action BuildDataCache()
        {
            return delegate {
                isDataCacheBuilding = true;

                Dictionary<string, Dictionary<string, List<string>>> tempcache = new Dictionary<string, Dictionary<string, List<string>>>();
            };
        }

        protected override void OnSleep()
        {
            Properties["AppDataCache"] = Serialize(AppDataCache);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
