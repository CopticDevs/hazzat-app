using Hazzat.Views;
using HazzatService;
using NodaTime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Hazzat
{
    public partial class App : Application
    {
        String one = "Tout";
        String two = "Baba";
        String three = "Hatour";
        String four = "Kiahk";
        String five = "Touba";
        String six = "Amshir";
        String seven = "Baramhat";
        String eight = "Barmouda";
        String nine = "Bashans";
        String ten = "Baona";
        String eleven = "Abib";
        String twelve = "Mesra";
        String thirteen = "El Nasii";

        public object Month { get; private set; }
        public string Day { get; private set; }

        private static Dictionary<string, Dictionary<string, List<string>>> AppDataCache { get; set; }    // Designed for BySeasons ViewModel

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
            InitializeComponent();

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

            Tuple<string, string> currTime = CurrentTime();

            MainPage = new MasterDetailMenu("Annual", 1); //Set to current Season
        }

        private Tuple<string, string> CurrentTime()
        {

            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int Day = DateTime.Now.Day;
            int Hour = DateTime.Now.Hour;
            int Min = DateTime.Now.Minute;
            LocalDateTime DateToConvertFrom = new LocalDateTime(Year, Month, Day, Hour, Min);

            CalendarSystem CoptCal = CalendarSystem.GetCopticCalendar(1);

            LocalDateTime ConvertedTime = DateToConvertFrom.WithCalendar(CoptCal);


            getMonth(ConvertedTime.Month.ToString());
            getDay(ConvertedTime.Day.ToString());

            if (this.Month.Equals("1"))
            {
                this.Month = one;
            }

            if (this.Month.Equals("2"))
            {
                this.Month = two;
            }

            if (this.Month.Equals("3"))
            {
                this.Month = three;
            }

            if (this.Month.Equals("4"))
            {
                this.Month = four;
            }

            if (this.Month.Equals("5"))
            {
                this.Month = five;
            }
            if (this.Month.Equals("6"))
            {
                this.Month = six;
            }
            if (this.Month.Equals("7"))
            {
                this.Month = seven;
            }

            if (this.Month.Equals("8"))
            {
                this.Month = eight;
            }

            if (this.Month.Equals("9"))
            {
                this.Month = nine;
            }

            if (this.Month.Equals("10"))
            {
                this.Month = ten;
            }
            if (this.Month.Equals("11"))
            {
                this.Month = eleven;
            }
            if (this.Month.Equals("12"))
            {
                this.Month = twelve;
            }
            if (this.Month.Equals("13"))
            {
                this.Month = thirteen;
            }

            return new Tuple<string, string>(ConvertedTime.Month.ToString(), ConvertedTime.Day.ToString());
        }

        private void getMonth(string Month)
        {
            this.Month = Month;
        }

        private void getDay(string Day)
        {
            this.Day = Day;
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
            return delegate
            {
                Dictionary<string, Dictionary<string, List<string>>> tempcache = new Dictionary<string, Dictionary<string, List<string>>>();
            };
        }

        protected override void OnSleep()
        {
            // Properties["AppDataCache"] = Serialize(AppDataCache);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
