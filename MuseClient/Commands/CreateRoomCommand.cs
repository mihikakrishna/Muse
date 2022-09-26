using System;
using System.Windows.Input;
using MuseClient.Services;

namespace MuseClient.Commands;

public class CreateRoomCommand : ICommand
{
    private readonly SignalRMuseService _signalRMuseService;

    public CreateRoomCommand(SignalRMuseService signalRMuseService)
    {
        _signalRMuseService = signalRMuseService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        await _signalRMuseService.CreateRoom();
    }

}