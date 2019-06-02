using System.Collections.Generic;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TyreKlicker.XF.Core.Resources;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ButtonText = AppResources.MainPageButton;
        }

        public IMvxCommand PressMeCommand =>
            new MvxCommand(() => { ButtonText = AppResources.MainPageButtonPressed; });

        public IMvxCommand GoToSecondPageCommand =>
            new MvxCommand(async () =>
            {
                var param = new Dictionary<string, string> {{"ButtonText", ButtonText}};

                await _navigationService.Navigate<SecondViewModel, Dictionary<string, string>>(param);
            });

        //public IMvxCommand OpenUrlCommand =>
        //    new MvxCommand<string>(async (url) =>
        //    {
        //        await Browser.OpenAsync(url, BrowserLaunchType.External);
        //    });

        public string ButtonText { get; set; }
    }
}