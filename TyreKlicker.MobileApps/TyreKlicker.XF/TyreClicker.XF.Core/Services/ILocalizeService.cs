using System.Globalization;

namespace TyreKlicker.XF.Core.Services
{
    public interface ILocalizeService
    {
        CultureInfo GetCurrentCultureInfo();
    }
}