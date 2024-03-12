using Avalonia.Controls;
using Avalonia.Input;

namespace Explorer;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void DoubleTappedHandler(object sender, TappedEventArgs args)
    {
        if (sender is ListBox listBox)
        {
            Explorer? explorer = (Explorer?) DataContext;

            if (listBox.SelectedItem is CollectionElement element)
                explorer.CurrentPath = element.Name;
        }
    }
}