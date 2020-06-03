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
        TaskWindow(TaskModel tm)
        {
            InitializeComponent();
            TVM = new TaskViewModel(tm);
            DataContext = TVM;
        }

    }
}
