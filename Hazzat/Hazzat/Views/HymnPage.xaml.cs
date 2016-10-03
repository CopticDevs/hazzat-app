using HazzatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class HymnPage : TabbedPage
    {
        public HymnPage(string HymnName, int HymnId)
        {
            InitializeComponent();

            Title = HymnName;

            SubscribeMessage();

            App.NameViewModel.CreateHymnTextViewModel(HymnId);
        }

        private void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithHymnText", (sender) =>
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

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithHazzat", (sender) =>
            {
                if (App.NameViewModel?.HazzatHymnContentInfo != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();

                    source.Html = $"{App.NameViewModel.HazzatHymnContentInfo[0].Content_Coptic}";

                    HazzatWebView.Source = source;
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithVerticalHazzat", (sender) =>
            {
                if (App.NameViewModel?.VerticalHazzatHymnContent != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();

                    source.Html = $"{App.NameViewModel.VerticalHazzatHymnContent[0].Content_Coptic}";

                    VerticalHazzatWebView.Source = source;
                }
            });
        }
    }
}
