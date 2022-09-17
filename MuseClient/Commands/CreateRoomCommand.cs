using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseDomain.Models;

namespace MuseClient.Commands;

public class CreateRoomCommand : ICommand
{
    private readonly SignalRChatroomService _chatroomService;

    public CreateRoomCommand(SignalRChatroomService chatroomService)
    {
        _chatroomService = chatroomService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        var roomMessage = new RoomMessage(roomCode: string.Empty);
        await _chatroomService.CreateRoom(roomMessage);
    }

}