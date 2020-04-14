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
        private string email;
        private string name;
        private int limit;
        private int size;
        private List<string> tasks;

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
        
        private List<string> ChengeToString(List<Task> tasks)
        {
            List<string> output = new List<string>();
            foreach(Task task in tasks)
            {
                output.Add(task.toJson());
            }
            return output;
        }

        public int GetLimit()
        {
            return limit;
        }
        public string getEmail() { return email; }
        public string getName() { return name; }

        public List<Task> getTasks()
        {
            List<Task> output = new List<Task>();
            foreach (string str in tasks)
            {
                output.Add(JsonSerializer.Deserialize<Task>(str));
            }
            return output;
        }
        private List<string> getTaskString()
        {
            return tasks;
        }

        public override Column fromJson(string filename)
        {
            string objetAsJson = File.ReadAllText(filename);
            Column temp = JsonSerializer.Deserialize<Column>(objetAsJson);
            this.limit = temp.GetLimit();
            this.tasks = temp.getTaskString();
            size = tasks.Count();
            return this;
        }

        public override string toJson()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
