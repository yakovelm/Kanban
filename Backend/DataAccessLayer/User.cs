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
        public string email { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }

        public User(string email)
        {
            this.email = email;
        }
        public User() { }
        public User(string email, string password, string nickname)
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
            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + filename))
            {
                string objectAsJson = read(filename);
                User temp = JsonSerializer.Deserialize<User>(objectAsJson);
                this.email = temp.email;
                this.password = temp.password;
                this.nickname = temp.nickname;
                return this;
            }
            else throw new Exception("Json file does not exist.");
        }

        public override string toJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
