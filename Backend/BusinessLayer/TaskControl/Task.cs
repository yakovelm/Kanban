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
        private int Tmax = 50;
        private int Dmax = 300;
        private int ID;
        private string title;
        private string desc;
        private DateTime due;
        private DateTime creation;
        private string email;

        public Task() { }
        public Task(int ID, string title, string desc, DateTime due, string email)
        {
            if (title.Length > Tmax) { throw new Exception("Title too long."); }
            if (desc.Length > Dmax) { throw new Exception("Description too long."); }
            this.ID = ID;
            this.title = title;
            this.desc = desc;
            this.due = due;
            this.email = email;
            creation = DateTime.Now;
        }

        public string toString()
        {
            return ("ID: "+ID+"\ntitle: "+title+"\ndesc: "+desc+"\nemail: "+email+"\ndue date: "+due.ToString()+"\ncreation time: "+creation.ToString());
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
            if (title.Length > Tmax) { throw new Exception("Title too long."); }
            this.title = title;
        }
        public void editDesc(string desc)
        {
            if (desc.Length > Dmax) { throw new Exception("Description too long."); }
            this.desc = desc;
        }
        public void editDue(DateTime due)
        {
            if (due.CompareTo(this.creation) < 0) { throw new Exception("new due is earlier then creation"); }
            this.due = due;
        }

        public DAL.Task ToDalObject()
        {
            return new DAL.Task(email,title,desc,ID,due,creation);
        }

        public void FromDalObject(DAL.Task DalObj)
        {
            email = DalObj.getEmail();
            title = DalObj.getTitle();
            ID = DalObj.getID();
            desc = DalObj.getDesc();
            due = DalObj.getDue();
            creation = DalObj.getCreation();
        }

        public void Save()
        {
        }

        public void Load()
        {
        }
    }
}
