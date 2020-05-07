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

        public const string IDAtt = "TID";
        public const string ColumnAtt = "Cname";
        public const string TitleAtt = "title";
        public const string DescAtt = "description";
        public const string DueAtt = "dueDate";
        public const string createAtt = "creationDate";

        public long ID { get => ID; set {
                log.Warn("task Id can not be chenged.");
                throw new Exception("task Id can not be chenged."); } }
        public string Cname { get => Cname; set { Cname = value; UpdateColumn(value); } }
        public string Title { get => Title; set { Title = value; UpdateTitle(value); } }
        public string Desc { get => Desc; set { Desc = value; UpdateDesc(value); } }
        public long Due { get => Due; set { Due = value; UpdateDue(value); } }
        public long Create { get => Create; set {
                log.Warn("Task Creation date can not be chenged.");
                throw new Exception("Task Creation date can not be chenged.");
            } }

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
            return $"{EmailAtt}='{Email}' AND {IDAtt}={ID}";
              
        }

        public Task() : base(new TaskCtrl())
        {
        }

        public List<Task> GetAllTasks(string email,string Cname)
        {
            return controller.Select($"{EmailAtt}='{email}' AND {ColumnAtt}='{Cname}'");
        }


        private void UpdateTitle(string t)
        {
            if (!controller.Update(MakeFilter(), TitleAtt, t))
            {
                log.Error("fail to updata the limit for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the limit for column " + Cname + " on email " + Email);
            }
        }
        private void UpdateDesc(string d)
        {
            if (!controller.Update(MakeFilter(), DescAtt, d))
            {
                log.Error("fail to updata the ordinal for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the ordinal for column " + Cname + " on email " + Email);
            }
        }
        private void UpdateColumn(string c)
        {
            if (!controller.Update(MakeFilter(), ColumnAtt, c))
            {
                log.Error("fail to updata the name for column " + Cname + " on email " + Email);
                throw new Exception("fail to updata the name for column " + Cname + " on email " + Email);
            }
        }
        private void UpdateDue(long d)
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
