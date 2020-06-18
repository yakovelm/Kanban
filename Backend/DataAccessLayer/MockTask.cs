using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class MockTask : ITask
    {
        public void Add()
        {
            throw new NotImplementedException();
        }

        public List<Task> GetAllTasks(long host, string Cname)
        {
            return new List<Task>();
        }

        public string MakeFilter()
        {
            return "Mock Filter";
        }

        public void UpdateAssignee(string assig)
        {
        }

        public void UpdateColumn(string c)
        {
        }

        public void UpdateDesc(string d)
        {
        }

        public void UpdateDue(long d)
        {
        }

        public void UpdateTitle(string t)
        {
        }
    }
}
