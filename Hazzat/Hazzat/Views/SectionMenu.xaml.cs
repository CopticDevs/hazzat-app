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

            BindingContext = this.viewModel = viewModel;

            switch(Device.RuntimePlatform)
            {
                case Device.Windows:
                    TBI.Order = ToolbarItemOrder.Secondary;
                    TBI.Text = "settings";
                    break;
            }

            SubscribeMessages();
            viewModel.LoadContentBasedOnNavigationType();
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

            string breadcrumbName = viewModel.NavigationInfo.Method == NavigationMethod.Season ? item.Structure_Name : viewModel.Title;
            HymnPage HymnPage = new HymnPage(new HymnPageViewModel(item.ItemId, $"{breadcrumbName} - {item.Title}"));

            await Navigation.PushAsync(HymnPage, true);
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
