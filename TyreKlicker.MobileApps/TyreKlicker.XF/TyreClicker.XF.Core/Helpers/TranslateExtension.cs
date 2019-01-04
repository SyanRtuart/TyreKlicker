using TyreKlicker.XF.Core.Resources;
using TyreKlicker.XF.Core.Services;

namespace TyreKlicker.XF.Core.Helpers
{
    public static class TranslateExtension
    {
        public static string Translate(this ILocalizeService localizeService, string str)
        {
            var tranlation = AppResources.ResourceManager.GetString(str, localizeService.GetCurrentCultureInfo());
            return string.IsNullOrEmpty(tranlation) ? str : tranlation;
        }
    }
}