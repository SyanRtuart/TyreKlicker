using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Views;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.ViewModels;
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
    }
}