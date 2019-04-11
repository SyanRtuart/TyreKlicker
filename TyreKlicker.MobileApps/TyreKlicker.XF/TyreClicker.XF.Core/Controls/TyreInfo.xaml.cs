using TyreKlicker.XF.Core.Models.Order;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TyreInfo : ContentView
    {
        public static readonly BindableProperty OrderProperty =
            BindableProperty.Create(nameof(Order), typeof(Order), typeof(TyreInfo), null);

        public Order Order
        {
            get { return (Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        public TyreInfo()
        {
            InitializeComponent();
        }
    }
}