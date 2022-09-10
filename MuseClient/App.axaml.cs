using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MuseClient.Stores;
using MuseClient.ViewModels;
using MuseClient.Views;

namespace MuseClient;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var navigationStore = new NavigationStore();
            var mainWindowViewModel = new MainWindowViewModel(navigationStore);

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}