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
        public const string NameAtt = DB.ColumnDBName2;
        public const string OrdAtt = DB.ColumnDBName3;
        public const string LimitAtt = DB.ColumnDBName4;
        private List<Task> tasks;
        public string Cname { get; set; }
        public long Ord { get; set; }
        public long Limit { get; set; }
        public Column(string Email, string Cname, long Ord, long Limit) : base(new ColumnCtrl())
        {
            this.Email = Email;
            this.Cname = Cname;
            this.Ord = Ord;
            this.Limit = Limit;
            tasks = new List<Task>();
        }
        public Column(string Email, string Cname): base(new ColumnCtrl()) //filter only constructor for loading a single column
        {
            this.Email = Email;
            this.Cname = Cname;
        }

        protected override string MakeFilter()
        {
            return $"WHERE {EmailAtt}='{Email}' AND {NameAtt}='{Cname}'";
        }

        public Column() : base(new ColumnCtrl()) { } //empty constructor for loading all column data
        public List<Column> GetAllColumns(string email)
        {
            List<Column> output = controller.Select($"WHERE {EmailAtt}='{email}'");
            foreach (Column c in output)
            {
                c.LoadTasks();
                log.Debug("loaded tasks for column: "+c.Cname+" "+c.getTasks().Count());
            }
            log.Debug("columns loaded for " + email + " with size: " + output.Count());
            return output;
        }
        public void LoadTasks()
        {
            Task temp = new Task();
            tasks = temp.GetAllTasks(Email,Cname);
        }
        public List<Task> getTasks() { return tasks; }
        public void UpdateLimit(long limit)
        {
           if(!controller.Update(MakeFilter(), LimitAtt, limit))
            {
                log.Error("failed to update the limit for column " + Cname + " on email " + Email);
                throw new Exception("failed to update the limit for column " + Cname + " on email "+Email);
            }
        }
        public void UpdateOrd(long ord)
        {
            if (!controller.Update(MakeFilter(), OrdAtt, ord))
            {
                log.Error("failed to update the ordinal for column " + Cname + " on email " + Email);
                throw new Exception("failed to update the ordinal for column " + Cname + " on email " + Email);
            }
        }
        public void Add()
        {
            
            if (!controller.Insert(this))
            {
                log.Error("failed to add new column for email " + Email);
                throw new Exception("failed to add new column for email " + Email);
            }
        }
        public void Delete()
        {

            if (!controller.Delete(MakeFilter()))
            {
                log.Error("failed to delete a column from email " + Email);
                throw new Exception("failed to delete a column from email " + Email);
            }
        }
    }
}
