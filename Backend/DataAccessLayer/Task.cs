using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Task
    {
        private string Email;
        private string Title;
        private string Desc;
        private int ID;
        private DateTime Due;
        private DateTime Creation;

        public Task(string email, string title, string desc, int id, DateTime due, DateTime creation)
        {
            Email = email;
            Title = title;
            ID = id;
            Desc = desc;
            Due = due;
            Creation = creation;
        }
        
        public  string toJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
