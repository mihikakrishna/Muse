using System;
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
            var hubConnection = new HubConnectionBuilder()
                    .WithUrl("http://localhost:5000/museHub")
                    .Build();
            var museService = new SignalRMuseService(hubConnection);
            var navigationStore = new NavigationStore(museService);
            var mainWindowViewModel = new MainWindowViewModel(navigationStore, museService);

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel,
            };

            museService.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Console.WriteLine("An exception has occured");
                }
            });
        }

        base.OnFrameworkInitializationCompleted();
    }
}