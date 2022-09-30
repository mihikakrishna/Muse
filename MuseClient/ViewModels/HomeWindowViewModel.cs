using System;
using System.Windows.Input;
using MuseClient.Commands;
using MuseClient.Services;
using MuseClient.Stores;
using MuseDomain.Models;
using ReactiveUI;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private readonly SignalRMuseService _signalRMuseService;
    private readonly NavigationStore _navigationStore;
    private string _username;
    private string _roomCode;

    public ICommand CreateRoomCommand { get; }
    public ICommand JoinRoomCommand { get; }

    public HomeWindowViewModel(NavigationStore navigationStore, SignalRMuseService signalRMuseService)
    {
        _signalRMuseService = signalRMuseService;
        _navigationStore = navigationStore;

        _username = string.Empty;
        _roomCode = string.Empty;

        CreateRoomCommand = new CreateRoomCommand(signalRMuseService);
        JoinRoomCommand = new JoinRoomCommand(this, _signalRMuseService);

        _signalRMuseService.CreatedRoom += SignalRMuseService_CreatedRoom;
        _signalRMuseService.JoinedRoom += SignalRMuseService_JoinedRoom;
        _signalRMuseService.ValidatedRoom += SignalRMuseService_ValidatedRoom;
    }

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string RoomCode
    {
        get => _roomCode;
        set => this.RaiseAndSetIfChanged(ref _roomCode, value);
    }

    private void SignalRMuseService_CreatedRoom(RoomMessage roomMessage)
    {
        _navigationStore.CurrentViewModel = new ListenTogetherWindowViewModel(
            navigationStore: _navigationStore,
            signalRMuseService: _signalRMuseService,
            username: Username,
            roomCode: roomMessage.RoomCode);
    }

    private void SignalRMuseService_JoinedRoom()
    {
        _navigationStore.CurrentViewModel = new ListenTogetherWindowViewModel(
            navigationStore: _navigationStore,
            signalRMuseService: _signalRMuseService,
            username: Username,
            roomCode: RoomCode);
    }

    private async void SignalRMuseService_ValidatedRoom(bool isRoomValid)
    {
        if (!isRoomValid)
        {
            Console.WriteLine("Invalid room. Try again.");
            RoomCode = "";
            return;
        }

        var roomMessage = new RoomMessage(roomCode: RoomCode);
        await _signalRMuseService.JoinRoom(roomMessage);

    }

}