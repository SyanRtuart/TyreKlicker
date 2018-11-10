using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace TyreKlicker.Core.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    //[MvxModalPresentation]
    public partial class LoginPage : MvxContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}