using hazzat.com;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hazzat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMenu : MasterDetailPage
    {
        public static SectionMenu Menu { get; set; }

        public MasterDetailMenu(string Season, int SeasonId)
        {
            InitializeComponent();

            MessagingCenter.Subscribe<MainMenu>(this, "MenuItemSelected", HideMasterPage);

            Menu = new SectionMenu(Season, SeasonId, NavigationType.Season);

            Detail = new NavigationPage(Menu);
        }

        public void HideMasterPage(MainMenu obj)
        {
            if (Device.OS != TargetPlatform.Windows)
            {
                IsPresented = false;
            }
        }

        protected void OnToolbarItemClicked(object sender, EventArgs args)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender; DisplayAlert("Yo!", "ToolbarItem '" + toolbarItem.Text + "' clicked", "okay");
        }
    }
}
