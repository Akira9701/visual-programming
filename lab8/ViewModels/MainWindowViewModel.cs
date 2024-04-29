using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace lab8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object? _content;
        private ObservableCollection<ViewModelBase> _viewModelBases;
        public object? Content
        {
            get => _content;
            set
            {
                this.RaiseAndSetIfChanged(ref _content, value);
            }
        }
        public MainWindowViewModel()
        {
            _viewModelBases = new ObservableCollection<ViewModelBase>();
            _viewModelBases.Add(new DataGridViewModela());
            _viewModelBases.Add(new TreeViewViewModel());
            _content = "ip-217";
            ChangeViewCommand = ReactiveCommand.Create<ViewModelBase>(ChangeView);
        }
        public ReactiveCommand<ViewModelBase, Unit> ChangeViewCommand { get; }
        private void ChangeView(ViewModelBase viewModel)
        {
            Content = viewModel;
        }

        public ObservableCollection<ViewModelBase> viewModelBases
        {
            get => _viewModelBases;
            set
            {
                this.RaiseAndSetIfChanged(ref _viewModelBases, value);
            }
        }
    }
}
