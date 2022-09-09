using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Services;
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
            var chatService =  new SignalRChatService(new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/chatHub")
                    .Build());
            var navigationStore = new NavigationStore(chatService);
            var mainWindowViewModel = new MainWindowViewModel(navigationStore);
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel,
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}