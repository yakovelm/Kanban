using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Globalization;
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
            NullCheck(email, password, nickname);
            email = email.ToLower();
            checkEmail(email); 
            checkUser(email, nickname);
            checkPassword(password);
            log.Debug("Values of register is legal");
            save(email, password, nickname);
        }
        private void NullCheck(params object[] s)
        {
            if(s.Length==2 & ActiveUser != null)
            {
                log.Warn("a login was attempted while a user is already logged in.");
                throw new Exception("user already login");
            }
            foreach(object check in s)
            {
                if(check==null & s.Length==3)
                {
                    log.Warn("attempted to register with at least one null value.");
                    throw new Exception("must register with non null values.");
                }
                else if(check ==null)
                {
                    log.Warn("user tried to login with at least one null value.");
                    throw new Exception("must login with non null values");
                }
            }
            if(s.Length==3 && (string)s[2] == "")
            {
                log.Warn("attempted to register with at least one empty value.");
                throw new Exception("must register with non empty values.");
            }
        }
        private void checkPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            if (!hasNumber.IsMatch(password) | !hasUpperChar.IsMatch(password) | password.Length < 4 | password.Length > 20 | !hasLowerChar.IsMatch(password))
            {
                log.Warn("password too weak. must include at least one uppercase letter, one lowercase letter and a number.");
                throw new Exception("must include at least one uppercase letter, one lowercase letter and a number.");
            }
        }
        private void save(string email, string password, string nickname)
        {
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
            NullCheck(email, password);
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
                log.Debug(ActiveUser.getemail() + " logged out.");
                ActiveUser = null;
            }
        }
        private void checkUser(string email, string nickname)
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
            }
        }
        private void checkEmail(string s)
        {
            try
            {
                if (!IsValidEmail(s))
                {

                    throw new Exception("email adress invalid");
                }
            }
            catch (Exception)
            {
                log.Debug("email is invalid");
                throw new Exception("email adress invalid");
            }
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (Exception)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
