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

        public event PropertyChangedEventHandler PropertyChange;

        public MasterDetailMenu()
        {
            InitializeComponent();

            Detail = Menu = new SectionMenu("Annual", 1);

            Menu.PropertyChanged += ChangeSectionMenu();
        }

        public PropertyChangedEventHandler ChangeSectionMenu()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Detail = Menu;
            });
            return PropertyChange;
        }


        protected void OnToolbarItemClicked(object sender, EventArgs args)
        {
            ToolbarItem toolbarItem = (ToolbarItem)sender; DisplayAlert("Yo!", "ToolbarItem '" + toolbarItem.Text + "' clicked", "okay");
        }
    }
}
