namespace Calculator;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		PageRouting();
	}

	private void PageRouting()
	{
		Routing.RegisterRoute(nameof(BasicCalculatorPage), typeof(BasicCalculatorPage));
	}
}
