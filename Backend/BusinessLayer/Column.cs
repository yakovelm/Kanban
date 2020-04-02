using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Column
    {
        private string email;
        private string name;
        private List<Task> tasks;
        private int limit;
        private int size;

        internal Column(string email, string name)
        {
            this.email = email;
            this.name = name;
            tasks = new List<Task>();
            size = 0;
            limit = -1;
        }

        void addTask(Task task) 
        {
            if (limit!=-1 & limit == size) { throw new ArgumentException() ; }
            tasks.Add(task);
            size++;
        }
        Task deleteTask(Task task)
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
        Task getTask(int ID)
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
        void setLimit(int limit)
        {
            if (limit < size) { throw new ArgumentException(); }
            this.limit = limit;
        }

    }
}
