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
        
        public UserService()
        {
            uc = new BusinessLayer.UserControl.UserController();
        }
        public Response<User> login(string email,string password)
        {
            try
            {
                uc.login(email, password);
                return new Response<User>(new User(uc.get_active().getemail(), uc.get_active().getnickname()));
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
        public Response logout(string email)
        {
            try
            {
                uc.logout(email);
                return new Response();
            }
            catch(Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response LoadData()
        {
            try
            {
                uc.LoadData();
                return new Response();
            }
            catch(Exception e) { return new Response(e.Message); }
        }
    }
}
