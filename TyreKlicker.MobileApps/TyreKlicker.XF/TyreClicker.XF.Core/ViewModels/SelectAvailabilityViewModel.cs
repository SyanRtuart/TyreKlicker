using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Services.Availability;
using TyreKlicker.XF.Core.Validators;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SelectAvailabilityViewModel : MvxNavigationViewModel<ValidatableObject<ObservableCollection<Availability>>, ValidatableObject<ObservableCollection<Availability>>>
    {
        //private CreateNewPendingOrderCommand _order;
        private readonly IAvailabilityService _availabilityService;

        private ObservableCollection<Availability> _next2Weeks;
        private Availability _selectedAvailability;
        private ObservableCollection<TimeSlot> _selectedTimeSlots;
        private TimeSlot _selectedTimeSlot;
        private ValidatableObject<ObservableCollection<Availability>> _availability;

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

        public ValidatableObject<ObservableCollection<Availability>> Availability
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
            //_availability = new ValidatableObject<ObservableCollection<Availability>>();
            //_availability.Value = new ObservableCollection<Availability>();
        }

        private void AddValidation()
        {
            _availability.Validations.Add(new IsCountNotZeroRule<ObservableCollection<Availability>> { ValidationMessage = "At least 1 time slot is required." });
        }

        public MvxCommand<Availability> ItemSelectedCommand => new MvxCommand<Availability>(ItemSelected);

        public IMvxAsyncCommand OkCommand => new MvxAsyncCommand(async () => await OkAsync());

        //public IMvxCommand ValidateAvailabilityCommand => new MvxCommand(() => ValidateAvailability());

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
                _availability.Value.Add(newAvailability);
            }

            //if (Validate())
            //{
            await NavigationService.Close(this, _availability);
            //}
        }

        //private bool Validate()
        //{
        //    var isAvailabilityValid = ValidateAvailability();

        //    return isAvailabilityValid;
        //}

        //private bool ValidateAvailability() => _availability.Validate();

        private void ItemSelected(Availability availability)
        {
            SelectedAvailability = availability;
        }

        public override void Prepare(ValidatableObject<ObservableCollection<Availability>> parameter)
        {
            _availability = parameter;

            // AddValidation();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Next2Weeks = _availabilityService.GetNext2Weeks();
            SelectedAvailability = Next2Weeks.First();
        }
    }
}