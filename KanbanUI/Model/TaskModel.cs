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
        public DateTime Due { get; set; }
        public DateTime Cre { get; set; }
        public TaskModel(BackendController c,string A, string T, string D, DateTime DU, DateTime C) : base(c)
        {
            Assignee = A;
            Title = T;
            Desc = D;
            Due = DU;
            Cre = C;
        }
        public TaskModel(BackendController c) : base(c)
        {

        }
    }
}
