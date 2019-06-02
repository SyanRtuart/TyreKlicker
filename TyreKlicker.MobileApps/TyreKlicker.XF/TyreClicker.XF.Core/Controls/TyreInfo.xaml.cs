using System;
using TyreKlicker.XF.Core.Models.Order;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TyreInfo : ContentView
    {
        public static readonly BindableProperty VehicleProperty =
            BindableProperty.Create(nameof(Vehicle), typeof(Vehicle), typeof(TyreInfo), null);

        public TyreInfo()
        {
            InitializeComponent();
        }

        public Vehicle Vehicle
        {
            get => (Vehicle) GetValue(VehicleProperty);
            set => SetValue(VehicleProperty, value);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
        }
    }
}