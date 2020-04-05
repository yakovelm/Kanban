using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntroSE.Kanban.Backend.BusinessLayer.UserControl;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserControl
{
    class test
    {
        static void Main(string[] args)
        {
            UserController nv = new UserController();
            try
            {
                nv.register("nitayv@gmail.com", "", "nit");
                nv.register("ofervitkin@gmail.com", "031165", "nit");
                Console.WriteLine("tast1 not ok");
            }
            catch (Exception)
            {
                Console.WriteLine("tast1 ok");
            }
            try
            {
                nv.login("nitayv@gmail.com", "22069");
            }
            catch (Exception)
            {
                Console.WriteLine("tast2 ok");
            }
            try
            {
                nv.register("nitayv@gmail.com", "22069","vit");
                Console.WriteLine("tast3 ok");
            }
            catch (Exception)
            {
                Console.WriteLine("tast3 ok");
            }
        }
    }
}
