using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MuseClient.Views;

public partial class CreateRoomWindow : UserControl
{
    public CreateRoomWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}