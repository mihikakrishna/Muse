using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MuseClient.Views;

public partial class ListenTogetherWindow : UserControl
{
    public ListenTogetherWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}