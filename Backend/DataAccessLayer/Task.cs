using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using IntroSE.Kanban.Backend.DataAccessLayer.DALControllers;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Task : DalObject<Task>
    {
        public const string HostAtt=DB.TaskDBName1;
        public const string IDAtt = DB.TaskDBName2;
        public const string ColumnAtt = DB.TaskDBName3;
        public const string TitleAtt = DB.TaskDBName4;
        public const string DescAtt = DB.TaskDBName5;
        public const string DueAtt = DB.TaskDBName6;
        public const string createAtt = DB.TaskDBName7;
        public long ID { get; }
        public string Cname { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public long Due { get; set; }
        public long Create { get; set; }
        public long HostID { get; set; }
        public Task(string Email,long ID, int HostID, string Cname, string Title, string Desc, long Due, long Cre) : base(new TaskCtrl())
        {
            this.Email = Email;
            this.ID = ID;
            this.Cname = Cname;
            this.Title = Title;
            this.Desc = Desc;
            this.Due = Due;
            this.Create = Cre;
            this.HostID = HostID;
        }
        protected override string MakeFilter()
        {
            return $"WHERE {HostAtt}={HostID} AND {IDAtt}={ID}";
        }
        public Task() : base(new TaskCtrl())
        {
            log.Debug("creating DAL task");
        }
        public List<Task> GetAllTasks(int host,string Cname)
        {
            List<Task> c= controller.Select($"WHERE {HostAtt}={host} AND {ColumnAtt}='{Cname}'");
            log.Debug(c.Count());
            return c;
        }
        public void UpdateTitle(string t)
        {
            if (!controller.Update(MakeFilter(), TitleAtt, t))
            {
                log.Error("fail to updata the limit for task " + Cname + " on email " + Email);
                throw new Exception("fail to updata the limit for task " + Cname + " on email " + Email);
            }
        }
        public void UpdateDesc(string d)
        {
            if (!controller.Update(MakeFilter(), DescAtt, d))
            {
                log.Error("fail to updata the ordinal for task " + Cname + " on email " + Email);
                throw new Exception("fail to updata the ordinal for task " + Cname + " on email " + Email);
            }
        }
        public void UpdateColumn(string c)
        {
            if (!controller.Update(MakeFilter(), ColumnAtt, c))
            {
                log.Error("fail to updata the name for task " + Cname + " on email " + Email);
                throw new Exception("fail to updata the name for task " + Cname + " on email " + Email);
            }
        }
        public void UpdateDue(long d)
        {
            if (!controller.Update(MakeFilter(), DueAtt, d))
            {
                log.Error("fail to updata the name for task " + Cname + " on email " + Email);
                throw new Exception("fail to updata the name for task " + Cname + " on email " + Email);
            }
        }
        public void Add()
        {
            if (!controller.Insert(this))
            {
                log.Error("fail to add new task for email " + Email);
                throw new Exception("fail to add new task for email " + Email);
            }
        }
    }
}
