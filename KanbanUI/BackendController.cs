using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using KanbanUI.Model;

namespace KanbanUI
{
    public class BackendController
    {
        private Service s; 
        public BackendController() {
            s = new Service();
        }
        public UserModel Login(string email, string password)
        {
            Response res = s.Login(email,password) ;
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new UserModel(this, email);
            
        }

        
    }
}
