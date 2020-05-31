using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DAL = IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer.TaskControl
{
    class Column : IPersistentObject<DAL.Column>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int host;
        private int ord;
        private const int maxname= 15;
        private const int defLimit = 100;
        private string name;
        private List<Task> tasks;
        private int limit;
        private int size;
        public Column(int host, string name,int ord)
        {
            log.Info("creating new empty " + name + " column for " + host + ".");
            this.host = host;
            if (name == null || name=="" || name.Length>maxname) throw new Exception("illegal name.");
            this.name = name;
            if (ord < 0) throw new Exception("ordinal illegal.");
            this.ord = ord;
            tasks = new List<Task>();
            size = 0;
            limit = defLimit;
            DAL.Column Dcol = ToDalObject();
            Dcol.Add();
        }
        public Column()
        {
            tasks = new List<Task>();
        }
        public int getSize()
        {
            return size;
        }
        public int getOrd()
        {
            return ord;
        }
        public int getLimit()
        {
            return limit;
        }
        public string getName()
        {
            return name;
        }
        public List<Task> getAll()
        {
            log.Debug("returning all tasks");
            return tasks;
        }
        public void setOrd(int ord)
        {
            if (ord < 0) throw new Exception("ordinal illegal.");
            this.ord = ord;
            DAL.Column Dcol = ToDalObject();
            Dcol.UpdateOrd(ord);
        }
        public void setLimit(int limit) // set the limit of this column
        {
            log.Info("changing task limit for column: " + name + " in " + host + " from: " + this.limit + " to: " + limit + ".");
            if (limit < size & limit > 0)
            {
                log.Warn("limit cannot be lower than current amount of tasks. limit not changed.");
                throw new Exception("limit cannot be lower than current amount of tasks. limit not changed.");
            }
            this.limit = limit;
            DAL.Column Dcol = ToDalObject();
            Dcol.UpdateLimit(limit);
        }
        public void addTasks(List<Task> ts) // append a list of tasks to the end of this column
        {
            if (limit > 0 & size + ts.Count() > limit)
            {
                log.Warn("task limit reached, tasks not added.");
                throw new Exception("task limit reached, tasks not added.");
            }
            foreach (Task t in ts) t.editColumn(name);
            tasks.AddRange(ts);
            size = size + ts.Count;
        }
        public void addTask(Task task) // add a new task to this column
        {
            log.Debug("adding task: #" + task.getID() + " title: " + task.getTitle() + " to column: " + name + " in " + host + ".");
            if (limit > 0 & limit <= size)
            {
                log.Warn("task limit reached, task not added.");
                throw new Exception("task limit reached, task not added.");
            }
            if(ord==0) task.insert();
            task.editColumn(name);
            tasks.Add(task);
            size++;
        }
        public Task deleteTask(Task task) // delete a task from this column (if exists) and return it
        {
            log.Debug("removing task: #" + task.getID() + " title: " + task.getTitle() + " from column: " + name + " in " + host + ".");
            if (tasks.Remove(task))
            {
                size--;
                return task;
            }
            log.Info("task does not exist in " + name + " column.");
            return null;
        }
        public Task getTask(int ID) // get a task from this column by ID
        {
            log.Debug("retrieving task with ID: " + ID + " from column: " + name + " in " + host + ".");
            foreach (Task task in tasks)
            {
                if (task.getID() == ID)
                {
                    return task;
                }
            }
            log.Info("task does not exist in " + name + " column.");
            return null;
        }
        public DAL.Column ToDalObject() // convert this column to a DataAccessLayer object
        {
            log.Debug("column " + name + " converting to DAL obj in " + host + ".");
            try
            {
                List<DAL.Task> Dtasks = new List<DAL.Task>();
                log.Debug("list made");
                foreach (Task t in tasks) {
                    //log.Debug("converting "+t.getTitle()+" to dal object.");
                    Dtasks.Add(t.ToDalObject()); 
                }
                log.Debug("post foreach");
                DAL.Column c= new DAL.Column(host, name, ord, limit);
                log.Debug("made dal column.");
                return c;
            }
            catch (Exception e)
            {
                log.Error("issue converting column BL object to column DAL object due to " + e.Message);
                throw e;
            }
        }
        public void FromDalObject(DAL.Column DalObj) // convert a DataAccessLayer object to a BuisnessLayer column and set this to corresponding values
        {
            log.Debug("column " + DalObj.Cname + " converting from DAL obj in " + DalObj.Email + ".");
            try
            {
                host = (int)DalObj.Host;
                name = DalObj.Cname;
                ord = (int)DalObj.Ord;
                limit = (int)DalObj.Limit;
                log.Debug(host + " "+name+" "+ord+" "+limit);
                DalObj.LoadTasks();
                log.Debug(DalObj.getTasks().Count());
                foreach (DAL.Task t in DalObj.getTasks()) // convert each task in column individually
                {
                    log.Debug("in foreach with: " + t.ID);
                    Task BT = new Task(); 
                    BT.FromDalObject(t);
                    log.Debug("made task: " + BT.getID()+ " with title: "+BT.getTitle());
                    tasks.Add(BT);
                    log.Debug(tasks.Count());
                }
                size = tasks.Count();
                log.Debug("column: " + getName() + " size: " + getSize());
            }
            catch (Exception e)
            {
                log.Error("issue converting column DAL object to column BL object due to " + e.Message);
                throw e;
            }
        }
        public void editTitle(int ID, string title, string assig) // update title of this task
        {
            Task t = getTask(ID);
            if (t == null)
            {
                throw new Exception("task does not exist in this columm.");
            }
            t.editTitle(assig,title);
        }
        public void editDesc(int ID, string desc, string assig)// update description of this task
        {
            Task t = getTask(ID);
            if (t == null)
            {
                throw new Exception("task does not exist in this columm.");
            }
            t.editDesc(assig,desc);
        }
        public void editDue(int ID, DateTime due, string assig)// update due date of this task
        {
            Task t = getTask(ID);
            if (t == null)
            {
                throw new Exception("task does not exist in this columm.");
            }
            t.editDue(assig, due);
        }
        public void delete()
        {
            DAL.Column c = new DAL.Column(host, name);
            c.Delete();
        }
        public void ChangeColumnName(string cur,int columnOrdinal, string newName)
        {
            throw new Exception("/////////////////////////////////////////////////////////////////////");
        }
    }
}