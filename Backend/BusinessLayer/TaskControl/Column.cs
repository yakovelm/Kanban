using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer.TaskControl
{
    class Column :IPersistentObject<DAL.Column>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string email;
        private string name;
        private List<Task> tasks;
        private int limit;
        private int size;

        public Column(string email, string name)
        {
            log.Info("creating new "+name+" column for "+email);
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

        public void addTask(Task task) 
        {
            log.Info("adding task: #"+task.getID()+" title: "+task.getTitle()+" to column: "+name+" in "+email);
            if (limit!=-1 & limit == size) {
                log.Warn("task limit reached, task not added.");
                throw new Exception("task limit reached, task not added.") ;
            }
            tasks.Add(task);
            Save();
            size++;
        }
        public Task deleteTask(Task task)
        {
            log.Info("removing task: #" + task.getID() + " title: " + task.getTitle() + " from column: " + name + " in " + email);
            if (tasks.Remove(task))
            {
                Save();
                return task;
            }
            log.Info("task does not exist in "+name+" column");
            return null;
        }
        Task getTask(string title)
        {
            log.Debug("retrieving task with title: " + title + " from column: " + name + " in " + email);
            foreach (Task task in tasks)
            {
                if (task.getTitle() == title)
                {
                    return task;
                }
            }
            log.Info("task does not exist in " + name + " column");
            return null;
        }
        public Task getTask(int ID)
        {
            log.Debug("retrieving task with ID: " + ID + " from column: " + name + " in " + email);
            foreach (Task task in tasks)
            {
                if (task.getID() == ID)
                {
                    return task;
                }
            }
            log.Info("task does not exist in " + name + " column");
            return null;
        }
        public void setLimit(int limit)
        {
            log.Info("changing task limit for column: " + name + " in " + email+" from: "+this.limit+" to: "+limit);
            if (limit < size) {
                log.Warn("limit cannot be lower than current amount of tasks. limit not changed");
                throw new Exception("limit cannot be lower than current amount of tasks. limit not changed"); 
            }
            this.limit = limit;
        }

        public DAL.Column ToDalObject()
        {
            log.Debug("column "+name + "converting to DAL obj in " + email);
            try
            {
                List<DAL.Task> Dtasks = new List<DAL.Task>();
                foreach (Task t in tasks) { Dtasks.Add(t.ToDalObject()); }
                return new DAL.Column(email, name, limit, Dtasks); ;
            }
            catch (Exception e)
            {
                log.Error("issue converting column BL object to column DAL object due to " + e.Message);
                throw e;
            }
        }

        public void FromDalObject(DAL.Column DalObj)
        {
            log.Debug("column " + name + "converting from DAL obj in " + email);
            try
            {
                email = DalObj.email;
                name = DalObj.name;
                limit = DalObj.limit;
                foreach (DAL.Task t in DalObj.getTasks())
                {
                    Task BT = new Task();
                    BT.FromDalObject(t);
                    tasks.Add(BT);
                }
                size = tasks.Count();
            }
            catch(Exception e) 
            { 
                log.Error("issue converting column DAL object to column BL object due to " + e.Message);
                throw e;
            }
        }

        public void Save()
        {
            log.Debug("column " + name + " saving to hard drive for " + email);
            DAL.Column DC=ToDalObject();
            try
            {
                DC.Write("JSON\\" + email + "\\" + name + ".json", DC.toJson());
            }
            catch(Exception e) { 
                log.Error("failed to write to file due to " + e.Message);
                throw e;
            }
        }
        
        public void Load()
        {
            log.Debug("column " + name + "loading from hard drive for " + email);
            DAL.Column DC = new DAL.Column(email, name);
            if(!File.Exists(Directory.GetCurrentDirectory()+ "\\JSON\\" + email + "\\" + name + ".json")) {
                log.Info("no preexisting "+name+"column file for "+email+" initializing new empty file");
                Save();
            }
            try
            {
                DC.fromJson("JSON\\" + email + "\\" + name + ".json");
            }
            catch (Exception e)
            {
                log.Error("failed to load column from file due to " + e.Message);
                throw e;
            }
            FromDalObject(DC);
        }
    }
}
