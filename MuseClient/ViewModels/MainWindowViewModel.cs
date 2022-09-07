using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
}