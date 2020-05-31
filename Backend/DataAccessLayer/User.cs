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
        public const string emailHostAtt = DB.UserDBName4;
        public const string UIDAtt = DB.UserDBName5;

        public string password;
        public string nickname { get; }
        public long emailHost { get; }
        public long UID { get; }
        public User(long UID,string email, string password, string nickname,long emailHost):base(new UserCtrl())
        {
            Email = email;
            this.password = password;
            this.nickname = nickname;
            this.emailHost = emailHost;
            this.UID = UID;
        }
        protected override string MakeFilter() //make a filter for specific user
        {
            return $"WHERE {EmailAtt}='{Email}'";
        }
        public void Insert()
        {
            if (!controller.Insert(this))
            {
                log.Error("fail to save user for email " + Email);
                throw new Exception("fail to save user for email " + Email);
            }
        }
    }
}
