using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.ViewModels;
using MuseDomain.Models;

namespace MuseClient.Commands;

public class NavigateToHomeWindowCommand : ICommand
{
    private readonly ListenTogetherWindowViewModel _viewModel;
    private readonly SignalRMuseService _signalRMuseService;

    public NavigateToHomeWindowCommand(ListenTogetherWindowViewModel viewModel, SignalRMuseService signalRMuseService)
    {
        _viewModel = viewModel;
        _signalRMuseService = signalRMuseService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        var roomMessage = new RoomMessage(roomCode: _viewModel.RoomCode);
        await _signalRMuseService.LeaveRoom(roomMessage);
    }
}