using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TyreKlicker.XF.Core.ViewModels;

namespace TyreKlicker.XF.Core.Pages
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