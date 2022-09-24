using System;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Commands;
using MuseClient.Services;
using MuseClient.Stores;
using MuseDomain.Models;
using ReactiveUI;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly SignalRRoomManagementService _roomManagementService;
    private string _username;
    private string _roomCode;
    public ICommand CreateRoomCommand { get; }
    public ICommand JoinRoomCommand { get; }

    public HomeWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        
        _username = string.Empty;
        _roomCode = string.Empty;

        var roomManagementHubConnection = new HubConnectionBuilder()
                    .WithUrl("http://localhost:5000/roomHub")
                    .Build();
        var roomManagementService = new SignalRRoomManagementService(roomManagementHubConnection);
        _roomManagementService = roomManagementService;

        roomManagementService.Connect().ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Console.WriteLine("An exception has occured");
            }
        });

        CreateRoomCommand = new CreateRoomCommand(roomManagementService);
        JoinRoomCommand = new JoinRoomCommand(this, roomManagementService);

        roomManagementService.CreatedRoom += roomManagementService_CreatedRoom;
        roomManagementService.JoinedRoom += roomManagementService_JoinedRoom;
        roomManagementService.LeftRoom += roomManagementService_LeftRoom;
        roomManagementService.ValidatedRoom += roomManagementService_ValidatedRoom;
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

    private void roomManagementService_CreatedRoom(RoomMessage roomMessage)
    {
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(
            navigationStore: _navigationStore,
            username: _username,
            roomCode: roomMessage.RoomCode);
    }

    private void roomManagementService_JoinedRoom()
    {
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(
            navigationStore: _navigationStore,
            username: _username,
            roomCode: RoomCode);
    }

    private void roomManagementService_LeftRoom()
    {
        Console.WriteLine("Left room");
    }
    
    private async void roomManagementService_ValidatedRoom(bool isRoomValid)
    {
        if (!isRoomValid)
        {
            Console.WriteLine("Invalid room. Try again.");
            RoomCode = "";
            return;
        }

        var roomMessage = new RoomMessage(roomCode: RoomCode);
        await _roomManagementService.JoinRoom(roomMessage);

    }

}