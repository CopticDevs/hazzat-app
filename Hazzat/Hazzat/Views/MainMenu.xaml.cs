using hazzat.com;
using HazzatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat
{
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            InitializeComponent();

            SubscribeMessage();

            App.NameViewModel.createSeasonsViewModel(true);
        }

        private void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "Done", (sender) =>
            {
                if (App.NameViewModel?.Seasons != null)
                {
                    if (App.NameViewModel.Seasons.Count() > 0)
                    {
                        foreach (SeasonInfo Season in App.NameViewModel.Seasons)
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                MenuStack.Add(CreateItemView(Season.Name, $"({Season.ServiceHymnsCount.ToString()}) hymns"));
                            });
                        }
                    }
                }
            });
        }

        public TextCell CreateItemView(string name, string description)
        {
            return new TextCell
            {
                Text = name,
                Detail = description
            };
        }
    }
}

