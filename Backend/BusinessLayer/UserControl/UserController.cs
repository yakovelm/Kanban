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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private User ActiveUser;
        private List<User> list;

        public UserController()
        {
            log.Debug("createing user controller.");
            this.ActiveUser = null;
        }
        public User get_active() { return ActiveUser; }
        public void register(string email, string password, string nickname)
        {
            if (email == null || password == null || nickname == null)
            {
                log.Warn("attempted to register with at least one null value.");
                throw new Exception("must register with non null values.");
            }
            if (email.Equals("") || password.Equals("") || nickname.Equals(""))
            {
                log.Warn("attempted to register with at least one empty value.");
                throw new Exception("must register with non empty values.");
            }
            email = email.ToLower();
            checkEmail(email);
            if (!checkUser(email, nickname))
            {
                log.Warn("attempted to register with a taken email.");
                throw new Exception("this email is already taken.");
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{4,20}");
            var hasLowerChar = new Regex(@"[a-z]+");
            if (!hasNumber.IsMatch(password) | !hasUpperChar.IsMatch(password) | !hasMiniMaxChars.IsMatch(password) | !hasLowerChar.IsMatch(password))
            {
                log.Warn("password too weak. must include at least one uppercase letter, one lowercase letter and a number.");
                throw new Exception("must include at least one uppercase letter, one lowercase letter and a number.");
            }
            string path = Directory.GetCurrentDirectory();
            string pathString = System.IO.Path.Combine(path, "JSON", email);
            System.IO.Directory.CreateDirectory(pathString);
            User NU = new User(email, password, nickname);
            NU.Save();
            log.Info(NU.getnickname() + " user created");
            list.Add(NU);
        }
        public void login(string email, string password)
        {
            if (ActiveUser != null)
            {
                log.Warn("a login was attempted while a user is already logged in.");
                throw new Exception("user already login");
            }
            if (email == null || password == null)
            {
                log.Warn("user tried to login with at least one null value.");
                throw new Exception("must login with non null values");
            }
            email = email.ToLower();
            checkEmail(email);
            foreach (User u in list)
            {
                if (u.isMatchEmail(email))
                {
                    log.Debug("email" + email + " exists in the sysmtem.");
                    if (u.isMatchPassword(password))
                    {
                        log.Debug("given password matches.");
                        ActiveUser = u;
                        log.Info(ActiveUser.getemail() + " has successfully logged in.");
                        ActiveUser.Login();
                    }
                    else
                    {
                        log.Warn(u.getemail() + " tried to login with incorrect password.");
                        throw new Exception("invaild password");
                    }
                }
            }
            if (ActiveUser == null)
            {
                log.Warn("user not yet registered.");
                throw new Exception(email + "," + password);
            }

        }
        public void logout(string email)
        {
            if (ActiveUser == null)
            {
                log.Warn("no user logged in. logout failed.");
                throw new Exception("user not login");
            }
            email = email.ToLower();
            if (!email.Equals(ActiveUser.getemail()))
            {
                log.Warn("a user that is not logged in attempted to log out. logout failed.");
                throw new Exception("given email is invalid");
            }
            else
            {
                ActiveUser.Logout();
                log.Debug(ActiveUser.getemail() + " logged out.");
                ActiveUser = null;
            }
        }
        private Boolean checkUser(string email, string nickname)
        {
            foreach (User u in list)
            {
                if (u.getemail().Equals(email) | u.getnickname().Equals(nickname))
                {
                    return false;
                }
            }
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
                log.Debug("user list has been loaded.");
                list.Add(u);
                if (u.IsLog() & ActiveUser == null)
                {
                    ActiveUser = u;
                    log.Info(ActiveUser.getemail() + " has successfully logged in.");
                }
                else if (u.IsLog())
                { throw new Exception("two or more user logged in."); }
            }
        }
        private void checkEmail(string s)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(s);
                if (addr.Address != s)
                {

                    throw new Exception("email adress invalid");
                }
            }
            catch (Exception e)
            {
                log.Debug("email is invalid");
                throw new Exception("email adress invalid");
            }
        }
    }
}
