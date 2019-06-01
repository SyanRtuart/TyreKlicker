using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Services;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class SecondViewModel : MvxViewModel<Dictionary<string, string>>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        private Dictionary<string, string> _parameter;
        private readonly ILocalizeService _localizeService;

        public SecondViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, ILocalizeService localizeService)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _localizeService = localizeService;

            MainPageButtonText = "test";
        }

        public string MainPageButtonText { get; set; }

        public IMvxCommand BackCommand => new MvxCommand(async () =>
        {
            var localizedText = _localizeService.Translate("SecondPage_ByeBye_Localization");

            await _userDialogs.AlertAsync(localizedText);
            await _navigationService.Close(this);
        });

        public override void Prepare(Dictionary<string, string> parameter)
        {
            _parameter = parameter;

            if (_parameter != null && _parameter.ContainsKey("ButtonText"))
                MainPageButtonText = "ButtonText";
        }
    }
}