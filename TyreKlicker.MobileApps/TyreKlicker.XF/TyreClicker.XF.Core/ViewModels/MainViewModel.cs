using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;

namespace TyreKlicker.XF.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ButtonText = Resources.AppResources.MainPageButton;
        }

        public IMvxCommand PressMeCommand =>
            new MvxCommand(() =>
            {
                ButtonText = Resources.AppResources.MainPageButtonPressed;
            });

        public IMvxAsyncCommand GoToSecondPageCommand =>
            new MvxAsyncCommand(async () =>
            {
                var param = new Dictionary<string, string> { { "ButtonText", ButtonText } };

                await _navigationService.Navigate<SecondViewModel, Dictionary<string, string>>(param);
            });

        //public IMvxCommand OpenUrlCommand =>
        //    new MvxAsyncCommand<string>(async (url) =>
        //    {
        //        await Browser.OpenAsync(url, BrowserLaunchType.External);
        //    });

        public string ButtonText { get; set; }
    }
}