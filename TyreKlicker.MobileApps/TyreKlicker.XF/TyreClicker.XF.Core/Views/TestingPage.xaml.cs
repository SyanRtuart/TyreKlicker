using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Views
{
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestingPage : MvxContentPage
    {
        public TestingPage()
        {
            InitializeComponent();
        }
    }
}