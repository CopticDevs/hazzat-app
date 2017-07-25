using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hazzat.ViewModels
{
    public class HymnPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets/Sets the service hymn id.
        /// </summary>
        public int ServiceHymnId { get; set; }
        public ServiceHymnsContentInfo[] TextHymnContentInfo { get; private set; }
        public ServiceHymnsContentInfo[] HazzatHymnContentInfo { get; private set; }
        public ServiceHymnsContentInfo[] VerticalHazzatHymnContent { get; private set; }

        /// <summary>
        /// Instantiates a new HymnPageViewModel with the given parameters
        /// </summary>
        /// <param name="serviceHymnId">The service hymn id.</param>
        /// <param name="title">The title.</param>
        public HymnPageViewModel(int serviceHymnId, string title)
        {
            ServiceHymnId = serviceHymnId;
            Title = title;

            GetHymnContent();
        }

        private void GetHymnContent()
        {
            IsBusy = true;
            HazzatController.GetSeasonServiceHymnText(ServiceHymnId, OnGetSeasonServiceHymnTextCompleted);
            HazzatController.GetSeasonServiceHymnHazzat(ServiceHymnId, OnGetSeasonServiceHymnHazzatCompleted);
            HazzatController.GetSeasonServiceHymnVerticalHazzat(ServiceHymnId, OnGetSeasonServiceHymnVerticalHazzatCompleted);
        }

        private void OnGetSeasonServiceHymnTextCompleted(object sender, GetSeasonServiceHymnTextCompletedEventArgs e)
        {
            TextHymnContentInfo = e.Result;
            MessagingCenter.Send(this, "DoneWithHymnText");
            IsBusy = false;
        }

        private void OnGetSeasonServiceHymnHazzatCompleted(object sender, GetSeasonServiceHymnHazzatCompletedEventArgs e)
        {
            HazzatHymnContentInfo = e.Result;
            MessagingCenter.Send(this, "DoneWithHazzat");
            IsBusy = false;
        }

        private void OnGetSeasonServiceHymnVerticalHazzatCompleted(object sender, GetSeasonServiceHymnVerticalHazzatCompletedEventArgs e)
        {
            VerticalHazzatHymnContent = e.Result;
            MessagingCenter.Send(this, "DoneWithVerticalHazzat");
            IsBusy = false;
        }



    }
}
