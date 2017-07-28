using Hazzat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hazzat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HymnPage : TabbedPage
    {
        private HymnPageViewModel viewModel;

        public HymnPage(HymnPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
    }
}
