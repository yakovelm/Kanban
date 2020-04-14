using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    public class UserController
    {
        private User ActiveUser;

        public UserController()
        {
            this.ActiveUser = null;
        }
        public User get_active() { return ActiveUser; }
        public void register(string email, string password, string nickname)
        {
            if (email == null || password == null || nickname == null) { throw new Exception("must register with non null values."); }
            if (email.Equals("") || password.Equals("") || nickname.Equals("")) { throw new Exception("must register with non empty values"); }
            if (checkUser(email)) { throw new Exception("this email is already taken."); }
            if (password.Length > 20 | password.Length < 4) { throw new Exception(" A ​user password must be in length of 4 to 20 characters"); }
            int a=0, b=0, c = 0;
            for(int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 'a' & password[i] <= 'z')
                    a++;
                if (password[i] >= 'A' & password[i] <= 'Z')
                    b++;
                if (password[i] >= '0' & password[i] <= '9')
                    c++;
                if (a > 0 & b > 0 & c > 0)
                    break;
            }
            if (a == 0 | b == 0 | c == 0)
                throw new Exception("must include at least one uppercase letter, one small character and a number.");
            string path = Directory.GetCurrentDirectory();
            string pathString = System.IO.Path.Combine(path, email);
            System.IO.Directory.CreateDirectory(pathString);
                        User NU = new User(email, password, nickname);
            NU.Save();
        }
        public void login(string email,string password)
        {
            if (email == null || password == null) { throw new Exception("must login with non null values"); }
            if (checkUser(email))
            {
                User u = new User(email);
                u.Load();
                if (u.getpassword().Equals(password))
                    this.ActiveUser = u;
            }
            
        }
        public void logout()
        {
            ActiveUser = null;
        }
        private Boolean checkUser(string email) 
        { 
            string path = Directory.GetCurrentDirectory();
            if (!System.IO.Directory.Exists(path + email))
                return false;
            return true;
        }
    }
}
