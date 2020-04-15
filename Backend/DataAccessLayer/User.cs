using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class User : DalObject<User>
    {
        private string email;
        private string password;
        private string nickname;

        public User(string email) 
        {
            this.email = email;
        }
        public User(string email,string password,string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
        }
        public string getEmail() { return email; }
        public string getNickname() { return nickname; }
        public string getPassword() { return password; }
        public override User fromJson(string filename)
        {
            string objectAsJson = File.ReadAllText(filename);
            User temp = JsonSerializer.Deserialize<User>(objectAsJson);
            return this;
        }

        public override string toJson()
        {
            Console.WriteLine(email + " " + password + " " + nickname);
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
