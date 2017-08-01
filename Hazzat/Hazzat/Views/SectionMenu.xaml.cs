﻿using Hazzat.Models;
using Hazzat.Types;
using Hazzat.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hazzat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SectionMenu : ContentPage
    {
        private SectionMenuViewModel viewModel;

        public SectionMenu(SectionMenuViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;

            switch(Device.RuntimePlatform)
            {
                case Device.Windows:
                    TBI.Order = ToolbarItemOrder.Secondary;
                    TBI.Text = "settings";
                    break;
            }
        }

        private void SubscribeMessages()
        {
            //SDKBUG: Seems like XAMARIN's Android/iOS do not support binding for grouped ListView
            MessagingCenter.Subscribe<SectionMenuViewModel>(this, "GetHymnGroupsCompleted", (sender) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    StructList.ItemsSource = viewModel.HymnGroups;
                });
            });
        }

        public async void SectionMenuInit(string ItemName, NavigationInfo navInfo)
        {
            SectionMenu newMenu = new SectionMenu(new SectionMenuViewModel(ItemName, navInfo));
            await Navigation.PopAsync();
            await Navigation.PushAsync(newMenu, true);
        }

        protected async void ServiceHymnTapped(object sender, ItemTappedEventArgs e)
        {
            ServiceHymnMenuItem item = (ServiceHymnMenuItem)e.Item;

            if (!item.HasSupportedContent)
            {
                DisplayNoContentAlert();
                return;
            }

            string breadcrumbName = viewModel.NavigationInfo.Method == NavigationMethod.Season ? item.Structure_Name : viewModel.Title;
            HymnPage HymnPage = new HymnPage(new HymnPageViewModel(item.ItemId, $"{breadcrumbName} - {item.Title}"));

            await Navigation.PushAsync(HymnPage, true);
        }

        private async void DisplayNoContentAlert()
        {
            string link = "http://hazzat.com";
            // setup link
            switch (viewModel.NavigationInfo.Method)
            {
                case NavigationMethod.Season:
                    link = $"http://www.hazzat.com/Seasons/tabid/57/ctl/ListSeasonHymns/mid/404/Season/{viewModel.NavigationInfo.ItemId}/Default.aspx";
                    break;
                case NavigationMethod.Type:
                    link = $"http://www.hazzat.com/Types/tabid/58/ctl/ListHymnsType/mid/405/Type/{viewModel.NavigationInfo.ItemId}/Default.aspx";
                    break;
                case NavigationMethod.Tune:
                    link = $"http://www.hazzat.com/Tunes/tabid/59/ctl/ListHymnsTune/mid/403/Tune/{viewModel.NavigationInfo.ItemId}/Default.aspx";
                    break;
                default:
                    break;
            }
            var result = await DisplayAlert("No supported content", "Please visit http://hazzat.com to view this hymn's content.", "Okay", "Link");

            if (!result)
            {
                Device.OpenUri(new Uri(link));
            }
        }

        protected async Task OnToolbarItemClicked(object sender, EventArgs args)
		{
			var result = await DisplayAlert("Notice", "2017 © hazzat.com", "Okay", "Link");

			if (!result)
			{
				Device.OpenUri(new Uri("http://www.hazzat.com/"));
			}
		}
    }
}
