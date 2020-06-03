using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    class TaskModel : NotifiableModelObject
    {
        public string Assignee { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Due { get; set; }
        public string Cre { get; set; }
        public TaskModel(BackendController c) : base(c)
        {

        }
    }
}
