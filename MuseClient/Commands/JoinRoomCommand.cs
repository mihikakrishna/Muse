using System;
using System.Windows.Input;
using MuseClient.Stores;
using MuseClient.ViewModels;

namespace MuseClient.Commands;

public class JoinRoomCommand : ICommand
{
    private readonly HomeWindowViewModel _viewModel;
    private readonly NavigationStore _navigationStore;

    public JoinRoomCommand(HomeWindowViewModel viewModel, NavigationStore navigationStore)
    {
        _viewModel = viewModel;
        _navigationStore = navigationStore;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        // send roomCode to server to ValidateRoom, if valid then continue, else throw an error window
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(
            navigationStore: _navigationStore,
            username: _viewModel.Username,
            roomCode: _viewModel.RoomCode);
    }

}