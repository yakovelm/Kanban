using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    public class UserController
    {
        private User ActiveUser;
        private List<User> List;

        public UserController()
        {
            this.ActiveUser = null;
            List = new List<User>();
        }
        public void register(string email, string password, string nickname)
        {
            if (email == null || password == null || nickname == null) { throw new Exception(); }
            if (email.Equals("") || password.Equals("") || nickname.Equals("")) { throw new Exception(); }
            foreach (User run in List)
            {
                if (run.getemail().Equals(email)||run.getnickname().Equals(nickname)) { throw new Exception(); }
            }
            List.Add(new User(email, password, nickname));
        }
        public void login(string email,string password)
        {
            if (email == null || password == null) { throw new Exception(); }
            foreach(User run in List)
            {
                if (run.getemail().Equals(email)&& run.getpassword().Equals(password))
                {
                    this.ActiveUser = run;
                }
            }
            if (ActiveUser == null) { throw new Exception(); }
        }
        public void logout()
        {
            ActiveUser = null;
        }
    }
}
