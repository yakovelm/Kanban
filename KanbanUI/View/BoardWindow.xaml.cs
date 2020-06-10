using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public BoardViewModel BVM;
        public BoardWindow(UserModel u)
        {
            InitializeComponent();
            BVM = new BoardViewModel(u);
            DataContext = BVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BVM.logout();
            LoginWindow l = new LoginWindow();
            l.Show();
            this.Close();
        }



        //private void left_Click(object sender, RoutedEventArgs e)
        //{
        //    ColumnModel col=(ColumnModel)((Button)sender).DataContext;
        //    col.moveLeft();
        //}

        //private void right_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
