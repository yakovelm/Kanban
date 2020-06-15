using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KanbanUI.Model
{
    public class TaskModel : NotifiableModelObject
    {
        public bool IsAssignee { get; set; }
        public SolidColorBrush BackgroundBrush
        {
            get => _background;
            set
            {
                _background = value;
                RaisePropertyChanged("BackgroundBrush");
            }
        }
        private SolidColorBrush _background;
        public SolidColorBrush BorderBrush
        {
            get => _borderBrush;
            set
            {
                _borderBrush = value;
                RaisePropertyChanged("BorderBrush");
            }
        }
        private SolidColorBrush _borderBrush;
        public int ID { get; set; }
        public int ColumnIndex { get; set; }
        public string Assignee { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Due { get=>_due.ToString(); set { _due = DateTime.Parse(value); } }
        private DateTime _due;
        public DateTime Cre { get; set; }
        public TaskModel(BackendController c, int ID, string A, string T, string D, DateTime DU, DateTime C, int columnIndex, string email) : base(c)
        {
            this.ID = ID;
            ColumnIndex = columnIndex;
            Assignee = A;
            Title = T;
            Desc = D;
            _due = DU;
            Cre = C;
            IsAssignee = (email == Assignee);
            if (DateTime.Now > _due)
            {
                BackgroundBrush = Brushes.Red;  
            }
            else if(((double)DateTime.Now.Subtract(Cre).Ticks / _due.Subtract(Cre).Ticks) > 0.75)
            {
                BackgroundBrush = Brushes.Orange;
            }
            if (IsAssignee)
            {
                BorderBrush = Brushes.Blue;
            }
        }
        public TaskModel(BackendController c) : base(c)
        {

        }

        internal void addTask(string title, string desc, DateTime due,string email)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                Controller.addTask(email,title, desc, due);
            }
        }

        internal void editTask(string title, string desc, DateTime due, string assignee, string email)
        {
            if (!title.Equals(Title)) Controller.editTitle(email, ColumnIndex, ID, title);
            if (desc!=Desc) Controller.editDesc(email, ColumnIndex, ID, desc);
            if (!due.Equals(_due)) Controller.editDue(email, ColumnIndex, ID, due);
            if (!assignee.Equals(Assignee)) Controller.editAssignee(email, ColumnIndex, ID, assignee);
        }
    }
}
