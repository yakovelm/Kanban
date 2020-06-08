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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KanbanUI.Model;
using KanbanUI.ViewModel;

namespace KanbanUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginViewModel LM;
        public LoginWindow()
        {
            InitializeComponent();
            LM = new LoginViewModel();
            DataContext = LM;
        }
        public LoginWindow(BackendController controller)
        {
            InitializeComponent();
            LM = new LoginViewModel(controller);
            DataContext = LM;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserModel u = LM.Login();
            if (u != null) {
                BoardWindow boardView = new BoardWindow(u);
                boardView.Show();
                //boardView.BVM.LoadColumns();
                this.Close();
            }
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow reg = new RegisterWindow(LM.Controller);
            reg.ShowDialog();
            //this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Reset_click(object sender, RoutedEventArgs e)
        {
            LM.Reset();
        }
    }
}
