using Hazzat.Helpers;
using Hazzat.Service;
using Xamarin.Forms;

namespace Hazzat.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the hazzat controller instance
        /// </summary>
        public HazzatController HazzatController;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public BaseViewModel()
        {
            HazzatController = new HazzatController();
        }
    }
}
