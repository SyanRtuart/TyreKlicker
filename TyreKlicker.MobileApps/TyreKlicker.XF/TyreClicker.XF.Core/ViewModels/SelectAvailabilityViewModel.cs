using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Availability;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SelectAvailabilityViewModel : MvxNavigationViewModel<CreateNewPendingOrderCommand, CreateNewPendingOrderCommand>
    {
        private CreateNewPendingOrderCommand _order;
        private IAvailabilityService _availabilityService;

        private IEnumerable<Availability> _next2Weeks;

        public IEnumerable<Availability> Next2Weeks
        {
            get { return _next2Weeks; }
            set
            {
                _next2Weeks = value;
                RaisePropertyChanged();
            }
        }

        public SelectAvailabilityViewModel(
            IMvxLogProvider logProvider,
            IMvxNavigationService navigationService, IAvailabilityService availabilityService) : base(logProvider, navigationService)
        {
            _availabilityService = availabilityService;
        }

        public override void Prepare(CreateNewPendingOrderCommand order)
        {
            _order = order;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Next2Weeks = _availabilityService.GetNext2Weeks();
        }
    }
}