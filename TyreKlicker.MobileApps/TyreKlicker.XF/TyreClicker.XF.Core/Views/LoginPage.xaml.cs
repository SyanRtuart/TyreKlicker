using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TyreKlicker.XF.Core.ViewModels;

namespace TyreKlicker.XF.Core.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    //[MvxModalPresentation]
    public partial class LoginPage : MvxContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}