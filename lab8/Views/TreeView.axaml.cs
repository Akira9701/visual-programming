using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using lab8.ViewModels;

namespace lab8.Views;

public partial class TreeView : UserControl
{
    public TreeView()
    {
        InitializeComponent();
        DataContext = new TreeViewViewModel();
    }
}