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
        private int Tmax = 50;
        private int Dmax = 300;
        private int ID;
        private string title;
        private string desc;
        private DateTime due;
        private DateTime creation;
        private string email;

        public Task() { log.Debug("new empty task obj created for "+email); }
        public Task(int ID, string title, string desc, DateTime due, string email)
        {
            log.Info("creating new task: #"+ID+" title: "+title+" for "+email);
            if (title.Length > Tmax) {
                log.Warn("Title too long");
                throw new Exception("Title too long."); 
            }
            if (desc.Length > Dmax) {
                log.Warn("Description too long");
                throw new Exception("Description too long."); 
            }
            this.ID = ID;
            this.title = title;
            this.desc = desc;
            this.due = due;
            this.email = email;
            creation = DateTime.Now;
        }

        public string getTitle()
        {
            return title;
        }
        public string getDesc()
        {
            return desc;
        }
        public DateTime getCreation()
        {
            return creation;
        }
        public int getID()
        {
            return ID;
        }

        public void editTitle(string title)
        {
            log.Info("task #"+ID+ "title changing from " + this.title+" to "+title + " for " + email);
            if (title.Length > Tmax) {
                log.Warn("Title too long");
                throw new Exception("Title too long."); 
            }
            this.title = title;
        }
        public void editDesc(string desc)
        {
            log.Info("task #" + ID + "description changing from " + this.desc + " to " + desc + " for " + email);
            if (desc.Length > Dmax) {
                log.Warn("Description too long");
                throw new Exception("Description too long.");
            }
            this.desc = desc;
        }
        public void editDue(DateTime due)
        {
            log.Info("task #" + ID + "due date changing from " + this.due + " to " + due + " for " + email);
            if (due.CompareTo(this.creation) < 0) {
                log.Warn("new due is earlier then creation");
                throw new Exception("new due is earlier then creation"); 
            }
            this.due = due;
        }

        public DAL.Task ToDalObject()
        {
            log.Debug("task #" + ID + "converting to DAL obj in " + email);
            return new DAL.Task(email,title,desc,ID,due,creation);
        }

        public void FromDalObject(DAL.Task DalObj)
        {
            log.Debug("task #" + ID + "converting from DAL obj");
            try
            {
                email = DalObj.Email;
                title = DalObj.Title;
                ID = DalObj.ID;
                desc = DalObj.Desc;
                due = DalObj.Due;
                creation = DalObj.Creation;
            }
            catch(Exception e)
            {
                log.Error("issue converting task Dal object to task BL object due to " + e.Message);
                throw e;
            }
        }

        public void Save()
        {
        }

        public void Load()
        {
        }
    }
}
