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
    private string _username;
    private string _roomCode;
    public ICommand CreateRoomCommand { get; }
    public ICommand JoinRoomCommand { get; }

    public HomeWindowViewModel(NavigationStore navigationStore)
    {
        _username = string.Empty;
        _roomCode = string.Empty;

        var chatRoomHubConnection = new HubConnectionBuilder()
                    .WithUrl("http://localhost:5000/roomHub")
                    .Build();

        var chatRoomService = new SignalRChatroomService(chatRoomHubConnection);

        chatRoomService.Connect().ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Console.WriteLine("An exception has occured");
            }
        });

        CreateRoomCommand = new CreateRoomCommand(this, navigationStore, chatRoomService);
        JoinRoomCommand = new JoinRoomCommand(this, navigationStore);

        chatRoomService.ReceivedCreatedRoom += chatRoomService_CreatedRoom;
        chatRoomService.JoinedRoom += chatRoomService_JoinedRoom;
        chatRoomService.LeftRoom += chatRoomService_LeftRoom;
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

    private void chatRoomService_CreatedRoom(RoomMessage roomMessage)
    {
        _roomCode = roomMessage.RoomCode;
        Console.WriteLine(_roomCode);
    }

    private void chatRoomService_JoinedRoom(RoomMessage roomMessage)
    {
        _roomCode = roomMessage.RoomCode;
        Console.WriteLine(_roomCode);
    }

    private void chatRoomService_LeftRoom(RoomMessage roomMessage)
    {
        _roomCode = roomMessage.RoomCode;
        Console.WriteLine(_roomCode);
    }

}