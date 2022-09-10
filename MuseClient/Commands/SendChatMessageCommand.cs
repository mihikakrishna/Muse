using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.ViewModels;
using MuseDomain.Models;

namespace MuseClient.Commands;
public class SendChatMessageCommand : ICommand
{
    private readonly ListenTogetherWindowViewModel _viewModel;
    private readonly SignalRChatService _chatService;

    public SendChatMessageCommand(ListenTogetherWindowViewModel viewModel, SignalRChatService chatService)
    {
        _viewModel = viewModel;
        _chatService = chatService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        try
        {
            await _chatService.SendMessage(new ChatMessage(message: "Hello World!"));

            _viewModel.ErrorMessage = string.Empty;
        }
        catch (Exception)
        {
            _viewModel.ErrorMessage = "Unable to send message.";
        }
    }
}
