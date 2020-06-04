using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KanbanUI.Model;
using KanbanUI.ViewModel;

namespace KanbanUI.View
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private BoardViewModel BVM;
        public BoardWindow(UserModel u)
        {
            InitializeComponent();
            BVM = new BoardViewModel(u);
            DataContext = BVM;
        }
    }
}
