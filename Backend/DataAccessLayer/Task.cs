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
        public string Email { get; set; } // json serialiser requires all relevant fields be public with get/set attributes
        public string Title { get; set; }
        public string Desc { get; set; }
        public int ID { get; set; }
        public DateTime Due { get; set; }
        public DateTime Creation { get; set; }

        public Task(string email, string title, string desc, int id, DateTime due, DateTime creation) // regular constructor for saving tasks
        {
            Email = email;
            Title = title;
            ID = id;
            Desc = desc;
            Due = due;
            Creation = creation;
        }
        public Task() { } // json package requires an empty constructor
        // since tasks are never individually loaded there is no load spscific constructor
        public override string toJson() // convert this object to a json format string
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }

        public override Task fromJson(string filename) // empty function to implement DalObject, not relevant for task since it is saved alongside column
        {
            return null;
        }
    }
}
