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
        public const string emailAtt = "email";
        public const string passwordAtt = "password";
        public const string nicknameAtt = "nickname";

        public string email
        {
            get => email; set
            {
                log.Warn("email can not be chenged.");
                throw new Exception("email can not be chenged.");
            }
        }
        public string password
        {
            get => email; set
            {
                log.Warn("password can not be chenged.");
                throw new Exception("password can not be chenged.");
            }
        }
        public string nickname
        {
            get => email; set
            {
                log.Warn("nickname can not be chenged.");
                throw new Exception("nickname can not be chenged.");
            }
        }
        public User(string email, string password, string nickname):base(new UserCtrl())
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
        }
        public User(): base(new UserCtrl()) { }
        protected override string MakeFilter()
        {
            return $"WHERE {emailAtt}='{email}'";
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
            if (!controller.Delete(""))
            {
                log.Error("fail to detele alldata");
                throw new Exception("fail to add new column for email " + Email);
            }
        }
    }
}
