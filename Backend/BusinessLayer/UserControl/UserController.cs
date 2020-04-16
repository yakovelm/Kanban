using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    public class UserController
    {
        private User ActiveUser;
        private List<User> list;

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
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{4,20}");
            var hasLowerChar = new Regex(@"[a-z]+");
            if (!hasNumber.IsMatch(password) | !hasUpperChar.IsMatch(password) | !hasMiniMaxChars.IsMatch(password) | !hasLowerChar.IsMatch(password)) 
            { throw new Exception("must include at least one uppercase letter, one small character and a number."); }
            string path = Directory.GetCurrentDirectory();
            string pathString = System.IO.Path.Combine(path,"JSON", email);
            System.IO.Directory.CreateDirectory(pathString);
            User NU = new User(email, password, nickname);
            NU.Save();
            list.Add(NU);
        }
        public void login(string email,string password)
        {
            if (email == null || password == null) { throw new Exception("must login with non null values"); }
            foreach(User u in list)
            {
                if (u.getemail().Equals(email))
                {
                    if (u.getpassword().Equals(password))
                    {
                        ActiveUser = u;
                    }
                    else throw new Exception("invaild password");
                }
            }
            if (ActiveUser == null)
            {
                throw new Exception("The user does not exist");
            }
            
        }
        public void logout(string email)
        {
            if(ActiveUser==null) { throw new Exception("user not login"); }
            if (!email.Equals(ActiveUser.getemail())) throw new Exception("given email is invalid");
            else ActiveUser = null;
        }
        private Boolean checkUser(string email) 
        { 
            string path = Directory.GetCurrentDirectory();
            if(!System.IO.Directory.Exists(path+"\\JSON\\" + email))
                return false;
            return true;
        }
        public void LoadData()
        {
            list = new List<User>();
            string[] users = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\JSON");
            foreach (string path in users)
            {
                var dir = new DirectoryInfo(path);
                User u = new User(dir.Name);
                u.Load();
                list.Add(u);
            }
        }
    }
}
