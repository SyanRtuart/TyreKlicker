using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Availability;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SelectAvailabilityViewModel : MvxNavigationViewModel<ObservableCollection<Availability>, ObservableCollection<Availability>>
    {
        //private CreateNewPendingOrderCommand _order;
        private readonly IAvailabilityService _availabilityService;

        private ObservableCollection<Availability> _next2Weeks;
        private Availability _selectedAvailability;
        private ObservableCollection<Availability> _availability;

        public ObservableCollection<Availability> Next2Weeks
        {
            get { return _next2Weeks; }
            set
            {
                _next2Weeks = value;
                RaisePropertyChanged();
            }
        }

        public Availability SelectedAvailability
        {
            get { return _selectedAvailability; }
            set
            {
                _selectedAvailability = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Availability> Availability
        {
            get { return _availability; }
            set
            {
                _availability = value;
                RaisePropertyChanged();
            }
        }

        public SelectAvailabilityViewModel(
            IMvxLogProvider logProvider,
            IMvxNavigationService navigationService, IAvailabilityService availabilityService) : base(logProvider, navigationService)
        {
            _availabilityService = availabilityService;
        }

        public MvxCommand<Availability> ItemSelectedCommand => new MvxCommand<Availability>(ItemSelected);
        public IMvxAsyncCommand OkCommand => new MvxAsyncCommand(async () => await OkAsync());

        private async Task OkAsync()
        {
            var timeSlotList = new List<TimeSlot>();
            foreach (var availability in Next2Weeks)
            {
                timeSlotList.AddRange(from x in availability.TimeSlots.Where(x => x.IsSelected) select x);
            }

            foreach (var timeSlot in timeSlotList)
            {
                var newAvailability = new Availability
                {
                    Start = timeSlot.Start,
                    Finish = timeSlot.Finish
                };
                _availability.Add(newAvailability);
            }

            await NavigationService.Close(this, _availability);
        }

        private void ItemSelected(Availability availability)
        {
            SelectedAvailability = availability;
        }

        public override void Prepare(ObservableCollection<Availability> parameter)
        {
            _availability = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Next2Weeks = _availabilityService.GetNext2Weeks();
            SelectedAvailability = Next2Weeks.First();
        }
    }
}