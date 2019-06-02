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
            BindableProperty.Create(nameof(Header), typeof(string), typeof(ContentCard));

        public static readonly BindableProperty ButtonNameProperty =
            BindableProperty.Create(nameof(ButtonName), typeof(string), typeof(ContentCard));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(IMvxCommand), typeof(ContentCard), null);

        public static readonly BindableProperty ButtonVisibleProperty =
            BindableProperty.Create(nameof(ButtonVisible), typeof(bool), typeof(ContentCard), false);

        public ContentCard()
        {
            InitializeComponent();
            CardHeader.SetBinding(Label.TextProperty, new Binding(nameof(Header), source: this));
            CardButton.SetBinding(Button.TextProperty, new Binding(nameof(ButtonName), source: this));
            CardButton.SetBinding(Button.CommandProperty, new Binding(nameof(Command), source: this));
            CardButton.SetBinding(IsVisibleProperty, new Binding(nameof(ButtonVisible), source: this));
        }

        public string Header
        {
            get => (string) GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public string ButtonName
        {
            get => (string) GetValue(ButtonNameProperty);
            set => SetValue(ButtonNameProperty, value);
        }

        public IMvxCommand Command
        {
            get => (IMvxCommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public bool ButtonVisible
        {
            get => (bool) GetValue(ButtonVisibleProperty);
            set => SetValue(ButtonVisibleProperty, value);
        }
    }
}