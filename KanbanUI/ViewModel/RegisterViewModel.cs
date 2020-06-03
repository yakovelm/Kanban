using KanbanUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanUI.ViewModel
{
    class RegisterViewModel: NotifiableObject
    {
        BackendController controller;
        public RegisterViewModel(BackendController controller)
        {
            this.controller = controller;
        }
    }
}
