using KanbanUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanUI;

namespace KanbanUI.ViewModel
{
     public class LoginViewModel : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        private string _email;
        public string Email { get => _email; set { _email = value; RaisePropertyChanged("Email"); } }
        private string _password;
        public string Password { get=> _password; set { _password = value; RaisePropertyChanged("Password"); } }
        private string _massage;
        public string Message { get => _massage; set { _massage = value; RaisePropertyChanged("Message"); } }

        public LoginViewModel()
        {
            Controller = new BackendController();
        }
        public LoginViewModel(BackendController b)
        {
            Controller = b;
        }
        public UserModel Login()
        {
            Message = "";
            try
            {
                return Controller.Login(_email, _password);
            }
            catch(Exception e)
            {
                Message = e.Message;
                return null;
            }
        }
    }
}
