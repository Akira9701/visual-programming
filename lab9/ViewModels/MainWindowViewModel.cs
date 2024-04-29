using Avalonia.Media.Imaging;
using ReactiveUI;
using System.Reactive;

namespace Volume.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Icon = new Bitmap(@"C:\SIBSUITS\visual-programming\lab9\Assets\volume.png");
        }

        public Bitmap Icon { get; }
    }
}
