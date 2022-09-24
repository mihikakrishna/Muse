using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.Stores;
using MuseClient.ViewModels;
using MuseDomain.Models;

namespace MuseClient.Commands;

public class JoinRoomCommand : ICommand
{
    private readonly HomeWindowViewModel _viewModel;
    private readonly SignalRRoomManagementService _roomManagementService;

    public JoinRoomCommand(HomeWindowViewModel viewModel, SignalRRoomManagementService roomManagementService)
    {
        _viewModel = viewModel;
        _roomManagementService = roomManagementService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        var roomMessage = new RoomMessage(roomCode: _viewModel.RoomCode);
        await _roomManagementService.ValidateRoom(roomMessage);
    }

}