using CanExecuteSample.ViewModels;
using CanExecuteSample.Views;
using Xamarin.Forms;

namespace CanExecuteSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var commandPage = new CommandPage()
            {
                BindingContext = new CommandViewModel(),
            };

            MainPage = new NavigationPage(commandPage);
        }
    }
}
