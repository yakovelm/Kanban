using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UC = IntroSE.Kanban.Backend.BusinessLayer.UserControl;

namespace IntroSE.Kanban.Backend.ServiceLayer.SubService
{
    class UserService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private BusinessLayer.UserControl.UserController uc;

        public UserService() 
        {
            uc = new BusinessLayer.UserControl.UserController();
        }
        public Response<User> login(string email, string password) // login an existing user
        {
            try
            {
                log.Info("logging in to user " + email + ".");
                uc.login(email, password);
                return new Response<User>(new User(uc.get_active().getemail(), uc.get_active().getnickname()));
            }
            catch (Exception e)
            {
                return new Response<User>(e.Message);
            }
        }
        public Response register(string email, string password, string nickname) // register a new user
        {
            try
            {
                log.Info("registering user " + email + ".");
                uc.register(email, password, nickname);
                return new Response();
            }
            catch (Exception e)
            {
                log.Info("register failed. due to " + e.Message); // can fail due to many reasons, added catch all.
                return new Response(e.Message);
            }
        }
        public Response DeleteData()
        {
            try
            {
                log.Info("attempting to Delete Data.");
                uc.DeleteData();
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response logout(string email) // logout active user
        {
            try
            {
                log.Info("logging out user " + email + ".");
                uc.logout(email);
                return new Response();
            }
            catch (Exception e)
            {
                log.Info("login failed."); // can fail due to many reasons, added catch all.
                return new Response(e.Message);
            }
        }
        public Response LoadData() // load all user data
        {
            try
            {
                log.Info("attempting to load user list.");
                uc.LoadData();
                return new Response();
            }
            catch (Exception e) { return new Response(e.Message); }
        }
    }
}
