using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Tyre;

namespace TyreKlicker.XF.Core.Services.Tyre
{
    public interface ITyreService
    {
        Task<ObservableCollection<Make>> GetMakes();

        Task<ObservableCollection<Model>> GetModels(Make make);

        Task<ObservableCollection<Years>> GetYears(Make make);

        Task<ObservableCollection<ApiVehicle>> GetVehicles(Make make, Model model, Years year);

        Task<ObservableCollection<WheelPair>> GetWheelPairs(Make make, Model model, Years year);
    }
}