using System;
using System.Globalization;
using MvvmCross;
using TyreKlicker.XF.Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Resources
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo _cultureInfo;

        public TranslateExtension()
        {
            _cultureInfo = Mvx.Resolve<ILocalizeService>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null) return null;

            var translation = AppResources.ResourceManager.GetString(Text, _cultureInfo);

#if DEBUG
            if (translation == null)
                throw new ArgumentException(string.Format("Key {0} was not found for culture {1}.", Text,
                    _cultureInfo));
#endif
            return translation;
        }
    }
}