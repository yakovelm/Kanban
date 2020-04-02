using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Task : IPersistentObject
    {
        private int ID;
        private string title;
        private string desc;
        private DateTime due;
        private DateTime creation;
        private string email;

        internal Task(int ID, string title, string desc, DateTime due, string email) 
        {
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
        public int getID()
        {
            return ID;
        }

        public void ToDalObject() { throw new NotImplementedException(); }

        public void editTitle(string title)
        {
            this.title = title;
        }
        public void editDesc(string desc)
        {
            this.desc = desc;
        }
        void editDue(DateTime due)
        {
            this.due = due;
        }

    }
}
