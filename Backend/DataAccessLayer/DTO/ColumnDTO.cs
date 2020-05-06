using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DALControllers;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
     internal class ColumnDTO : DTO
    {
        public const string ColumnCIDColumnName = "CID";
        public const string ColumnNameColumnName = "Cname";
        public const string ColumnSizeColumnName = "Size";///
        public const string ColumnOrdColumnName = "Ord";
        public const string ColumnLimitColumnName = "Limit";

        private long CID;
        private string Cname;
        private long Size;
        private long Ord;
        private long Limit;

        public long cID{ get => CID; set { CID = value; controller.Update(Email, ColumnCIDColumnName, value); } }
        public string cName{ get => Cname; set { Cname = value; controller.Update(Email,ColumnNameColumnName, value); } }
        public long size{ get => Size; set { Size = value; controller.Update(Email, ColumnSizeColumnName, value); } }
        public long Ordinal{ get => Ord; set { Ord = value; controller.Update(Email, ColumnOrdColumnName, value); } }
        public long limit{ get => Limit; set { Limit = value; controller.Update(Email, ColumnLimitColumnName, value); } }
        public ColumnDTO(long CID, string Email,string Cname, long Size,long Ord,long Limit) : base(new ColumnCtrl())
        {
            this.CID = CID;
            _email = Email;
            this.Cname = Cname;
            this.Size = Size;
            this.Ord = Ord;
            this.Limit = Limit;
        }
    }
}
