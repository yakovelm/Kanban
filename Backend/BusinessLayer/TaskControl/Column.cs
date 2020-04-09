using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer.TaskControl
{
    class Column :IPersistentObject<DAL.Column>
    {
        private string email;
        private string name;
        private List<Task> tasks;
        private int limit;
        private int size;

        public Column(string email, string name)
        {
            this.email = email;
            this.name = name;
            tasks = new List<Task>();
            size = 0;
            limit = -1;
        }

        public int getSize()
        {
            return size;
        }

        public int getLimit()
        {
            return limit;
        }
        public List<Task> getListTask()
        {
            return tasks;
        }
        public string getName()
        {
            return name;
        }
        public string toString()
        {
            string o = "email: " + email + "\nname: " + name + "\nsize: " + size + "\nlimit: " + limit;
            foreach(Task task in tasks)
            {
                o = o + "\n\t" + task.getTitle();
            }
            return (o);
        }

        public void addTask(Task task) 
        {
            if (limit!=-1 & limit == size) { throw new ArgumentException() ; }
            tasks.Add(task);
            size++;
        }
        public Task deleteTask(Task task)
        {
            if (tasks.Remove(task))
            {
                return task;
            }
            return null;
        }
        Task getTask(string title)
        {
            foreach (Task task in tasks)
            {
                if (task.getTitle() == title)
                {
                    return task;
                }
            }
            return null;
        }
        public Task getTask(int ID)
        {
            foreach (Task task in tasks)
            {
                if (task.getID() == ID)
                {
                    return task;
                }
            }
            return null;
        }
        public void setLimit(int limit)
        {
            if (limit < size) { throw new ArgumentException(); }
            this.limit = limit;
        }

        public DAL.Column ToDalObject()
        {
            List<DAL.Task> Dtasks = new List<DAL.Task>();
            foreach (Task t in tasks) { Dtasks.Add(t.ToDalObject()); }
            return new DAL.Column(email, name, limit, Dtasks); ;
        }

        public void FromDalObject(DAL.Column DalObj)
        {
            email = DalObj.getEmail();
            name = DalObj.getName();
            limit = getLimit();
            foreach(DAL.Task t in DalObj.getTasks()) 
            {
                Task BT = new Task();
                BT.FromDalObject(t);
                tasks.Add(BT);
            }
            size = tasks.Count();
        }

        public void Save()
        {
            DAL.Column DC=ToDalObject();
            DC.Write("JSON\\" + email + "\\" + name + ".json",DC.toJson());
        }
        
        public void Load()
        {
            DAL.Column DC = new DAL.Column(email, name);
            DC.fromJson("JSON\\" + email + "\\" + name + ".json");
            FromDalObject(DC);
        }
    }
}
