using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MuseClient.Views;

public partial class HomeWindow : UserControl
{
    public HomeWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}