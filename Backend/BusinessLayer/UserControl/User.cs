using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;
using System.IO;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    class User : IPersistentObject<DAL.User>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private String email;
        private String password;
        private String nickname;

        public User() { } // json package requires an empty constructor
        public User(string email) { this.email = email; } // load data constructor, needs only email
        public User(string email, string password, string nickname) // new user creation (register) constructor
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
        public DAL.User ToDalObject() // converts this object to a DataAccessLayer object
        {
            log.Debug("converting user to DAL obj for " + email + ".");
            return new DAL.User(email, password, nickname);
        }
        public void DeleteData()
        {
            DAL.User DU = ToDalObject();
            DU.DeleteAllData();
        }

        public void FromDalObject(DAL.User DalObj) // converts a DataAccessLayer object to an object of this type and sets this to the corresponding values
        {
            try
            {
                log.Debug("converting user from DAL obj for " + email + ".");
                this.email = DalObj.email;
                this.password = DalObj.password;
                this.nickname = DalObj.nickname;
            }
            catch (Exception e)
            {
                log.Error("issue converting user DAL object to user BL object due to " + e.Message);
                throw e;
            }
        }
        public void Insert() 
        {
            DAL.User DU = ToDalObject();
            DU.Insert();
        }
    }
}
