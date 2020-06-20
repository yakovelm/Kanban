using KanbanUI.Model;
using System;

namespace KanbanUI.ViewModel
{
    class TaskViewModel : NotifiableObject
    {

        public string WindowTitle { get => _windowTitle; set { _windowTitle = value; RaisePropertyChanged("WindowTitle"); } }
        private string _windowTitle;
        public bool isEdit { get; }
        public bool isAssignee { get; }
        string _message;
        public string Message { get => _message; set { _message = value; RaisePropertyChanged("Message"); } }
        string _assignee;
        public string Assignee { get => _assignee; set { _assignee = value; RaisePropertyChanged("Assignee"); } }
        string _title;
        public string Title
        {
            get => _title; set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
                else Message = "illegal Title.";
            }
        }
        string _desc;
        public string Desc { get => _desc; set { _desc = value; RaisePropertyChanged("Desc"); } }

        DateTime _cre;
        public string Cre { get => _cre.ToString(); set { _cre = DateTime.Parse(value); RaisePropertyChanged("Cre"); } }

        int _dueDay;
        public string DueDay
        {
            get => _dueDay.ToString(); set
            {
                try
                {
                    if (Int32.Parse(value) > 31 || Int32.Parse(value) < 1) throw new Exception("date format illegal.");
                    _dueDay = Int32.Parse(value);
                    RaisePropertyChanged("DueDay");
                }
                catch { Message = "date format illegal."; }
            }
        }

        internal Boolean apply()
        {
            if (!isEdit)
            {
                try
                {
                    TM.addTask(Title, Desc, parseDate(), UM.email);
                    return true;
                }
                catch (Exception e) { Message = e.Message; }
                return false;
            }
            else
            {
                try
                {
                    TM.editTask(Title, Desc, parseDate(), Assignee, UM.email);
                    return true;
                }
                catch (Exception e) { Message = e.Message; }
                return false;
            }
        }

        int _dueMonth;
        public string DueMonth
        {
            get => _dueMonth.ToString(); set
            {
                try
                {
                    if (Int32.Parse(value) > 12 || Int32.Parse(value) < 1) throw new Exception("date format illegal.");
                    _dueMonth = Int32.Parse(value);
                    RaisePropertyChanged("DueMonth");
                }
                catch { Message = "date format illegal."; }
            }
        }
        int _dueYear;
        public string DueYear
        {
            get => _dueYear.ToString(); set
            {
                try
                {
                    if (Int32.Parse(value) < DateTime.Now.Year) throw new Exception("date format illegal.");
                    _dueYear = Int32.Parse(value);
                    RaisePropertyChanged("DueYear");
                }
                catch { Message = "date format illegal."; }
            }
        }
        UserModel UM;
        TaskModel TM;
        public TaskViewModel(TaskModel tm, UserModel um)
        {

            UM = um;
            TM = tm;
            Assignee = TM.Assignee;
            Title = TM.Title;
            Desc = TM.Desc;
            DueDay = DateTime.Parse(TM.Due).Day.ToString();
            DueMonth = DateTime.Parse(TM.Due).Month.ToString();
            DueYear = DateTime.Parse(TM.Due).Year.ToString();
            Cre = TM.Cre.ToString();
            WindowTitle = "Edit Task";
            isAssignee = (UM.email == Assignee);
            isEdit = isAssignee;
            Console.WriteLine(Assignee + "   " + UM.email);
        }
        public TaskViewModel(UserModel um)
        {
            isEdit = false;
            isAssignee = true;
            UM = um;
            TM = new TaskModel(um.Controller);
            Assignee = UM.email;
            Cre = DateTime.Now.ToString();
            DueDay = DateTime.Today.AddDays(1).Day.ToString();
            DueMonth = DateTime.Today.Month.ToString();
            DueYear = DateTime.Today.Year.ToString();
            Message = "new Task will be assigned to you.";
            WindowTitle = "Add Task";

        }

        private DateTime parseDate()
        {
            try
            {
                var ret = new DateTime(_dueYear, _dueMonth, _dueDay);
                return ret;
            }
            catch { }
            return DateTime.MinValue;
        }
    }
}
