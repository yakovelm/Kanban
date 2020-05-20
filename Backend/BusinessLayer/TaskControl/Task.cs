using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer.TaskControl
{
    class Task : IPersistentObject<DAL.Task>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int Tmax = 50;
        private const int Dmax = 300;
        private int ID;
        private string Cname;
        private string title;
        private string desc;
        private DateTime due;
        private DateTime creation;
        private string email;
        public Task() { log.Debug("new empty task obj created for " + email); } // empty constructor for loading whole columns from json
        public Task(int ID,string Cname, string title, string desc, DateTime due, string email)
        {
            log.Info("creating new task: #" + ID + " title: " + title + " for " + email);
            if(title==null || title.Equals(""))
            {
                log.Error("title is invalid.");
                throw new Exception("title is invalid.");
            }
            if (due==null || due < DateTime.Now)
            {
                log.Error("due is invalid.");
                throw new Exception("due is invalid.");
            }
            if (title.Length > Tmax)
            {
                log.Warn("Title too long");
                throw new Exception("Title too long.");
            }
            if (desc!=null && desc.Length > Dmax)
            {
                log.Warn("Description too long");
                throw new Exception("Description too long.");
            }
            this.ID = ID;
            this.Cname = Cname;
            this.title = title;
            this.desc = desc;
            this.due = due;
            this.email = email;
            creation = DateTime.Now;
        }
        public void insert()
        {
            DAL.Task Dtask = ToDalObject();
            Dtask.Add();
        }
        public string getTitle() { return title;}
        public string getDesc() { return desc;}
        public DateTime getCreation() { return creation;}
        public int getID() { return ID;}
        public DateTime GetDue() { return due; }
        public void editColumn(string Cname) // update title of this task
        {
            log.Info("task #" + ID + "column changing from " + this.Cname + " to " + Cname + " for " + email + ".");
            this.Cname = Cname;
            DAL.Task Dtask = ToDalObject();
            Dtask.UpdateColumn(Cname);
        }
        public void editTitle(string title) // update title of this task
        {
            log.Info("task #" + ID + "title changing from " + this.title + " to " + title + " for " + email + ".");
            if (title == null || title.Length > Tmax |title.Equals(""))
            {
                log.Warn("Title too long.");
                throw new Exception("Title too long.");
            }
            this.title = title;
            DAL.Task Dtask = ToDalObject();
            Dtask.UpdateTitle(title);
        }
        public void editDesc(string desc) // update description of this task
        {
            log.Info("task #" + ID + "description changing from " + this.desc + " to " + desc + " for " + email + ".");
            if (desc != null && desc.Length > Dmax)
            {
                log.Warn("Description too long.");
                throw new Exception("Description too long.");
            }
            this.desc = desc;
            DAL.Task Dtask = ToDalObject();
            Dtask.UpdateDesc(desc);
        }
        public void editDue(DateTime due) // update due date of this task
        {
            log.Info("task #" + ID + "due date changing from " + this.due + " to " + due + " for " + email + ".");
            if (due==null || due < DateTime.Now)
            {
                log.Warn("new due is earlier then now.");
                throw new Exception("new due is earlier then now.");
            }
            this.due = due;
            DAL.Task Dtask = ToDalObject();
            Dtask.UpdateDue(due.Ticks);
        }

        public DAL.Task ToDalObject() // convert this task to a DataAccessLayer object
        {
            log.Debug("task #" + ID + "converting to DAL obj in " + email + ".");
            return new DAL.Task(email,ID,Cname,title,desc,due.Ticks,creation.Ticks);
        }

        public void FromDalObject(DAL.Task DalObj)// convert a DataAccessLayer object to a BuisnessLayer task and set this to corresponding values
        {
            log.Debug("task #" + DalObj.ID + " converting from DAL obj.");
            try
            {
                email = DalObj.Email;
                Cname = DalObj.Cname;
                title = DalObj.Title;
                ID = (int)DalObj.ID;
                desc = DalObj.Desc;
                due = new DateTime(DalObj.Due);
                creation = new DateTime(DalObj.Create);
            }
            catch (Exception e)
            {
                log.Error("issue converting task Dal object to task BL object due to " + e.Message);
                throw e;
            }
        }
    }
}
