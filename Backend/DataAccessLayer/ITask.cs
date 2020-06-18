using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    interface ITask
    {
        List<Task> GetAllTasks(long host, string Cname);
        void UpdateTitle(string t);
        void UpdateDesc(string d);
        void UpdateColumn(string c);
        void UpdateAssignee(string assig);
        void UpdateDue(long d);
        void Add();
    }
}
