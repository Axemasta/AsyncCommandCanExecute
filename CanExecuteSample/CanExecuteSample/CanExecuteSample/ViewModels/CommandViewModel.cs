using CanExecuteSample.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace CanExecuteSample.ViewModels
{
    public class CommandViewModel : ViewModelBase
    {
        public IAsyncCommand FirstCommand { get; }
        public IAsyncCommand SecondCommand { get; }
        public IAsyncCommand RiskyCommand { get; }
        public IAsyncCommand<string> StringCommand { get; }
        public IAsyncCommand<int> IntCommand { get; }

        public CommandViewModel()
        {
            Title = "Commands";

            FirstCommand = CreateCommand(OnFirst);
            SecondCommand = CreateCommand(OnSecond);
            RiskyCommand = CreateCommand(OnRisky);
            StringCommand = CreateCommand<string>(OnString);
            IntCommand = CreateCommand<int>(OnInt);
        }

        private async Task OnFirst()
        {
            Debug.WriteLine("Executing first command");

            await Task.Delay(1000);
        }

        private async Task OnSecond()
        {
            Debug.WriteLine("Executing second command");

            await Task.Delay(1000);
        }

        private async Task OnRisky()
        {
            Debug.WriteLine("Executing a risky command");

            await Task.Delay(1000);

            Debug.WriteLine("Performing risky business");

            throw new Exception("The risk went too far!");
        }

        private async Task OnString(string value)
        {
            Debug.WriteLine($"Executing string command with param: {value}");

            await Task.Delay(1000);
        }

        private async Task OnInt(int value)
        {
            Debug.WriteLine($"Executing int command with param: {value}");

            await Task.Delay(1000);
        }
    }
}
