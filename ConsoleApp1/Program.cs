using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UU=IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BusinessLayer.UserControl;
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

                print(s.LoadData());
                print(s.Register(te2, tp2, tn2));
                ////print(s.Login(te2, tp2));
                ////print(s.GetColumn(te2, 0).toString());
                ////Console.WriteLine("before LoadData");
                ////print(s.LimitColumnTasks(te2, 1, 5));
                ////print(s.Register(te2, tp2, "nit"));
                ////print(s.Login(te2, tp2).toString());
                ////print(s.AddTask(te2, "title1", "desc", new DateTime(2200,06,22)));
                ////print(s.AddColumn(te, 4, "good"));
                ////print(s.MoveColumnLeft(te2, 1));
                ////print(s.MoveColumnRight(te2, 2));
                ////print(s.RemoveColumn(te2, 1));
                ////print(s.LimitColumnTasks(te2, 1, 5));
                ////Console.WriteLine("*************************************************************************");


                //print(s.LoadData());
                //print(s.Register(te2, tp2, "nit"));
                //print(s.Login(te2, tp2).toString());
                //print(s.Login(te2, tp2).toString());
                //print(s.AddTask(te2, "title1", "desc", new DateTime(2200, 06, 22)));
                //print(s.AddTask(te2, "title2", "desc", new DateTime(2200, 06, 22)));
                //print(s.AddTask(te2, "title3", "desc", new DateTime(2200, 06, 22)));
                //print(s.AddTask(te2, "title4", "desc", new DateTime(2200, 06, 22)));
                //print(s.AddTask(te2, "title5", "desc", new DateTime(2200, 06, 22)));
                //print(s.AddTask(te2, "title6", "desc", new DateTime(2200, 06, 22)));
                //print(s.AddTask(te2, "title7", "desc", new DateTime(2200, 06, 22)));
                //print(s.AddColumn(te, 4, "good"));
                //print(s.AddColumn(te2, 4, "good"));
                //for (int i = 1; i < 8; i++)
                //{
                //    print(s.AdvanceTask(te2, 1, i));
                //    if (i > 4)
                //    {
                //        print(s.AdvanceTask(te2, 2, i));
                //    }
                //}
                //print(s.AdvanceTask(te2, 2, 1));
                //print(s.AdvanceTask(te2, 3, 1));
                //print(s.MoveColumnLeft(te, 1));
                //print(s.MoveColumnLeft(te2, 1));
                //print(s.MoveColumnRight(te, 2));
                //print(s.MoveColumnRight(te2, 2));
                //print(s.RemoveColumn(te, 1));
                //print(s.RemoveColumn(te2, 1));
                //print(s.LimitColumnTasks(te, 1, 5));
                //print(s.LimitColumnTasks(te2, 20, 5));
                //print(s.LimitColumnTasks(te2, 1, 0));
                //print(s.LimitColumnTasks(te2, 1, 5));

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
