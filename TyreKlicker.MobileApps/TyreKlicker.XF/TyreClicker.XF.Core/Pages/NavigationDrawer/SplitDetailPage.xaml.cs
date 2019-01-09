using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TyreKlicker.XF.Core.ViewModels;

namespace TyreKlicker.XF.Core.Pages
{
    [MvxMasterDetailPagePresentation()]
    public partial class SplitDetailPage : MvxContentPage<SplitDetailViewModel>
    {
        public SplitDetailPage()
        {
            InitializeComponent();
        }
    }
}