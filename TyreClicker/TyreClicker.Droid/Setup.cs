// ---------------------------------------------------------------
// <author>Paul Datsyuk</author>
// <url>https://www.linkedin.com/in/pauldatsyuk/</url>
// ---------------------------------------------------------------

using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;

namespace TyreKlicker.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.MvxApp, Core.FormsApp>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

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