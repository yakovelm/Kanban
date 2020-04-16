using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Task : DalObject<Task>
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int ID { get; set; }
        public DateTime Due { get; set; }
        public DateTime Creation { get; set; }

        public Task(string email, string title, string desc, int id, DateTime due, DateTime creation)
        {
            Email = email;
            Title = title;
            ID = id;
            Desc = desc;
            Due = due;
            Creation = creation;
        }
        public Task() { }

        public override string toJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }

        public override Task fromJson(string filename)// we dont use with this method
        {
            return null;
        }
    }
}
