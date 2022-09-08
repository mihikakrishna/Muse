using System;
using MuseClient.ViewModels;
using ReactiveUI;

namespace MuseClient.Stores;

public class NavigationStore : StoreBase
{
    private ViewModelBase _currentViewModel;
    public event Action? CurrentViewModelIsChanged;
    
    public NavigationStore()
    {
        _currentViewModel = new HomeWindowViewModel(this);
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        { 
            this.RaiseAndSetIfChanged(ref _currentViewModel, value);
            OnCurrentViewModelIsChanged();
        }
    }

    private void OnCurrentViewModelIsChanged()
    {
        CurrentViewModelIsChanged?.Invoke();
    }
}