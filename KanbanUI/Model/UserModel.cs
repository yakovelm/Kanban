using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    public class UserModel: NotifiableModelObject
    {
        public string email { get; set; }

        public UserModel(BackendController controller, string email) : base(controller) {
           this.email = email;
        }
    }
}
