using System;

namespace KanbanUI.Model
{
    public class UserModel : NotifiableModelObject
    {
        public string email { get; set; }

        public UserModel(BackendController controller, string email) : base(controller)
        {
            this.email = email;
        }

        internal string logout()
        {
            try
            {
                Controller.Logout(email);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
