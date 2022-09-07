using MuseClient.ViewModels;
using ReactiveUI;

namespace MuseClient.Stores;

public class NavigationStore : StoreBase
{
    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    public NavigationStore()
    {
        _currentViewModel = new HomeWindowViewModel();
    }
}