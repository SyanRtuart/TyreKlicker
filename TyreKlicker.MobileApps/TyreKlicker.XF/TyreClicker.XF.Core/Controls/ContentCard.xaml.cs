using MvvmCross.Commands;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentCard : MvxContentView
    {
        public static readonly BindableProperty HeaderProperty =
            BindableProperty.Create("Header", typeof(string), typeof(ContentCard));

        public static readonly BindableProperty ButtonNameProperty =
            BindableProperty.Create("ButtonName", typeof(string), typeof(ContentCard));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(IMvxAsyncCommand), typeof(ContentCard), null);

        public static readonly BindableProperty ButtonVisibleProperty =
            BindableProperty.Create(nameof(ButtonVisible), typeof(bool), typeof(ContentCard), false);

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public string ButtonName
        {
            get { return (string)GetValue(ButtonNameProperty); }
            set { SetValue(ButtonNameProperty, value); }
        }

        public IMvxAsyncCommand Command
        {
            get { return (IMvxAsyncCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public bool ButtonVisible
        {
            get { return (bool)GetValue(ButtonVisibleProperty); }
            set { SetValue(ButtonVisibleProperty, value); }
        }

        public ContentCard()
        {
            InitializeComponent();
            CardHeader.SetBinding(Label.TextProperty, new Binding(nameof(Header), source: this));
            CardButton.SetBinding(Button.TextProperty, new Binding(nameof(ButtonName), source: this));
            CardButton.SetBinding(Button.CommandProperty, new Binding(nameof(Command), source: this));
            CardButton.SetBinding(IsVisibleProperty, new Binding(nameof(ButtonVisible), source: this));
        }
    }
}