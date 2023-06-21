namespace Calculator.Views;

public partial class BasicCalculatorPage : ContentPage
{
	public BasicCalculatorPage(BasicCalculatorViewModel viewModel)
	{
		InitializeComponent();

		BindingContext= viewModel;
	}
}