




using MvvmCross.Forms.Core;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TyreKlicker.XF.Core
{
    public partial class FormsApp : MvxFormsApplication
    {
        public FormsApp()
        {
            InitializeComponent();
        }
    }
}