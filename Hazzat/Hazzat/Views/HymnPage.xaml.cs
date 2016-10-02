using HazzatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class HymnPage : ContentPage
    {
        public HymnPage(string HymnName, int HymnId)
        {
            InitializeComponent();

            Title = HymnName;

            SubscribeMessage();

            App.NameViewModel.createHymnTextViewModel(HymnId);
        }

        private void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithContent", (sender) =>
            {
                if (App.NameViewModel?.HymnContentInfo != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        HymnText.Text = App.NameViewModel.HymnContentInfo[0].Content_English;
                        Title = $"{Title} - Tune {App.NameViewModel.HymnContentInfo[0].Type_Name}";
                    });
                }
            });
        }
    }
}
