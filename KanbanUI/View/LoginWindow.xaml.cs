using KanbanUI.Model;
using KanbanUI.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
            if (u != null)
            {
                BoardWindow boardView = new BoardWindow(u);
                boardView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                boardView.WindowState = WindowState.Maximized;
                boardView.Show();
                //boardView.BVM.LoadColumns();
                this.Close();
            }
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow reg = new RegisterWindow(LM.Controller);
            reg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
