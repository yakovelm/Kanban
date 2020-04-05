using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    public class User : IPersistentObject
    {
        private String email;
        private String password;
        private String nickname;

        public User()
        {
            email = null;
            password = null;
            nickname = null;
        }
        public User(string email, string password, string nickname) {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
        }
        void IPersistentObject.ToDalObject()
        {
            throw new NotImplementedException();
        }
        public string getemail()
        {
            return this.email;
        }
        public string getpassword()
        {
            return this.password;
        }
        public string getnickname()
        {
            return this.nickname;
        }
    }
}
