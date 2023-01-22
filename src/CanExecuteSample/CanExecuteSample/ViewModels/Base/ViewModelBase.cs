namespace CanExecuteSample.ViewModels.Base
{
    public abstract class ViewModelBase : CanExecuteViewModel
    {
        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}
