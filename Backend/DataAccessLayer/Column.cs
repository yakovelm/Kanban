using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using IntroSE.Kanban.Backend.DataAccessLayer.DALControllers;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Column : DalObject<Column>
    {
        public const string NameAtt = "Cname";
        public const string OrdAtt = "Ord";
        public const string LimitAtt = "Limit";

        private string Cname;
        private long Ord;
        private long Limit;
        private List<Task> tasks;


        public string cName{ get => Cname; set { Cname = value; UpdateName(value); } }
        public long Ordinal{ get => Ord; set { Ord = value; UpdateOrd(value); } }
        public long limit{ get => Limit; set { Limit = value; UpdateLimit(value); } }
        public Column(string Email, string Cname, long Ord, long Limit) : base(new ColumnCtrl())
        {
            this.Email = Email;
            this.Cname = Cname;
            this.Ord = Ord;
            this.Limit = Limit;
            tasks = new List<Task>();
        }

        protected override string MakeFilter()
        {
            return $"{EmailAtt}='{Email}' AND {NameAtt}='{Cname}'";
        }

        public Column() : base(new ColumnCtrl())
        {
        }

        public List<Column> GetAllColumns(string email)
        {
            List<Column> output = controller.Select($"{EmailAtt}='{email}'");
            foreach(Column c in output)
            {
                c.LoadTasks();
            }
            return output;
        }

        private void LoadTasks()
        {
            Task temp = new Task();
            tasks = temp.GetAllTasks(Email, Cname);
        }

        public List<Task> getTasks() { return tasks; }

        private void UpdateLimit(long limit)
        {
           if(!controller.Update(MakeFilter(), LimitAtt, limit))
            {
                log.Error("fail to updata the limit for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the limit for column " + Cname + " on email "+Email);
            }
        }
        private void UpdateOrd(long ord)
        {
            if (!controller.Update(MakeFilter(), OrdAtt, ord))
            {
                log.Error("fail to updata the ordinal for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the ordinal for column " + Cname + " on email " + Email);
            }
        }
        private void UpdateName(string name)
        {
            if (!controller.Update(MakeFilter(), NameAtt, name))
            {
                log.Error("fail to updata the name for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the name for column " + Cname + " on email " + Email);
            }
        }
        public void Add()
        {
            
            if (!controller.Insert(this))
            {
                log.Error("fail to add new column for email " + Email);
                throw new Exception("fail to add new column for email " + Email);
            }
        }
        public void Delete()
        {

            if (!controller.Delete(MakeFilter()))
            {
                log.Error("fail to add new column for email " + Email);
                throw new Exception("fail to add new column for email " + Email);
            }
        }
        public void DeleteAllData()
        {
            if (!controller.Delete(""))
            {
                log.Error("fail to add new column for email " + Email);
                throw new Exception("fail to add new column for email " + Email);
            }
        }


        // public Column() { } // json package requires an empty constructor
        //public string email { get; set; } // json serialiser requires all relevant fields be public with get/set attributes
        //public string name { get; set; }
        //public int limit { get; set; }
        //public int size { get; set; }
        //public int ord { get; set; }
        //public int CID { get; set; }
        //public List<string> tasks { get; set; }

        //public Column(string email, string name, int limit, List<Task> tasks) // regular constructor for saving data
        //{
        //    this.email = email;
        //    this.name = name;
        //    this.limit = limit;
        //    this.tasks = ChengeToString(tasks);
        //    size = tasks.Count();
        //}
        //public Column(string email, string name) // partial constructor for loading data
        //{
        //    this.email = email;
        //    this.name = name;
        //}

        //private List<string> ChengeToString(List<Task> tasks) // turn task list into a slist of strings to save
        //{
        //    List<string> output = new List<string>();
        //    foreach (Task task in tasks)
        //    {
        //        output.Add(task.toJson());
        //    }
        //    return output;
        //}

        //public List<Task> getTasks() // convert a list of strings to a list of tasks
        //{
        //    List<Task> output = new List<Task>();
        //    foreach (string str in tasks)
        //    {
        //        output.Add(JsonSerializer.Deserialize<Task>(str));
        //    }
        //    return output;
        //}

        //public override Column fromJson(string filename) // load this objects data from a json file
        //{
        //    string objetAsJson = read(filename);
        //    Column temp = JsonSerializer.Deserialize<Column>(objetAsJson);
        //    this.limit = temp.limit;
        //    this.tasks = temp.tasks;
        //    size = tasks.Count();
        //    return this;
        //}

        //public override string toJson() // convert this objects data to a json format string
        //{
        //    return JsonSerializer.Serialize(this, this.GetType());
        //}
    }
}
