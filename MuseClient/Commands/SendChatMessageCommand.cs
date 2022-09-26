using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.ViewModels;
using MuseDomain.Models;

namespace MuseClient.Commands;
public class SendChatMessageCommand : ICommand
{
    private readonly ListenTogetherWindowViewModel _viewModel;
    private readonly SignalRMuseService _museService;

    public SendChatMessageCommand(ListenTogetherWindowViewModel viewModel, SignalRMuseService museService)
    {
        _viewModel = viewModel;
        _museService = museService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        try
        {
            var chatMessage = new ChatMessage(
                message: _viewModel.ChatInput,
                username: _viewModel.Username,
                roomCode: _viewModel.RoomCode,
                timestamp: DateTime.UtcNow);
            await _museService.SendMessage(chatMessage);
            _viewModel.ChatInput = string.Empty;
            _viewModel.ErrorMessage = string.Empty;
        }
        catch (Exception)
        {
            _viewModel.ErrorMessage = "Unable to send message.";
        }
    }
}
