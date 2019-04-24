using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using TyreKlicker.XF.Core.Models.Address;
using TyreKlicker.XF.Core.Models.Order;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SelectAvailabilityViewModel : MvxNavigationViewModel<CreateNewPendingOrderCommand, CreateNewPendingOrderCommand>
    {
        private CreateNewPendingOrderCommand _order;

        private List<Address> _addresses;

        public List<Address> Addresses
        {
            get { return _addresses; }
            set
            {
                _addresses = value;
                RaisePropertyChanged();
            }
        }


        public SelectAvailabilityViewModel(
            IMvxLogProvider logProvider, 
            IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public override void Prepare(CreateNewPendingOrderCommand order)
        {
            _order = order;

         
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Addresses = new List<Address>();
            for (int i = 0; i < 15; i++)
            {
                Addresses.Add(new Address
                {
                    City = $"City{i}"
                });
            }
        }
    }
}
