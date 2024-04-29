using System;
using System.Collections.Generic;
using lab8.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;

namespace lab8.ViewModels
{
	public class TreeViewViewModel : ViewModelBase
    {
        private ObservableCollection<Users> users;
        public ObservableCollection<Users> Users
        {
            get => users;
            set => this.RaiseAndSetIfChanged(ref users, value);
        }

        public HTTPRequestViewModel HttpClientViewModel { get; set; }

        public TreeViewViewModel()
        {
            users = new ObservableCollection<Users>();
            HttpClientViewModel = new HTTPRequestViewModel();

            LoadData();
        }

        public async Task LoadData()
        {
            List<Users>? users = await HttpClientViewModel.GetUsers();
            if (users != null)
            {
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
        }
    }
}