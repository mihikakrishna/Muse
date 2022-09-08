using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Avalonia.Threading;
using Avalonia;
using MuseClient.Commands;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace MuseClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ChatViewModel ChatViewModel { get; set; }
        public ObservableCollection<string> Greeting { get; set; }
        public MainWindowViewModel(ChatViewModel chatViewModel)
        {
            ChatViewModel = chatViewModel;
        }
    }
}
