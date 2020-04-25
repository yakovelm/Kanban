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
        public string email { get; set; } // json serialiser requires all relevant fields be public with get/set attributes
        public string name { get; set; }
        public int limit { get; set; }
        public int size { get; set; }
        public List<string> tasks { get; set; }

        public Column(string email, string name, int limit, List<Task> tasks) // regular constructor for saving data
        {
            this.email = email;
            this.name = name;
            this.limit = limit;
            this.tasks = ChengeToString(tasks);
            size = tasks.Count();
        }
        public Column(string email, string name) // partial constructor for loading data
        {
            this.email = email;
            this.name = name;
        }

        public Column() { } // json package requires an empty constructor

        private List<string> ChengeToString(List<Task> tasks) // turn task list into a slist of strings to save
        {
            List<string> output = new List<string>();
            foreach (Task task in tasks)
            {
                output.Add(task.toJson());
            }
            return output;
        }

        public List<Task> getTasks() // convert a list of strings to a list of tasks
        {
            List<Task> output = new List<Task>();
            foreach (string str in tasks)
            {
                output.Add(JsonSerializer.Deserialize<Task>(str));
            }
            return output;
        }

        public override Column fromJson(string filename) // load this objects data from a json file
        {
            string objetAsJson = read(filename);
            Column temp = JsonSerializer.Deserialize<Column>(objetAsJson);
            this.limit = temp.limit;
            this.tasks = temp.tasks;
            size = tasks.Count();
            return this;
        }

        public override string toJson() // convert this objects data to a json format string
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
