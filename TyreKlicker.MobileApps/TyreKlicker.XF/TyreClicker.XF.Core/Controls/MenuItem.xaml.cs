using MvvmCross.Commands;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TyreKlicker.XF.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuItem : Grid
    {
        public const string Order = "Order";
        public const string JobBoard = "Job Board";
        public const string News = "News";
        public const string Account = "Account";

        public static readonly BindableProperty LabelProperty =
            BindableProperty.Create("Label", typeof(string), typeof(MenuItem));

        public static readonly BindableProperty IconUniCodeProperty =
            BindableProperty.Create("IconUniCode", typeof(string), typeof(MenuItem));

        //&#xf1ea;

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(IMvxCommand), typeof(MenuItem), null);

        //        public static readonly BindableProperty ViewModelNameProperty =
        //    BindableProperty.Create(nameof(ViewModelName), typeof(string), typeof(MenuItem), null);

        //        public static readonly BindableProperty VMProperty =
        //BindableProperty.Create(nameof(VMProperty), typeof(Object), typeof(MenuItem), null);

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public string IconUniCode
        {
            get { return (string)GetValue(IconUniCodeProperty); }
            set { SetValue(IconUniCodeProperty, value); }
        }

        public IMvxCommand Command
        {
            get { return (IMvxCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        //public string ViewModelName
        //{
        //    get { return (string)GetValue(ViewModelNameProperty); }
        //    set { SetValue(ViewModelNameProperty, value); }
        //}

        //public Object VM
        //{
        //    get { return (Object)GetValue(VMProperty); }
        //    set { SetValue(VMProperty, value); }

        //}

        public static void Execute(ICommand command)
        {
            if (command == null) return;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        public MenuItem()
        {
            InitializeComponent();

            BindingContext = this;
        }
    }
}