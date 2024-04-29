using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab8.ViewModels;

namespace lab8.Views
{
    public partial class DataGrid : UserControl
    {
        public DataGrid()
        {
            InitializeComponent();
            DataContext = new DataGridViewModela();
        }
    }
}
