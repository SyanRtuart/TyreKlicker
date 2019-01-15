using MvvmCross.Forms.Views;
using TyreKlicker.XF.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Views.PendingOrders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PendingOrderDetailsPage : MvxContentPage<PendingOrderDetailsViewModel>
    {
        public PendingOrderDetailsPage()
        {
            InitializeComponent();
        }
    }
}