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

        private List<Task> tasks;


        public string Cname { get => Cname; set { Cname = value; UpdateName(value); } }
        public long Ord { get => Ord; set { Ord = value; UpdateOrd(value); } }
        public long Limit { get => Limit; set { Limit = value; UpdateLimit(value); } }
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

        public void LoadTasks()
        {
            Task temp = new Task();
            tasks = temp.GetAllTasks(Email, Cname);
        }
        public Column load()
        {
            List<Column> res = controller.Select(MakeFilter());
            if (res.Count > 1) throw new Exception("found 2 matching Columns.");
            if (res.Count < 0) throw new Exception("ok i have no idea. we fucked up.");
            if (res.Count == 0) throw new Exception("task failed to load Column from DB.");
            return res[0];
        }
        public List<Task> getTasks() { return tasks; }

        public void UpdateLimit(long limit)
        {
           if(!controller.Update(MakeFilter(), LimitAtt, limit))
            {
                log.Error("fail to updata the limit for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the limit for column " + Cname + " on email "+Email);
            }
        }
        public void UpdateOrd(long ord)
        {
            if (!controller.Update(MakeFilter(), OrdAtt, ord))
            {
                log.Error("fail to updata the ordinal for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the ordinal for column " + Cname + " on email " + Email);
            }
        }
        public void UpdateName(string name)
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
    }
}
