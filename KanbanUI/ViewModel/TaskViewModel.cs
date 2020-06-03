using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanUI.Model;

namespace KanbanUI.ViewModel
{
    class TaskViewModel : NotifiableObject
    {
        string _assignee;
        public string Assignee { get => _assignee; set { _assignee = value; RaisePropertyChanged("Assignee"); } }
        string _title;
        public string Title { get => _title; set { _title = value; RaisePropertyChanged("Title"); } }
        string _desc;
        public string Desc { get => _desc; set { _desc = value; RaisePropertyChanged("Desc"); } }
        string _due;
        public string Due { get => _due; set { _due = value; RaisePropertyChanged("Due"); } }
        string _cre;
        public string Cre { get => _cre; set { _cre = value; RaisePropertyChanged("Cre"); } }
        TaskModel TM;
        public TaskViewModel(TaskModel tm)
        {
            TM = tm;
            Assignee = TM.Assignee;
            Title = TM.Title;
            Desc = TM.Desc;
            Due = TM.Due;
            Cre = TM.Cre;
        }
    }
}
