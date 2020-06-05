using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    public class ColumnModel: NotifiableModelObject
    {
        public string Name;
        public int limit;
        public List<TaskModel> tasks;
        public ColumnModel(BackendController c,string email,string Name): base(c)
        {
            Tuple<string,int, List<TaskModel>> col = Controller.getColumn(email,Name);
            this.Name = col.Item1;
            limit = col.Item2;
            tasks = col.Item3;
        }
    }
}
