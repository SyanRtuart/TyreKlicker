using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Order;
using TyreKlicker.XF.Core.Models.Tyre;
using TyreKlicker.XF.Core.Services.Order;
using TyreKlicker.XF.Core.Services.Tyre;

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
        private Order _order;
        private Make _selectedMake;
        private Model _selectedModel;
        private Years _selectedYear;
        private Vehicle _selectedVehicle;

        public NewPendingOrderViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IOrderService orderService,
            ITyreService tyreService) :
            base(logProvider, navigationService)
        {
            _tyreService = tyreService;
            _orderService = orderService;
            GetMakesCommand.ExecuteAsync();
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

        public IMvxAsyncCommand GetMakesCommand => new MvxAsyncCommand(async () => await GetMakesAsync());

        public IMvxAsyncCommand GetModelsCommand => new MvxAsyncCommand(async () => await GetModelsAsync());

        public IMvxAsyncCommand GetYearsCommand => new MvxAsyncCommand(async () => await GetYearsAsync());

        public IMvxAsyncCommand GetVehiclesCommand => new MvxAsyncCommand(async () => await GetVehiclesAsync());

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
            Vehicles = await _tyreService.GetVehicles(_selectedMake, _selectedModel, _selectedYear);
        }
    }
}