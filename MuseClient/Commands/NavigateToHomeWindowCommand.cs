using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.Stores;
using MuseClient.ViewModels;
using MuseDomain.Models;

namespace MuseClient.Commands;

public class NavigateToHomeWindowCommand : ICommand
{
    private readonly ListenTogetherWindowViewModel _viewModel;
    private readonly SignalRMuseService _signalRMuseService;
    private readonly NavigationStore _navigationStore;

    public NavigateToHomeWindowCommand(ListenTogetherWindowViewModel viewModel, SignalRMuseService museService, NavigationStore navigationStore)
    {
        _viewModel = viewModel;
        _signalRMuseService = museService;
        _navigationStore = navigationStore;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        var roomMessage = new RoomMessage(roomCode: _viewModel.RoomCode);
        await _signalRMuseService.LeaveRoom(roomMessage);
        _navigationStore.CurrentViewModel = new HomeWindowViewModel(_navigationStore, _signalRMuseService);
    }
}