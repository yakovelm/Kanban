using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UU=IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer.UserControl;
using CCC = IntroSE.Kanban.Backend.DataAccessLayer.User;
using SSL = IntroSE.Kanban.Backend.ServiceLayer.SubService;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class test
    {
        static int count = 0;
        static void Main(string[] args) 
            //when using this tester make sure to run everything under the print function to keep counter in sync
        {

            try
            {
                UU.Service s = new UU.Service();
                string te = "yaki@bgu.il";
                string tp = "123zsaZSA1212";
                string tn = "yaki";

                string te2 = "nitay@bgu.il";
                string tp2 = "123zsaZSA1212";
                string tn2 = "nitay";

                print(s.Register(te2, tp2, null));
                print(s.Register(te2, null, null));
                print(s.Register(null, tp2, tn2));
                print(s.Register(te2, tp2, ""));
                print(s.Register(te2, "", tn2));
                print(s.Register("", tp2, tn2));
                print(s.Register(te2, tp2, tn2));
                print(s.Login(te2, tp2).toString());
                print(s.GetColumn(te2,0).toString());
                print(s.AddTask(te2, "a", null, DateTime.Today));
                print(s.GetColumn(te2, 0).toString());



            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            Console.Read();
        }
        static void print(Response res) //i miss python T_T
        {
            count++;
            if (res.ErrorOccured) { Console.WriteLine(count+":\n"+res.ErrorMessage); }
            else { Console.WriteLine(count + ":\n" + "no error."); }
            Console.Read();
            Console.Read();
        }
        static void print(Response<UU.User> res) //i miss python T_T
        {
            count++;
            if (res.ErrorOccured) { Console.WriteLine(count + ":\n" + res.ErrorMessage); }
            else { Console.WriteLine(count + ":\n" + "no error."); }
            Console.Read();
            Console.Read();
        }
        static void print(string prt) 
        {
            count++;
            Console.WriteLine(count + ":\n" + prt);
            Console.Read();
            Console.Read();
        }
    }
    
}
