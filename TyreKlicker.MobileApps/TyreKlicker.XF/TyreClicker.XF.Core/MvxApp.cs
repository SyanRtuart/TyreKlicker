using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Plugin.Json;
using MvvmCross.ViewModels;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Services.AuthenticationService;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core
{
    public class MvxApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //CreatableTypes().
            //    EndingWith("Repository")
            //    .AsTypes()
            //    .RegisterAsLazySingleton();

            Mvx.RegisterType<Services.IAppSettings, Services.AppSettings>();
            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterType<IRequestProvider, RequestProvider>();
            Mvx.RegisterType<IAuthenticationService, AuthenticationService>();

            Resources.AppResources.Culture = Mvx.Resolve<Services.ILocalizeService>().GetCurrentCultureInfo();
            //Settings.AccessToken = null;
            //ToDo Test if the token is actually valid.
            if (string.IsNullOrEmpty(Settings.AccessToken))
                RegisterAppStart<ViewModels.LoginViewModel>();
            else
                RegisterAppStart<ViewModels.SplitRootViewModel>();
        }
    }
}