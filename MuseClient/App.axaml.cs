using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Services;
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
        HubConnection connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:5001/chatHub")
            .Build();

        ChatViewModel chatViewModel = ChatViewModel.CreateConnectedViewModel(new SignalRChatService(connection));

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(chatViewModel),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
