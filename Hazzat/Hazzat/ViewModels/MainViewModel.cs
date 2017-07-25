using Hazzat.Service;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using System;
using Xamarin.Forms;

namespace Hazzat.ViewModels
{
    public class MainViewModel
    {
        /// <summary>
        /// A collection of hazzat.com objects
        /// </summary>
        public ServiceHymnsContentInfo[] TextHymnContentInfo { get; private set; }
        public ServiceHymnsContentInfo[] HazzatHymnContentInfo { get; private set; }
        public ServiceHymnsContentInfo[] VerticalHazzatHymnContent { get; private set; }

        private HazzatController hazzatController = new HazzatController();

        public void GetHymnContent(int itemId)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetSeasonServiceHymnText(itemId, OnGetSeasonServiceHymnTextCompleted);
            hazzatController.GetSeasonServiceHymnHazzat(itemId, OnGetSeasonServiceHymnHazzatCompleted);
            hazzatController.GetSeasonServiceHymnVerticalHazzat(itemId, OnGetSeasonServiceHymnVerticalHazzatCompleted);
        }

        public void OnGetSeasonServiceHymnTextCompleted(object sender, GetSeasonServiceHymnTextCompletedEventArgs e)
        {
            TextHymnContentInfo = e.Result;
            MessagingCenter.Send(this, "DoneWithHymnText");
            App.IsLoaded = true;
        }

        public void OnGetSeasonServiceHymnHazzatCompleted(object sender, GetSeasonServiceHymnHazzatCompletedEventArgs e)
        {
            HazzatHymnContentInfo = e.Result;
            MessagingCenter.Send(this, "DoneWithHazzat");
            App.IsLoaded = true;
        }

        private void OnGetSeasonServiceHymnVerticalHazzatCompleted(object sender, GetSeasonServiceHymnVerticalHazzatCompletedEventArgs e)
        {
            VerticalHazzatHymnContent = e.Result;
            MessagingCenter.Send(this, "DoneWithVerticalHazzat");
            App.IsLoaded = true;
        }
    }
}
