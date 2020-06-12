using KanbanUI.Model;
using KanbanUI.ViewModel;
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

namespace KanbanUI.View
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        TaskViewModel TVM;
        public TaskWindow( UserModel um)
        {
            InitializeComponent();
            TVM = new TaskViewModel(um);
            DataContext = TVM;
        }
        public TaskWindow(TaskModel tm,UserModel um)
        {
            InitializeComponent();
            TVM = new TaskViewModel(tm,um);
            DataContext = TVM;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (TVM.apply())
            {
                Close();
            }
        }
    }
}
