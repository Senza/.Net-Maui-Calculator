namespace Calculator.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {

        }

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string title;

        
    }
}
