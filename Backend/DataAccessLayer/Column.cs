using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Column : DalObject<Column>
    {
        public string email { get; set; }
        public string name { get; set; }
        public int limit { get; set; }
        public int size { get; set; }
        public List<string> tasks { get; set; }

        public Column(string email, string name, int limit, List<Task> tasks)
        {
            this.email = email;
            this.name = name;
            this.limit = limit;
            this.tasks = ChengeToString(tasks);
            size = tasks.Count();
        }
        public Column(string email, string name)
        {
            this.email = email;
            this.name = name;
        }

        public Column() { }

        private List<string> ChengeToString(List<Task> tasks)
        {
            List<string> output = new List<string>();
            foreach (Task task in tasks)
            {
                output.Add(task.toJson());
            }
            return output;
        }

        public List<Task> getTasks()
        {
            List<Task> output = new List<Task>();
            foreach (string str in tasks)
            {
                output.Add(JsonSerializer.Deserialize<Task>(str));
            }
            return output;
        }

        public override Column fromJson(string filename)
        {
            string objetAsJson = read(filename);
            Column temp = JsonSerializer.Deserialize<Column>(objetAsJson);
            this.limit = temp.limit;
            this.tasks = temp.tasks;
            size = tasks.Count();
            return this;
        }

        public override string toJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
