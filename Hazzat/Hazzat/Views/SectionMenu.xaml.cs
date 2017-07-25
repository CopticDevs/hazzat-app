using Hazzat.Models;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using Hazzat.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hazzat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SectionMenu : ContentPage
    {
        public SectionMenuViewModel viewModel;
        private static ObservableCollection<ServiceDetails> serviceList;
        private static int itemId;
        private static string itemName;

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

            serviceList = new ObservableCollection<ServiceDetails>();
            itemId = -1;
            itemName = viewModel.Title;
        }

        public async void SectionMenuInit(string ItemName, int ItemId, NavigationType navType)
        {
            SectionMenu newMenu = new SectionMenu(new SectionMenuViewModel(ItemId, ItemName, navType));
            await Navigation.PopAsync();
            await Navigation.PushAsync(newMenu, true);
        }

        protected async void ServiceHymnTapped(object sender, ItemTappedEventArgs e)
        {
            ServiceHymnMenuItem item = (ServiceHymnMenuItem)e.Item;

            string breadcrumbName = viewModel.NavigationType == NavigationType.Season ? item.Structure_Name : itemName;
            HymnPage HymnPage = new HymnPage(breadcrumbName, item.Title, item.ItemId);

            await Navigation.PushAsync(HymnPage, true);

            HymnPage.SubscribeMessage();

            App.NameViewModel.GetHymnContent(item.ItemId);
        }

		protected async Task OnToolbarItemClicked(object sender, EventArgs args)
		{
			var result = await DisplayAlert("Notice", "This is a beta version for a proof of concept.", "Okay", "Link");

			if (!result)
			{
				Device.OpenUri(new Uri("http://www.hazzat.com/"));
			}
		}
    }
}
