using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.Model
{
    public class UserModel: NotifiableModelObject
    {
         private string _email;
        public string email { get=> _email; set { _email = value; RaisePropertyChanged("email"); } }

        public UserModel(BackendController controller, string email) : base(controller) {
           _email = email;
        }
    }
}
