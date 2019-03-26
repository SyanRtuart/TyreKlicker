using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using TyreKlicker.XF.Droid.Services.Message;

namespace TyreKlicker.XF.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.MvxApp, Core.FormsApp>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Xamarin.Forms.DependencyService.Register<MessageService>();
            Mvx.RegisterSingleton<Core.Services.ILocalizeService>(() => new Services.LocalizeService());
        }

        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            var formsPresenter = base.CreateFormsPagePresenter(viewPresenter);
            Mvx.RegisterSingleton(formsPresenter);
            return formsPresenter;
        }
    }
}