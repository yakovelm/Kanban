﻿using System;
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
        private string email;
        private string name;
        private List<Task> tasks;
        private int limit;
        private int size;

        public Column(string email, string name)
        {
            log.Info("creating new " + name + " column for " + email+".");
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

        public void addTask(Task task) // add a new task to this column
        {
            log.Debug("adding task: #" + task.getID() + " title: " + task.getTitle() + " to column: " + name + " in " + email+".");
            if (limit != -1 & limit <= size)
            {
                log.Warn("task limit reached, task not added.");
                throw new Exception("task limit reached, task not added.");
            }
            tasks.Add(task);
            Save();
            size++;
        }
        public Task deleteTask(Task task) // delete a task from this column (if exists) and return it
        {
            log.Debug("removing task: #" + task.getID() + " title: " + task.getTitle() + " from column: " + name + " in " + email+".");
            if (tasks.Remove(task))
            {
                Save();
                size--;
                return task;
            }
            log.Info("task does not exist in " + name + " column.");
            return null;
        }
        public Task getTask(int ID) // get a task from this column by ID
        {
            log.Debug("retrieving task with ID: " + ID + " from column: " + name + " in " + email + ".");
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
        public void setLimit(int limit) // set the limit of this column
        {
            log.Info("changing task limit for column: " + name + " in " + email + " from: " + this.limit + " to: " + limit + ".");
            if (limit < size & limit > 0)
            {
                log.Warn("limit cannot be lower than current amount of tasks. limit not changed.");
                throw new Exception("limit cannot be lower than current amount of tasks. limit not changed.");
            }
            this.limit = limit;
            Save();
        }

        public DAL.Column ToDalObject() // convert this column to a DataAccessLayer object
        {
            log.Debug("column " + name + "converting to DAL obj in " + email + ".");
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

        public void FromDalObject(DAL.Column DalObj) // convert a DataAccessLayer object to a BuisnessLayer column and set this to corresponding values
        {
            log.Debug("column " + name + "converting from DAL obj in " + email + ".");
            try
            {
                email = DalObj.email;
                name = DalObj.name;
                limit = DalObj.limit;
                foreach (DAL.Task t in DalObj.getTasks()) // convert each task in column individually
                {
                    Task BT = new Task();
                    BT.FromDalObject(t);
                    tasks.Add(BT);
                }
                size = tasks.Count();
            }
            catch (Exception e)
            {
                log.Error("issue converting column DAL object to column BL object due to " + e.Message);
                throw e;
            }
        }

        public void editTitle(int ID, string title) // update title of this task
        {
            Task t = getTask(ID);
            if (t == null)
            {
                throw new Exception("task does not exist in this columm.");
            }
            t.editTitle(title);
            Save();
        }
        public void editDesc(int ID, string desc)// update description of this task
        {
            Task t = getTask(ID);
            if (t == null)
            {
                throw new Exception("task does not exist in this columm.");
            }
            t.editDesc(desc);
            Save();
        }
        public void editDue(int ID, DateTime due)// update due date of this task
        {
            Task t = getTask(ID);
            if (t == null)
            {
                throw new Exception("task does not exist in this columm.");
            }
            t.editDue(due);
            Save();
        }
        public void Save() // save this column to a a json file
        {
            try
            {
                log.Debug("column " + name + " saving to hard drive for " + email + ".");
                DAL.Column DC = ToDalObject();
                DC.Write("JSON\\" + email + "\\" + name + ".json", DC.toJson());
            }
            catch (Exception e)
            {
                log.Error("failed to write to file due to " + e.Message);
                throw e;
            }
        }

        public void Load() // lod this column's data from a json file
        {
            log.Debug("column " + name + "loading from hard drive for " + email + ".");
            DAL.Column DC = new DAL.Column(email, name);
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\JSON\\" + email + "\\" + name + ".json"))
            {
                log.Info("no preexisting " + name + "column file for " + email + " initializing new empty file.");
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
