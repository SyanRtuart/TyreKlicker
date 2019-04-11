using TyreKlicker.XF.Core.Models.Address;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressInfo : ContentView
    {
        public static readonly BindableProperty AddressProperty =
            BindableProperty.Create(nameof(Address), typeof(Address), typeof(AddressInfo), null);

        public Address Address
        {
            get { return (Address)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public AddressInfo()
        {
            InitializeComponent();
        }
    }
}