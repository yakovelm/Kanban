using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Board
    {
        public readonly IReadOnlyCollection<string> ColumnsNames;
        internal Board(IReadOnlyCollection<string> columnsNames) 
        {
            this.ColumnsNames = columnsNames;
        }
        // You can add code here
        public override string ToString()
        {
            string ret= "";
            foreach (string c in ColumnsNames) { ret=ret+c+"\n"; }
            return ret;
        }
    }
}
