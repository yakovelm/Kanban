﻿using System;
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
            log.Debug("create usercontroller");
            this.ActiveUser = null;
        }
        public User get_active() { return ActiveUser; }
        public void register(string email, string password, string nickname)
        {
            if (email == null || password == null || nickname == null) 
            {
                log.Warn("attempt to register with non null values.");
                throw new Exception("must register with non null values."); 
            }
            if (email.Equals("") || password.Equals("") || nickname.Equals("")) 
            {
                log.Warn("attempt to register with non empty values");
                throw new Exception("must register with non empty values");
            }
            if (checkUser(email)) 
            {
                log.Warn("attempt to sign up with a registered email");
                throw new Exception("this email is already taken.");
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{4,20}");
            var hasLowerChar = new Regex(@"[a-z]+");
            if (!hasNumber.IsMatch(password) | !hasUpperChar.IsMatch(password) | !hasMiniMaxChars.IsMatch(password) | !hasLowerChar.IsMatch(password)) 
            {
                log.Warn("attempt to register with a weak password");
                throw new Exception("must include at least one uppercase letter, one small character and a number."); 
            }
            string path = Directory.GetCurrentDirectory();
            string pathString = System.IO.Path.Combine(path,"JSON", email);
            System.IO.Directory.CreateDirectory(pathString);
            User NU = new User(email, password, nickname);
            NU.Save();
            log.Info(NU.getnickname() + " user created");
            list.Add(NU);
        }
        public void login(string email,string password)
        {
            if (ActiveUser != null)
            {
                log.Warn("login user attemt to login: "+ActiveUser.getnickname());
                throw new Exception("user already login");
            }
            if (email == null || password == null) 
            {
                log.Warn("user tried to login without the required values");
                throw new Exception("must login with non null values"); }
            foreach(User u in list)
            {
                if (u.isMatchEmail(email))
                {
                    log.Debug("email" + email + "exist in the sysmtem");
                    if (u.isMatchPassword(password))
                    {
                        log.Debug("given password match");
                        ActiveUser = u;
                        log.Info(ActiveUser.getnickname() + " login");
                    }
                    else
                    {
                        log.Warn(u.getnickname()+" tried to login with incorrect password");
                        throw new Exception("invaild password");
                    }
                }
            }
            if (ActiveUser == null)
            {
                log.Warn("user tried to login with an unregistered email");
                throw new Exception("The user does not exist");
            }
            
        }
        public void logout(string email)
        {
            if(ActiveUser==null) 
            {
                log.Warn("unlogin user tried to logout");
                throw new Exception("user not login"); 
            }
            if (!email.Equals(ActiveUser.getemail()))
            {
                log.Warn("attempt to logout, given email is not equals to the login user's email");
                throw new Exception("given email is invalid");
            }
            else
            {
                log.Debug(ActiveUser.getnickname() + " logout");
                ActiveUser = null;
            }
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
                log.Debug("user list loaded");
                list.Add(u);
            }
        }
    }
}
