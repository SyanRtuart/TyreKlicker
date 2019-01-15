using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using TyreKlicker.XF.Core.ViewModels;

namespace TyreKlicker.XF.Core.Views
{
    [MvxMasterDetailPagePresentation()]
    public partial class SplitDetailView : MvxContentPage<SplitDetailViewModel>
    {
        public SplitDetailView()
        {
            InitializeComponent();
        }
    }
}