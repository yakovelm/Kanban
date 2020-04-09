using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    class User : IPersistentObject<DAL.User>
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

        public DAL.User ToDalObject()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        void IPersistentObject<DAL.User>.FromDalObject()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }
    }
}
