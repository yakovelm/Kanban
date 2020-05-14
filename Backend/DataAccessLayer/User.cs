using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DALControllers;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class User : DalObject<User>
    {
        public const string passwordAtt = DB.UserDBName2;
        public const string nicknameAtt = DB.UserDBName3;

        public string email;
        public string password;
        public string nickname;
        public User(string email, string password, string nickname):base(new UserCtrl())
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
        }
        public User(): base(new UserCtrl()) { }
        protected override List<Tuple<string, string>> MakeFilter()
        {
            List<Tuple<string, string>> output = new List<Tuple<string, string>>();
            output.Add(Tuple.Create(EmailAtt, "'" + Email + "'"));
            return output;
        }
        public void Insert()
        {
            if (!controller.Insert(this))
            {
                log.Error("fail to save user for email " + Email);
                throw new Exception("fail to save user for email " + Email);
            }
        }
        public void Delete()
        {

            if (!controller.Delete(MakeFilter()))
            {
                log.Error("fail to delete user " + Email);
                throw new Exception("fail to delete user " + Email);
            }
        }
        public void DeleteAllData()
        {
            if (!controller.Delete(new List<Tuple<string, string>>()))
            {
                log.Error("fail to detele alldata");
                throw new Exception("fail to add new column for email " + Email);
            }
        }
    }
}
