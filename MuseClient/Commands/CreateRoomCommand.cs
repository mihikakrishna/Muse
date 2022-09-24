using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseDomain.Models;

namespace MuseClient.Commands;

public class CreateRoomCommand : ICommand
{
    private readonly SignalRRoomManagementService _roomManagementService;

    public CreateRoomCommand(SignalRRoomManagementService roomManagementService)
    {
        _roomManagementService = roomManagementService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        var roomMessage = new RoomMessage(roomCode: string.Empty);
        await _roomManagementService.CreateRoom(roomMessage);
    }

}