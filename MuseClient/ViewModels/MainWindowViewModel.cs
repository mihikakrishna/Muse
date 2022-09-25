using MuseClient.Services;
using MuseClient.Stores;
using ReactiveUI;

namespace MuseClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly SignalRMuseService _signalRMuseService;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    public MainWindowViewModel(NavigationStore navigationStore, SignalRMuseService signalRMuseService)
    {
        _navigationStore = navigationStore;
        _signalRMuseService = signalRMuseService;
        _navigationStore.CurrentViewModelIsChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        this.RaisePropertyChanged(nameof(CurrentViewModel));
    }
}