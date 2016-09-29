using HazzatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class HymnsView : ContentPage
    {
        public HymnsView(string ServiceName, int ServiceId)
        {
            InitializeComponent();

            SubscribeMessage();

            App.NameViewModel.createViewModelHymns(ServiceId);
        }

        private void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "Done", (sender) =>
            {
                if (App.NameViewModel?.HazzatHymns != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        HymnList.ItemsSource = App.NameViewModel.HazzatHymns;
                    });
                }
            });
        }
    }
}
