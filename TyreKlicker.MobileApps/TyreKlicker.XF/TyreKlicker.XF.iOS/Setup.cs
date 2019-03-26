namespace TyreKlicker.XF.iOS
{
    public class Setup : MvxFormsIosSetup<Core.MvxApp, Core.FormsApp>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            //ToDo Register IMessage service
            Mvx.RegisterSingleton<Core.Services.ILocalizeService>(() => new Services.LocalizeService());
        }
    }
}