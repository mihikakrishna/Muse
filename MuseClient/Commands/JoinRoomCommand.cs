using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.ViewModels;
using MuseDomain.Models;

namespace MuseClient.Commands;

public class JoinRoomCommand : ICommand
{
    private readonly HomeWindowViewModel _viewModel;
    private readonly SignalRMuseService _signalRMuseService;

    public JoinRoomCommand(HomeWindowViewModel viewModel, SignalRMuseService signalRMuseService)
    {
        _viewModel = viewModel;
        _signalRMuseService = signalRMuseService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        var roomMessage = new RoomMessage(roomCode: _viewModel.RoomCode);
        await _signalRMuseService.ValidateRoom(roomMessage);
    }

}