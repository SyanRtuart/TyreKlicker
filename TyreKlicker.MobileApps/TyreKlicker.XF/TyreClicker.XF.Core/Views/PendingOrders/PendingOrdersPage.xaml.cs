using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TyreKlicker.XF.Core.ViewModels;

namespace TyreKlicker.XF.Core.Views.PendingOrders
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class PendingOrdersPage : MvxContentPage<PendingOrdersViewModel>
    {
        public PendingOrdersPage()
        {
            InitializeComponent();
        }
    }
}