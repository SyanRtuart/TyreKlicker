using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TyreKlicker.Core.ViewModels;

namespace TyreKlicker.Core.Pages
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class AccountPage : MvxContentPage<AccountViewModel>
    {
        public AccountPage()
        {
            InitializeComponent();
        }
    }
}