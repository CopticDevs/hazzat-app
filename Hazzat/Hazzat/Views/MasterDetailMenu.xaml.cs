using hazzat.com;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class MasterDetailMenu : MasterDetailPage, INotifyPropertyChanged
    {
        public static SectionMenu Menu { get; set; }

        public MasterDetailMenu(string Season, int SeasonId)
        {
            InitializeComponent();

            MessagingCenter.Subscribe<MainMenu>(this, "SeasonSelected",  TooglePresented);

            Menu = new SectionMenu(Season, SeasonId);

            Detail = new NavigationPage(Menu);
        }

        private void TooglePresented(MainMenu obj)
        {
            IsPresented = false;
        }


        protected void OnToolbarItemClicked(object sender, EventArgs args)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender; DisplayAlert("Yo!", "ToolbarItem '" + toolbarItem.Text + "' clicked", "okay");
        }
    }
}
