using MuseClient.Services;
using MuseClient.Stores;
using ReactiveUI;

namespace MuseClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelIsChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        this.RaisePropertyChanged(nameof(CurrentViewModel));
    }
}