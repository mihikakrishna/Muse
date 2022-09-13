using System;
using MuseClient.Stores;
using ReactiveUI;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private string _username;
    private string _roomCode;

    public HomeWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _username = string.Empty;
        _roomCode = string.Empty;
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

    public void CreateRoomCommand()
    {
        Console.WriteLine("Create Room");
        // send roomId to server to createRoom
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(
            navigationStore: _navigationStore, 
            username: _username, 
            roomCode: _roomCode);
    }

    public void JoinRoomCommand()
    {
        Console.WriteLine("Join room");
        // send roomId to server to ValidateRoom
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(
            navigationStore: _navigationStore, 
            username: _username, 
            roomCode: _roomCode);
    }
}