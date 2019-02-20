using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Models.Tyre;
using TyreKlicker.XF.Core.Services.Order;
using TyreKlicker.XF.Core.Services.Tyre;
using TyreKlicker.XF.Core.Validators;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class NewPendingOrderViewModel : MvxNavigationViewModel
    {
        private readonly IOrderService _orderService;
        private readonly ITyreService _tyreService;

        private ObservableCollection<Make> _makes;
        private ObservableCollection<Model> _models;
        private ObservableCollection<Years> _years;
        private ObservableCollection<Vehicle> _vehicle;
        private ObservableCollection<VehicleTrim> _trims;
        private ObservableCollection<Wheel> _tyres;

        private Order _order;
        private Make _selectedMake;
        private Model _selectedModel;
        private Years _selectedYear;
        private Vehicle _selectedVehicle;
        private Wheel _selectedTyre;
        private VehicleTrim _selectedVehicleTrim;
        private ValidatableObject<string> _registration;

        public NewPendingOrderViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService,
            ITyreService tyreService) :
            base(logProvider, navigationService)
        {
            _tyreService = tyreService;
            _orderService = orderService;

            GetMakesCommand.ExecuteAsync();

            _registration = new ValidatableObject<string>();

            AddValidations();
        }

        public ObservableCollection<Make> Makes
        {
            get { return _makes; }
            set
            {
                _makes = value;
                RaisePropertyChanged(() => Makes);
            }
        }

        public ObservableCollection<Model> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                RaisePropertyChanged(() => Models);
            }
        }

        public ObservableCollection<Years> Years
        {
            get { return _years; }
            set
            {
                _years = value;
                RaisePropertyChanged(() => Years);
            }
        }

        public ObservableCollection<Vehicle> Vehicles
        {
            get { return _vehicle; }
            set
            {
                _vehicle = value;
                RaisePropertyChanged(() => Vehicles);
            }
        }

        public ObservableCollection<VehicleTrim> Trims
        {
            get { return _trims; }
            set
            {
                _trims = value;
                RaisePropertyChanged(() => Trims);
            }
        }

        public ObservableCollection<Wheel> Tyres
        {
            get { return _tyres; }
            set
            {
                _tyres = value;
                RaisePropertyChanged(() => Tyres);
            }
        }

        public Make SelectedMake
        {
            get { return _selectedMake; }
            set
            {
                _selectedMake = value;

                RaisePropertyChanged(() => SelectedMake);
            }
        }

        public Model SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
                RaisePropertyChanged(() => SelectedModel);
            }
        }

        public Years SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                RaisePropertyChanged(() => SelectedYear);
            }
        }

        public Vehicle SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                _selectedVehicle = value;
                RaisePropertyChanged(() => SelectedVehicle);
            }
        }

        public VehicleTrim SelectedVehicleTrim
        {
            get { return _selectedVehicleTrim; }
            set
            {
                _selectedVehicleTrim = value;
                RaisePropertyChanged(() => SelectedVehicleTrim);
            }
        }

        public Wheel SelectedTyre
        {
            get { return _selectedTyre; }
            set
            {
                _selectedTyre = value;
                RaisePropertyChanged(() => SelectedTyre);
            }
        }

        public ValidatableObject<string> Registration
        {
            get
            {
                return _registration;
            }
            set
            {
                _registration = value;
                RaisePropertyChanged(() => Registration);
            }
        }

        public IMvxAsyncCommand GetMakesCommand => new MvxAsyncCommand(async () => await GetMakesAsync());

        public IMvxAsyncCommand GetModelsCommand => new MvxAsyncCommand(async () => await GetModelsAsync());

        public IMvxAsyncCommand GetYearsCommand => new MvxAsyncCommand(async () => await GetYearsAsync());

        public IMvxAsyncCommand GetVehiclesCommand => new MvxAsyncCommand(async () => await GetVehiclesAsync());

        public IMvxAsyncCommand GetTyresCommand => new MvxAsyncCommand(async () => await GetTyresAsync());

        public IMvxCommand ValidateRegistrationCommand => new MvxCommand(() => ValidateRegistration());

        private async Task GetMakesAsync()
        {
            Makes = await _tyreService.GetMakes();
        }

        private async Task GetModelsAsync()
        {
            Models = await _tyreService.GetModels(_selectedMake);
        }

        private async Task GetYearsAsync()
        {
            Years = await _tyreService.GetYears(_selectedMake);
        }

        private async Task GetVehiclesAsync()
        {
            var vehicles = await _tyreService.GetVehicles(_selectedMake, _selectedModel, _selectedYear);
            Vehicles = vehicles;
            GetTrims(vehicles);
        }

        private void GetTrims(ObservableCollection<Vehicle> vehicles)
        {
            Trims = new ObservableCollection<VehicleTrim>();
            foreach (var trim in vehicles.Select(x => x.Trim).Distinct())
            {
                Trims.Add(new VehicleTrim { Trim = trim });
            }
        }

        private async Task GetTyresAsync()
        {
            var vehicles = Vehicles.Where(x => x.Trim == SelectedVehicleTrim.Trim).ToList();
            Tyres = new ObservableCollection<Wheel>();
            foreach (var vehicle in vehicles)
            {
                foreach (var tyre in vehicle.Wheels)
                {
                    Tyres.Add(tyre.Front);
                }
            }
        }

        private bool ValidateRegistration()
        {
            return _registration.Validate();
        }

        private void AddValidations()
        {
            _registration.Validations.Add(new VehicleRegistrationRule<string> { ValidationMessage = "A username is required." });
        }
    }
}