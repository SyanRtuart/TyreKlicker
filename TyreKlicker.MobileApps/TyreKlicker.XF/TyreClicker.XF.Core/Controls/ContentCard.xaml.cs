using MvvmCross.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentCard : ContentView
    {
        public static readonly BindableProperty HeaderProperty =
            BindableProperty.Create("Header", typeof(string), typeof(ContentCard));

        public static readonly BindableProperty ButtonNameProperty =
            BindableProperty.Create("ButtonName", typeof(string), typeof(ContentCard));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(IMvxAsyncCommand), typeof(ContentCard), null);

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

        public ContentCard()
        {
            InitializeComponent();
            CardHeader.SetBinding(Label.TextProperty, new Binding(nameof(Header), source: this));
            CardButton.SetBinding(Button.TextProperty, new Binding(nameof(ButtonName), source: this));
            CardButton.SetBinding(Button.CommandProperty, new Binding(nameof(Command), source: this));
        }
    }
}