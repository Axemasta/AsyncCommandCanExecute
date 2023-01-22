using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using BaseCommand = Xamarin.CommunityToolkit.ObjectModel.Internals.BaseCommand<object>;

namespace CanExecuteSample.ViewModels.Base
{
    public abstract class CanExecuteViewModel : ObservableObject
    {
        private List<BaseCommand> typeCommands = new List<BaseCommand>();

        private bool canExecute = true;

        public bool CanExecute
        {
            get => canExecute;
            set
            {
                if (SetProperty(ref canExecute, value))
                {
                    RaiseCanExecuteChangedChanged();
                }
            }
        }

        public IAsyncCommand CreateCommand(Func<Task> handler)
        {
            Func<Task> wrappedHandler = () => Task.Run(async () =>
            {
                try
                {
                    CanExecute = false;

                    await handler();
                }
                catch (Exception ex)
                {
                    // This can be improved by passing a logger
                    Console.WriteLine("An exception occured executing command: {0}", ex);
                }
                finally
                {
                    CanExecute = true;
                }
            });

            var command = new AsyncCommand(wrappedHandler, () => CanExecute);

            RegisterCommandForUpdates(command);

            return command;
        }

        public IAsyncCommand<TValue> CreateCommand<TValue>(Func<TValue, Task> handler)
        {
            Func<TValue, Task> wrappedHandler = (value) => Task.Run(async () =>
            {
                try
                {
                    CanExecute = false;

                    await handler(value);
                }
                catch (Exception ex)
                {
                    // This can be improved by passing a logger
                    Console.WriteLine("An exception occured executing command: {0}", ex);
                }
                finally
                {
                    CanExecute = true;
                }
            });

            var command = new AsyncCommand<TValue>(wrappedHandler, () => CanExecute);

            RegisterCommandForUpdates(command);

            return command;
        }

        private void RaiseCanExecuteChangedChanged()
        {
            typeCommands.ForEach(c => c.RaiseCanExecuteChanged());
        }

        private void RegisterCommandForUpdates(object command)
        {
            var objectCommand = command as BaseCommand;

            if (objectCommand is null)
            {
                return;
            }

            typeCommands.Add(objectCommand);
        }
    }
}
