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
            SubscribeMessage();

            App.NameViewModel.createSeasonsViewModel(true);

            InitializeComponent();
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
                                MenuStack.Children.Add(CreateItemView(Color.White, Season.Name));

                            });
                        }
                    }
                }
            });
        }

        View CreateItemView(Color color, string name)
        {
            return new Frame
            {
                OutlineColor = Color.Accent,
                Padding = new Thickness(5),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 15,
                    Children =
                    {
                        new BoxView
                        {
                            Color = color
                        }, new Label
                        { Text = name,
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                    }
                }
            };
        }
    }
}

