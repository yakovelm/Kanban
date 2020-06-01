using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class UBlink
    {
        public bool Load { get; set; }
        public string Lastemail { get; set; }
        public int LastId { get; set; }
        public int HostId { get; set; }
        public UBlink()
        {
            Lastemail = null;
            LastId =-1;
            HostId = -1;
            Load = false;
        }
    }
}
