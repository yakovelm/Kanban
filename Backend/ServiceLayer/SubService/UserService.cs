using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.SubService
{
    class UserService
    {
        private BusinessLayer.UserControl.UserController uc;
        private string useremail;
        
        public UserService()
        {
            uc = new BusinessLayer.UserControl.UserController();
        }
        public Response login(string email,string password)
        {
            try
            {
                uc.login(email, password);
                useremail = email;
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
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
