using System.Windows.Input;
using MuseClient.Commands;
using MuseClient.Services;
using MuseClient.Stores;
using ReactiveUI;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private readonly SignalRMuseService _signalRMuseService;

    private string _username;
    private string _roomCode;

    public ICommand CreateRoomCommand { get; }
    public ICommand JoinRoomCommand { get; }

    public HomeWindowViewModel(NavigationStore navigationStore, SignalRMuseService signalRMuseService)
    {
        _signalRMuseService = signalRMuseService;

        _username = string.Empty;
        _roomCode = string.Empty;

        CreateRoomCommand = new CreateRoomCommand(this, navigationStore, signalRMuseService);
        JoinRoomCommand = new JoinRoomCommand(this, navigationStore, _signalRMuseService);
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

}