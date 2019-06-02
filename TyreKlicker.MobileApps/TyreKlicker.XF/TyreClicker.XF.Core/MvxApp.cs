using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Plugin.Json;
using MvvmCross.ViewModels;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Resources;
using TyreKlicker.XF.Core.Services;
using TyreKlicker.XF.Core.Services.AuthenticationService;
using TyreKlicker.XF.Core.Services.AutoMapper;
using TyreKlicker.XF.Core.Services.RequestProvider;
using TyreKlicker.XF.Core.Services.User;
using TyreKlicker.XF.Core.ViewModels;

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

            //Add AutoMapper
            Mvx.RegisterSingleton(MapService.ConfigureMapper());

            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.RegisterSingleton(() => UserDialogs.Instance);
            Mvx.RegisterType<IRequestProvider, RequestProvider>();
            Mvx.RegisterType<IAuthenticationService, AuthenticationService>();
            Mvx.RegisterType<IUserService, UserService>();

            AppResources.Culture = Mvx.Resolve<ILocalizeService>().GetCurrentCultureInfo();

            //Settings.AccessToken = null;
            //ToDo Test if the token is actually valid.
            //RegisterAppStart<SelectAvailabilityViewModel>();

            if (string.IsNullOrEmpty(Settings.AccessToken))
                RegisterAppStart<LoginViewModel>();
            else
                RegisterAppStart<SplitRootViewModel>();
        }
    }
}