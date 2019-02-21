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

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SelectVehicalViewModel : MvxNavigationViewModel<Order, Order>
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

        public SelectVehicalViewModel
        (IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService,
            ITyreService tyreService) :
            base(logProvider, navigationService)
        {
            _tyreService = tyreService;
            _orderService = orderService;

            GetMakesCommand.ExecuteAsync();
        }

        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                RaisePropertyChanged(() => Order);
            }
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
                _order.Make = value.Name;
                RaisePropertyChanged(() => SelectedMake);
            }
        }

        public Model SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
                _order.Model = value.Name;
                RaisePropertyChanged(() => SelectedModel);
            }
        }

        public Years SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                _order.Year = value.Name;
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
                _order.Trim = value.Trim;
                RaisePropertyChanged(() => SelectedVehicleTrim);
            }
        }

        public Wheel SelectedTyre
        {
            get { return _selectedTyre; }
            set
            {
                _selectedTyre = value;
                _order.Tyre = value.Tire;
                RaisePropertyChanged(() => SelectedTyre);
            }
        }

        public IMvxAsyncCommand OkCommand => new MvxAsyncCommand(async () => await NavigateBack());

        public IMvxAsyncCommand GetMakesCommand => new MvxAsyncCommand(async () => await GetMakesAsync());

        public IMvxAsyncCommand GetModelsCommand => new MvxAsyncCommand(async () => await GetModelsAsync());

        public IMvxAsyncCommand GetYearsCommand => new MvxAsyncCommand(async () => await GetYearsAsync());

        public IMvxAsyncCommand GetVehiclesCommand => new MvxAsyncCommand(async () => await GetVehiclesAsync());

        public IMvxCommand GetTyresCommand => new MvxCommand(GetTyres);

        private async Task GetMakesAsync()
        {
            Makes = await _tyreService.GetMakes();
        }

        private async Task GetModelsAsync()
        {
            ClearSelection(true);

            Models = await _tyreService.GetModels(_selectedMake);
        }

        private async Task GetYearsAsync()
        {
            ClearSelection(years: true, trims: true, tyres: true);

            Years = await _tyreService.GetYears(_selectedMake);
        }

        private async Task GetVehiclesAsync()
        {
            if (_selectedMake == null || _selectedModel == null || _selectedYear == null)
                return;

            var vehicles = await _tyreService.GetVehicles(_selectedMake, _selectedModel, _selectedYear);
            Vehicles = vehicles;
            GetTrims(vehicles);
        }

        private void GetTrims(ObservableCollection<Vehicle> vehicles)
        {
            if (!vehicles.Any()) return;

            Trims = new ObservableCollection<VehicleTrim>();
            foreach (var trim in vehicles.Select(x => x.Trim).Distinct())
            {
                Trims.Add(new VehicleTrim { Trim = trim });
            }
        }

        private void GetTyres()
        {
            if (SelectedVehicleTrim == null) return;

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

        private void ClearSelection(bool makes = false,
            bool models = false,
            bool years = false,
            bool trims = false,
            bool tyres = false)
        {
            if (makes)
            {
                Models = null;
                SelectedModel = null;
            }

            if (models)
            {
                Years = null;
                SelectedYear = null;
            }

            if (years)
            {
                Vehicles = null;
                SelectedVehicle = null;
            }

            if (trims)
            {
                Trims = null;
                SelectedVehicleTrim = null;
            }

            if (tyres)
            {
                Tyres = null;
                SelectedTyre = null;
            }
        }

        private void ClearSelection(bool all)
        {
            Models = null;
            SelectedModel = null;
            Years = null;
            SelectedYear = null;
            Vehicles = null;
            SelectedVehicle = null;
            Trims = null;
            SelectedVehicleTrim = null;
            Tyres = null;
            SelectedTyre = null;
        }

        private async Task NavigateBack()
        {
            await NavigationService.Close(this, _order);
        }

        public override void Prepare()
        {
            // first callback. Initialize parameter-agnostic stuff here
        }

        public override void Prepare(Order order)
        {
            _order = order;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            // do the heavy work here
        }
    }
}