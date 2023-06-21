#if WINDOWS
using Microsoft.UI.Windowing;
#endif
namespace Calculator;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
#if WINDOWS7_0_OR_GREATER
        var windows = base.CreateWindow(activationState);
        windows.Height = 885;
        windows.Width = 450;
        return windows;
#else
        return base.CreateWindow(activationState);
#endif
    }

}
