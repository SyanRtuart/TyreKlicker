using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Views.PendingOrders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAvailabilityPage
    {
        public SelectAvailabilityPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }
}