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

        public Task(string Email, long ID, string Cname, string Title, string Desc, long Due, long Cre) : base(new TaskCtrl())
        {
            this.Email = Email;
            this.ID = ID;
            this.Cname = Cname;
            this.Title = Title;
            this.Desc = Desc;
            this.Due = Due;
            this.Create = Cre;
        }

        protected override string MakeFilter()
        {
            return $"WHERE {EmailAtt}='{Email}' AND {IDAtt}={ID}";
              
        }

        public Task() : base(new TaskCtrl())
        {
        }

        public List<Task> GetAllTasks(string email,string Cname)
        {
            List<Task> c= controller.Select($"WHERE {EmailAtt}='{email}' AND {ColumnAtt}='{Cname}'");
            log.Debug(c.Count());
            return c;
        }

        public Task load()
        {
            List<Task> res = controller.Select(MakeFilter());
            if (res.Count > 1) throw new Exception("found 2 matching tasks.");
            if (res.Count<0) throw new Exception("ok i have no idea. we fucked up.");
            if (res.Count==0) throw new Exception("task failed to load task from DB.");
            return res[0];
        }
        public void UpdateTitle(string t)
        {
            if (!controller.Update(MakeFilter(), TitleAtt, t))
            {
                log.Error("fail to updata the limit for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the limit for column " + Cname + " on email " + Email);
            }
        }
        public void UpdateDesc(string d)
        {
            if (!controller.Update(MakeFilter(), DescAtt, d))
            {
                log.Error("fail to updata the ordinal for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the ordinal for column " + Cname + " on email " + Email);
            }
        }
        public void UpdateColumn(string c)
        {
            if (!controller.Update(MakeFilter(), ColumnAtt, c))
            {
                log.Error("fail to updata the name for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the name for column " + Cname + " on email " + Email);
            }
        }
        public void UpdateDue(long d)
        {
            if (!controller.Update(MakeFilter(), DueAtt, d))
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
        //public string Email { get; set; } // json serialiser requires all relevant fields be public with get/set attributes
        //public string Title { get; set; }
        //public string Desc { get; set; }
        //public int ID { get; set; }
        //public DateTime Due { get; set; }
        //public DateTime Creation { get; set; }

        //public Task(string email, string title, string desc, int id, DateTime due, DateTime creation) // regular constructor for saving tasks
        //{
        //    Email = email;
        //    Title = title;
        //    ID = id;
        //    Desc = desc;
        //    Due = due;
        //    Creation = creation;
        //}
        //public Task() { } // json package requires an empty constructor
        //// since tasks are never individually loaded there is no load spscific constructor
        //public override string toJson() // convert this object to a json format string
        //{
        //    return JsonSerializer.Serialize(this, this.GetType());
        //}

        //public override Task fromJson(string filename) // empty function to implement DalObject, not relevant for task since it is saved alongside column
        //{
        //    return null;
        //}
    }
    }
