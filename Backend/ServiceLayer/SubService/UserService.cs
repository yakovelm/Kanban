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
        private BusinessLayer.UserControl.UserController uc;
        private UC.User active;
        
        public UserService()
        {
            uc = new BusinessLayer.UserControl.UserController();
        }
        public Response<User> login(string email,string password)
        {
            try
            {
                uc.login(email, password);
                active = uc.get_active();
                return new Response<User>(new User(active.getemail(),active.getnickname()));
            }
            catch (Exception e)
            {
                return new Response<User>(e.Message);
            }
        }
        public Response register(string email,string password,string nickname)
        {
            try
            {

                uc.register(email, password, nickname);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response logout()
        {
            try
            {
                uc.logout();
                return new Response();
            }
            catch(Exception e)
            {
                return new Response(e.Message);
            }
        }
    }
}
