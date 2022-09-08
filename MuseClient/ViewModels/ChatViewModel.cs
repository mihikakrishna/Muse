using System;
using MuseClient.Services;
using System.Windows.Input;
using MuseClient.Commands;
using MuseDomain.Models;
using System.Diagnostics;

namespace MuseClient.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        public string Messages { get; set; }
        public ICommand SendChatMessageCommand { get; }

        private string _errorMessage = string.Empty;
        private bool _isConnected;

        public ChatViewModel(SignalRChatService chatService)
        {
            SendChatMessageCommand = new SendChatMessageCommand(this, chatService);
            Messages = new string(Array.Empty<char>());

            chatService.MessageRecieved += ChatService_MessageReceived;
        }
        private void ChatService_MessageReceived(ChatMessage chatMessage)
        {
            Messages = chatMessage.Message;
            Console.WriteLine(chatMessage.Message);
        }
        public static ChatViewModel CreateConnectedViewModel(SignalRChatService chatService)
        {
            ChatViewModel viewModel = new ChatViewModel(chatService);

            chatService.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Console.WriteLine("Unable to connect to chat hub");
                }
            });

            return viewModel;
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                Console.WriteLine("Connected to Server");
            }
        }

    }
}
