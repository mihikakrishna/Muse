using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MuseClient.Views;

public partial class JoinRoomWindow : UserControl
{
    public JoinRoomWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}