using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DALControllers;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
    abstract class DTO
    {
        public const string EmailColumnName = "email";
        protected DALCtrl controller;

        public string Email { get; set; } = "";
        protected DTO(DALCtrl controller)
        {
            this.controller = controller;
        }
    }
}
