﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Models.Tyre;
using TyreKlicker.XF.Core.Services.Order;
using TyreKlicker.XF.Core.Services.Tyre;
using TyreKlicker.XF.Core.Validators;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SelectVehicalViewModel : MvxNavigationViewModel<Vehicle, Vehicle>
    {
        private readonly ITyreService _tyreService;

        private ObservableCollection<Make> _makes;
        private ObservableCollection<Model> _models;

        private ValidatableObject<Make> _selectedMake;
        private ValidatableObject<Model> _selectedModel;
        private ValidatableObject<Wheel> _selectedTyre;
        private ValidatableObject<ApiVehicle> _selectedVehicle;
        private ValidatableObject<VehicleTrim> _selectedVehicleTrim;
        private ValidatableObject<Years> _selectedYear;
        private ObservableCollection<VehicleTrim> _trims;
        private ObservableCollection<Wheel> _tyres;
        private ObservableCollection<ApiVehicle> _vehicle;
        private ObservableCollection<Years> _years;

        public SelectVehicalViewModel
        (IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService,
            ITyreService tyreService) :
            base(logProvider, navigationService)
        {
            _tyreService = tyreService;

            _selectedMake = new ValidatableObject<Make>();
            _selectedModel = new ValidatableObject<Model>();
            _selectedYear = new ValidatableObject<Years>();
            _selectedTyre = new ValidatableObject<Wheel>();
            _selectedVehicleTrim = new ValidatableObject<VehicleTrim>();

            AddValidations();
        }

        public ObservableCollection<Make> Makes
        {
            get => _makes;
            set
            {
                _makes = value;
                RaisePropertyChanged(() => Makes);
            }
        }

        public ObservableCollection<Model> Models
        {
            get => _models;
            set
            {
                _models = value;
                RaisePropertyChanged(() => Models);
            }
        }

        public ObservableCollection<Years> Years
        {
            get => _years;
            set
            {
                _years = value;
                RaisePropertyChanged(() => Years);
            }
        }

        public ObservableCollection<ApiVehicle> Vehicles
        {
            get => _vehicle;
            set
            {
                _vehicle = value;
                RaisePropertyChanged(() => Vehicles);
            }
        }

        public ObservableCollection<VehicleTrim> Trims
        {
            get => _trims;
            set
            {
                _trims = value;
                RaisePropertyChanged(() => Trims);
            }
        }

        public ObservableCollection<Wheel> Tyres
        {
            get => _tyres;
            set
            {
                _tyres = value;
                RaisePropertyChanged(() => Tyres);
            }
        }

        public ValidatableObject<Make> SelectedMake
        {
            get => _selectedMake;
            set
            {
                _selectedMake = value;
                RaisePropertyChanged(() => SelectedMake);
            }
        }

        public ValidatableObject<Model> SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                RaisePropertyChanged(() => SelectedModel);
            }
        }

        public ValidatableObject<Years> SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                RaisePropertyChanged(() => SelectedYear);
            }
        }

        public ValidatableObject<ApiVehicle> SelectedVehicle
        {
            get => _selectedVehicle;
            set
            {
                _selectedVehicle = value;
                RaisePropertyChanged(() => SelectedVehicle);
            }
        }

        public ValidatableObject<VehicleTrim> SelectedVehicleTrim
        {
            get => _selectedVehicleTrim;
            set
            {
                _selectedVehicleTrim = value;
                RaisePropertyChanged(() => SelectedVehicleTrim);
            }
        }

        public ValidatableObject<Wheel> SelectedTyre
        {
            get => _selectedTyre;
            set
            {
                _selectedTyre = value;
                RaisePropertyChanged(() => SelectedTyre);
            }
        }

        public IMvxCommand OkCommand => new MvxCommand(async () => await OkAsync());

        public IMvxCommand GetMakesCommand => new MvxCommand(async () => await GetMakesAsync());

        public IMvxCommand GetModelsCommand => new MvxCommand(async () => await GetModelsAsync());

        public IMvxCommand GetYearsCommand => new MvxCommand(async () => await GetYearsAsync());

        public IMvxCommand GetVehiclesCommand => new MvxCommand(async () => await GetVehiclesAsync());

        public IMvxCommand GetTyresCommand => new MvxCommand(GetTyres);

        public override async Task Initialize()
        {
            await base.Initialize();
            await GetMakesAsync();
        }

        private async Task GetMakesAsync()
        {
            Makes = await _tyreService.GetMakes();
        }

        private async Task GetModelsAsync()
        {
            ClearSelection(true);

            Models = await _tyreService.GetModels(_selectedMake.Value);
        }

        private async Task GetYearsAsync()
        {
            ClearSelection(years: true, trims: true, tyres: true);

            Years = await _tyreService.GetYears(_selectedMake.Value);
        }

        private async Task GetVehiclesAsync()
        {
            if (_selectedMake.Value == null || _selectedModel.Value == null || _selectedYear.Value == null)
                return;

            var vehicles =
                await _tyreService.GetVehicles(_selectedMake.Value, _selectedModel.Value, _selectedYear.Value);
            Vehicles = vehicles;
            GetTrims(vehicles);
        }

        private void GetTrims(ObservableCollection<ApiVehicle> vehicles)
        {
            if (!vehicles.Any()) return;

            Trims = new ObservableCollection<VehicleTrim>();
            foreach (var trim in vehicles.Select(x => x.Trim).Distinct()) Trims.Add(new VehicleTrim {Trim = trim});
        }

        private void GetTyres()
        {
            if (SelectedVehicleTrim == null) return;

            var vehicles = Vehicles.Where(x => x.Trim == SelectedVehicleTrim.Value.Trim).ToList();
            Tyres = new ObservableCollection<Wheel>();
            foreach (var vehicle in vehicles)
            foreach (var tyre in vehicle.Wheels)
                Tyres.Add(tyre.Front);
        }

        private void ClearSelection(bool makes = false,
            bool models = false,
            bool years = false,
            bool trims = false,
            bool tyres = false)
        {
            if (makes)
            {
                Models = null;
                SelectedModel = new ValidatableObject<Model>();
            }

            if (models)
            {
                Years = null;
                SelectedYear = new ValidatableObject<Years>();
            }

            if (years)
            {
                Vehicles = null;
                SelectedVehicle = new ValidatableObject<ApiVehicle>();
            }

            if (trims)
            {
                Trims = null;
                SelectedVehicleTrim = new ValidatableObject<VehicleTrim>();
            }

            if (tyres)
            {
                Tyres = null;
                SelectedTyre = new ValidatableObject<Wheel>();
            }

            AddValidations();
        }

        private void ClearSelection(bool all)
        {
            Models = null;
            SelectedModel = new ValidatableObject<Model>();
            Years = null;
            SelectedYear = new ValidatableObject<Years>();
            Vehicles = null;
            SelectedVehicle = new ValidatableObject<ApiVehicle>();
            Trims = null;
            SelectedVehicleTrim = new ValidatableObject<VehicleTrim>();
            Tyres = null;
            SelectedTyre = new ValidatableObject<Wheel>();

            AddValidations();
        }

        public override void Prepare(Vehicle parameter)
        {
        }

        public override void Prepare()
        {
        }

        private async Task OkAsync()
        {
            if (Validate())
                await NavigationService.Close(this, new Vehicle
                {
                    Make = SelectedMake.Value.Name,
                    Model = SelectedModel.Value.Name,
                    Year = SelectedYear.Value.Name,
                    Trim = SelectedVehicleTrim.Value.Trim,
                    Tyre = SelectedTyre.Value.Tire
                });
        }

        private bool Validate()
        {
            var isValidSelectedMake = ValidateSelectedMake();
            var isValidSelectedModel = ValidateSelectedModel();
            var isValidSelectedYear = ValidateSelectedYear();
            var isValidSelectedTyre = ValidateSelectedTyre();
            var isValidSelectedVehicleTrim = ValidateSelectedVehicleTrim();

            return isValidSelectedMake && isValidSelectedModel && isValidSelectedYear && isValidSelectedTyre &&
                   isValidSelectedVehicleTrim;
        }

        private bool ValidateSelectedMake()
        {
            return _selectedMake.Validate();
        }

        private bool ValidateSelectedModel()
        {
            return _selectedModel.Validate();
        }

        private bool ValidateSelectedYear()
        {
            return _selectedYear.Validate();
        }

        private bool ValidateSelectedTyre()
        {
            return _selectedTyre.Validate();
        }

        private bool ValidateSelectedVehicleTrim()
        {
            return _selectedVehicleTrim.Validate();
        }

        private void AddValidations()
        {
            _selectedMake.Validations.Add(new IsMakeValidRule<Make> {ValidationMessage = "An make is required."});
            _selectedModel.Validations.Add(new IsModelValidRule<Model> {ValidationMessage = "An model is required."});
            _selectedYear.Validations.Add(new IsYearsValidRule<Years> {ValidationMessage = "An model is required."});
            _selectedTyre.Validations.Add(new IsWheelValidRule<Wheel> {ValidationMessage = "An tyre is required."});
            _selectedVehicleTrim.Validations.Add(new IsVehicleTrimValidRule<VehicleTrim>
                {ValidationMessage = "An model is required."});
        }
    }
}