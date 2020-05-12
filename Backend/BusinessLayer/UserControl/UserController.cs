using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IntroSE.Kanban.Backend.DataAccessLayer.DALControllers;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    class UserController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int MaxLength = 25;
        private const int MinLength = 5;
        private User ActiveUser;
        private List<User> list;
        private bool Load;

        public UserController()
        {
            log.Debug("createing user controller.");
            this.ActiveUser = null;
            this.Load = false;
        }
        public User get_active() { return ActiveUser; }
        public void register(string email, string password, string nickname) // register a new user
        {
            if (Load)
            {
                NullCheck(email, password, nickname);
                email = email.ToLower();
                checkEmail(email);
                checkUser(email, nickname);
                checkPassword(password);
                log.Debug("register values are legal.");
                save(email, password, nickname);
            }
        }
        private void NullCheck(params object[] s) // checks if any of the given parameters are null or empty
        {
            foreach(object check in s)
            {
                if(check==null)
                {
                    log.Warn("entered at least one null value.");
                    throw new Exception("must enter non null values.");
                }
                else if ((string)check == "")
                {
                    log.Warn("entered at least one empty value.");
                    throw new Exception("must enter non empty values.");
                }
            }
        }
        private void checkPassword(string password) // check if a password matches the requirements given
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            if (!hasNumber.IsMatch(password) | !hasUpperChar.IsMatch(password) | password.Length < MinLength | password.Length > MaxLength | !hasLowerChar.IsMatch(password))
            {
                log.Warn("password too weak. must include at least one uppercase letter, one lowercase letter and a number and be between 4 and 20 characters.");
                throw new Exception("must include at least one uppercase letter, one lowercase letter and a number and be between 4 and 20 characters.");
            }
        }
        private void save(string email, string password, string nickname) // saves newly registered user
        {
            User NU = new User(email, password, nickname);
            NU.Insert();
            log.Info("user created for "+ NU.getemail());
            list.Add(NU);
        }
        public void login(string email, string password) // login an existing user
        {
            if (Load)
            {
                if (ActiveUser != null)
                { // cant log in if a user is already logged in
                    log.Warn("a login was attempted while a user is already logged in.");
                    throw new Exception("user already login.");
                }
                NullCheck(email, password);
                email = email.ToLower();
                checkEmail(email);
                foreach (User u in list) // run on user list to fint correct user to login
                {
                    if (u.isMatchEmail(email))
                    {
                        log.Debug("email " + email + " exists in the system.");
                        if (u.isMatchPassword(password))
                        {
                            log.Debug("given password matches.");
                            ActiveUser = u;
                            log.Info(ActiveUser.getemail() + " has successfully logged in.");
                        }
                        else
                        {
                            log.Warn(u.getemail() + " tried to login with incorrect password.");
                            throw new Exception("invaild password.");
                        }
                    }
                }
                if (ActiveUser == null)
                {
                    log.Warn("user not yet registered.");
                    throw new Exception(email + "," + password);
                }
            }

        }
        public void logout(string email) // log out active user
        {
            if (Load)
            {
                if (ActiveUser == null) // if no active user no one can log out
                {
                    log.Warn("no user logged in. logout failed.");
                    throw new Exception("no user logged in.");
                }
                email = email.ToLower();
                if (!email.Equals(ActiveUser.getemail()))
                {
                    log.Warn("a user that is not logged in attempted to log out. logout failed.");
                    throw new Exception("given email is invalid.");
                }
                else
                {
                    log.Debug(ActiveUser.getemail() + " logged out.");
                    ActiveUser = null;
                }
            }
        }
        private void checkUser(string email, string nickname) // checks that an email is not taken upon registration
        {
            foreach (User u in list)
            {
                if (u.getemail().Equals(email))
                {
                    log.Warn("attempted to register with a taken email.");
                    throw new Exception("this email is already taken.");
                }
            }
        }
        public void LoadData() // load userlist from json files
        {
            if (!Load)
            {
                try
                {
                    list = new List<User>();
                    DataAccessLayer.DALControllers.UserCtrl DUC = new UserCtrl();
                    foreach (DataAccessLayer.User run in DUC.Select(""))
                    {
                        User u = new User();
                        u.FromDalObject(run);
                        list.Add(u);
                    }
                    Load = true;
                }
                catch (Exception e)
                {
                    log.Error("faild to load data");
                    throw new Exception("faild to load data: "+e.Message);
                }
            }
            else 
            {
                log.Warn("User attempt repeat LoadData");
                throw new Exception("Can't double loadDate");
            }
        }
        public void DeleteData()
        {
            if (Load)
            {
                if (ActiveUser != null)
                {
                    foreach (User u in list)
                    {
                        u.DeleteData();
                    }
                    list = new List<User>();
                }
            }
        }
        private void checkEmail(string s) // check that email adress matches standard email format
        {
            try
            {

                if (string.IsNullOrWhiteSpace(s)) // check that email is not null and contains no spaces
                    throw new Exception("email adress invalid.");
                
                string DomainMapper(Match match) // creates a regex map for replace function
                {
                    var idn = new IdnMapping();
                    var domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
                s = Regex.Replace(s, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
                if (!Regex.IsMatch(s,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", //stackoverflow regex
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                    throw new Exception("email adress invalid.");
            }
            catch (Exception)
            {
                log.Warn("email is invalid.");
                throw new Exception("email adress invalid.");
            }
        }
    }
}
