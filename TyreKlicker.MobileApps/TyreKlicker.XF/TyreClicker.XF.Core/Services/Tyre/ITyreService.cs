using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Tyre;

namespace TyreKlicker.XF.Core.Services.Tyre
{
    public interface ITyreService
    {
        Task<IEnumerable<Make>> GetMakes();

        Task<IEnumerable<Model>> GetModels(Make make);

        Task<IEnumerable<Years>> GetYears(Make make);
    }
}
