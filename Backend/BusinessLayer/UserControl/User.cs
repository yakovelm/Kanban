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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        public User(string email, string password, string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
        }
        public string getemail()
        {
            return this.email;
        }
        public Boolean isMatchPassword(string password)
        {
            return this.password.Equals(password);
        }
        public Boolean isMatchEmail(string email)
        {
            return this.email.Equals(email);
        }
        public string getnickname()
        {
            return this.nickname;
        }
        public DAL.User ToDalObject()
        {
            log.Debug("converting user to DAL obj for " + email + ".");
            return new DAL.User(email, password, nickname);
        }

        public void Save()
        {
            try
            {
                log.Debug("saving user to hard drive for " + email + ".");
                DAL.User DU = ToDalObject();
                DU.Write("JSON\\" + email + "\\" + email + ".json", DU.toJson());
            }
            catch (Exception e)
            {
                log.Error("faild to write to file due to " + e.Message);
                throw e;
            }
        }

        public void FromDalObject(DAL.User DalObj)
        {
            try
            {
                log.Debug("converting user from DAL obj for " + email + ".");
                this.email = DalObj.getEmail();
                this.password = DalObj.getPassword();
                this.nickname = DalObj.getNickname();
            }
            catch (Exception e)
            {
                log.Error("issue converting user DAL object to user BL object due to " + e.Message);
                throw e;
            }
        }

        public void Load()
        {
            try
            {
                DAL.User DU = new DAL.User(email);
                log.Debug("user " + DU.getNickname() + "loading from hard drive for " + email+".");
                DU.fromJson("JSON\\" + email + "\\" + email + ".json");
                FromDalObject(DU);
            }
            catch (Exception e)
            {
                log.Error("failed to load user from file due to " + e.Message);
                throw e;
            }
        }
    }
}
