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
        private int ID;
        private string title;
        private string desc;
        private DateTime due;
        private DateTime creation;
        private string email;

        public Task(int ID, string title, string desc, DateTime due, string email)
        {
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
            this.title = title;
        }
        public void editDesc(string desc)
        {
            this.desc = desc;
        }
        public void editDue(DateTime due)
        {
            this.due = due;
        }

        DAL.Task IPersistentObject<DAL.Task>.ToDalObject()
        {
            DAL.Task ret = new DAL.Task();
            return ret;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
