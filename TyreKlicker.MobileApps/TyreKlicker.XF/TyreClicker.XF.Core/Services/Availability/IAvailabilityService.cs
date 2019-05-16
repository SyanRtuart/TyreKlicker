using System.Collections.ObjectModel;

namespace TyreKlicker.XF.Core.Services.Availability
{
    public interface IAvailabilityService
    {
        ObservableCollection<Models.Order.Availability> GetNext2Weeks();
    }
}