using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    public class TaskModel : NotifiableModelObject
    {
        public string Assignee { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Due { get=>_due.ToString(); set { _due = DateTime.Parse(value); } }
        private DateTime _due;
        public DateTime Cre { get; set; }
        public TaskModel(BackendController c,string A, string T, string D, DateTime DU, DateTime C) : base(c)
        {
            Assignee = A;
            Title = T;
            Desc = D;
            _due = DU;
            Cre = C;
        }
        public TaskModel(BackendController c) : base(c)
        {

        }

        internal void apply(string title, string desc, DateTime due, string assignee,string email)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                Controller.addTask(email,title, desc, due);
            }
            else 
            {
                
            }

        }
    }
}
