using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;
using System.IO;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    public class User : IPersistentObject<DAL.User>
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
        public User(string email) { this.email = email; }
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
            return new DAL.User(email, password, nickname);
        }

        public void Save()
        {
            DAL.User DU = ToDalObject();
            DU.Write("JSON\\" + email + ".json", DU.toJson());
        }

        public void FromDalObject(DAL.User DalObj)
        {
            email = DalObj.getEmail();
            password = DalObj.getPassword();
            nickname = DalObj.getNickname();
        }

        public void Load()
        {
            DAL.User DU = new DAL.User(email);
            DU.fromJson("JSON\\" + email + ".json");
            FromDalObject(DU);
        }
    }
}
