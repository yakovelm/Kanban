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

                string te3 = "aa@a.aa";
                string tp3 = "Aa123";
                string tn3 = "aleks";

                //proper login tests - all should work
                print(s.LoadData());
                print(s.Register(te3, tp3, tn3));
                print(s.Register(te2,tp2,tn2,te3));
                print(s.Register(te,tp,tn));
                print(s.Login(te3, tp3));
                print(s.GetBoard(te3).toString());
                for (int i = 4; i <= 20; i++) print(s.AddTask(te3, "title" + i, "desc" + i, new DateTime(2200, 06, 22)));
                for (int i = 1; i <= 15; i++) print(s.AdvanceTask(te3, 0, i));
                for (int i = 1; i <= 10; i++) print(s.AdvanceTask(te3, 1, i));
                for (int i = 1; i <= 5; i++) print(s.AdvanceTask(te3, 2, i));
                print(s.AddColumn(te3, 0, "ADD1").toString());
                print(s.GetBoard(te3).toString());
                print(s.AddColumn(te3, 1, "ADD2").toString());
                print(s.GetBoard(te3).toString());
                print(s.Logout(te3));
                print(s.DeleteData());
                print(s.Register(te3, tp3, tn3));
                print(s.Login(te3, tp3));
                print(s.AddColumn(te3, 1, "ADD2").toString());
                print(s.GetBoard(te3).toString());
                print(s.AddColumn(te3, 1, "ADD$").toString());
                print(s.GetBoard(te3).toString());
                print(s.GetColumn(te3, 0).toString());
                print(s.GetColumn(te3, 1).toString());
                print(s.GetColumn(te3, 2).toString());
                print(s.GetColumn(te3, 3).toString());
                print(s.GetColumn(te3, 4).toString());
                print(s.AddTask(te3, "asd", "asd", new DateTime(2100, 06, 22)));
                print(s.Logout(te3));
                print(s.Register("asd@asd.asd", "asd!12A", "asd", te3));
                print(s.Login("asd@asd.asd", "asd!12A"));
                print(s.DeleteTask("asd@asd.asd",0,1));


                //print(s.Logout(te));

                //login edge cases - all should fail
                //print(s.LoadData());
                //print(s.Register(te, tp, tn));
                //print(s.Login(te2, tp2));
                //print(s.Login(te2, tp));
                //print(s.Logout(te));
                //print(s.Login(te, tp));
                //print(s.Logout(te2));
                //print(s.Logout(te));
                //print(s.LoadData());

                //proper creation tests - all should work
                //print(s.Register(te3, tp3, tn3));
                //print(s.Login(te3, tp3));
                //print(s.GetBoard(te3).toString());
                //for (int i = 1; i < 8; i++) print(s.AddTask(te3, "title" + i, "desc" + i, new DateTime(2200, 06, 22)));
                //print(s.AdvanceTask(te3, 0, 2));
                //print(s.AdvanceTask(te3, 0, 3));
                //print(s.AdvanceTask(te3, 0, 6));
                //print(s.UpdateTaskDescription(te3, 1, 2, "this is desc 2"));
                //print(s.UpdateTaskDescription(te3, 0, 4, "this is desc 4"));
                //print(s.UpdateTaskDescription(te3, 1, 2, "this is desc 2-2"));

                //print(s.UpdateTaskTitle(te3, 1, 2, "title 2"));
                //print(s.UpdateTaskTitle(te3, 0, 5, "title 5"));

                //print(s.UpdateTaskDueDate(te3, 1, 2, new DateTime(2100,06,22)));

                //print(s.GetColumn(te3, 0).toString());
                //print(s.GetColumn(te3, 1).toString());
                //print(s.GetColumn(te3, 2).toString());      
                //print(s.AddColumn(te3, 3, "col3").toString());
                //print(s.GetColumn(te3, 3).toString());
                //print(s.AddTask(te3, "title1", null, new DateTime(2200, 10, 10)));
                //print(s.GetColumn(te3, 0).toString());
                //print(s.Logout(te3));

                //creation edge cases - all should fail
                //print(s.AddColumn(te3, 4, "col4").toString());
                //print(s.AddTask(te3, "title2", "desc2", new DateTime(2200, 10, 10)));
                print(s.Login(te3, tp3));
                //print(s.AddColumn(te2, 4, "col3").toString());
                //print(s.AddTask(te2, "title2", "desc2", new DateTime(2200, 10, 10)));
                //print(s.AddColumn(te3, 4, "col3").toString());
                //print(s.AddColumn(te3, -6, "col7").toString());
                //print(s.AddColumn(te3, 9, "col9").toString());
                //print(s.AddColumn(te3, 4, "").toString());
                //print(s.AddColumn(te3, 4, null).toString());
                print(s.LimitColumnTasks(te3, 0, 3));
                for (int i = 2; i < 8; i++) print(s.AddTask(te3, "title"+i, "desc"+i, new DateTime(2200, 06, 22)));
                print(s.GetBoard(te3).toString());
                print(s.GetColumn(te3, 1).toString());
                print(s.Logout(te3));

                //proper change tests - all should work
                print(s.Login(te3, tp3));
                print(s.GetBoard(te3).toString());
                print(s.GetColumn(te3, 0).toString());
                print(s.GetColumn(te3, 1).toString());
                print(s.GetColumn(te3, 2).toString());
                print(s.GetColumn(te3, 3).toString());
                print(s.MoveColumnLeft(te3, 3));
                print(s.MoveColumnLeft(te3, 2));
                print(s.GetBoard(te3).toString());
                print(s.LimitColumnTasks(te3, 0, 20));
                for (int i = 4; i <= 20; i++) print(s.AddTask(te3, "title" + i, "desc" + i, new DateTime(2200, 06, 22)));
                for (int i = 1; i <= 15; i++) print(s.AdvanceTask(te3, 0, i));
                for (int i = 1; i <= 10; i++) print(s.AdvanceTask(te3, 1, i));
                for (int i = 1; i <= 5; i++) print(s.AdvanceTask(te3, 2, i));
                print(s.GetColumn(te3, 0).toString());
                print(s.GetColumn(te3, 1).toString());
                print(s.GetColumn(te3, 2).toString());
                print(s.GetColumn(te3, 3).toString());
                print(s.MoveColumnLeft(te3, 3));
                print(s.MoveColumnLeft(te3, 3));
                print(s.MoveColumnRight(te3, 0));
                print(s.GetBoard(te3).toString());
                print(s.GetColumn(te3, 0).toString());
                print(s.GetColumn(te3, 1).toString());
                print(s.GetColumn(te3, 2).toString());
                print(s.GetColumn(te3, 3).toString());
                print(s.Logout(te3));

                //change edge cases - all should fail
                print(s.LimitColumnTasks(te3, 1, 2));
                print(s.MoveColumnLeft(te3, 3));
                print(s.MoveColumnRight(te3, 1));
                print(s.AdvanceTask(te3, 1, 18));
                print(s.Login(te3, tp3));
                print(s.LimitColumnTasks(te2, 1, 2));
                print(s.MoveColumnLeft(te, 3));
                print(s.MoveColumnRight(te2, 1));
                print(s.AdvanceTask(te, 1, 18));

                print(s.MoveColumnLeft(te3, 7));
                print(s.MoveColumnRight(te3, 42));
                print(s.AdvanceTask(te3, 10, 18));
                print(s.AdvanceTask(te3, 1, 32));
                print(s.MoveColumnLeft(te3, 0));
                print(s.MoveColumnRight(te3, 3));
                print(s.LimitColumnTasks(te3, 0, 1));
                print(s.LimitColumnTasks(te3, 1, 5));
                print(s.AdvanceTask(te3, 0, 18));
                print(s.Logout(te3));

                //proper remove tests - all should work
                print(s.Login(te3, tp3));
                print(s.GetBoard(te3).toString());
                print(s.GetColumn(te3, 0).toString());
                print(s.GetColumn(te3, 1).toString());
                print(s.GetColumn(te3, 2).toString());
                print(s.GetColumn(te3, 3).toString());
                print(s.LimitColumnTasks(te3, 0, 100));
                print(s.LimitColumnTasks(te3, 1, 100));
                print(s.LimitColumnTasks(te3, 2, 100));
                print(s.LimitColumnTasks(te3, 3, 100));
                print(s.RemoveColumn(te3, 0));
                print(s.RemoveColumn(te3, 1));
                print(s.GetBoard(te3).toString());
                print(s.GetColumn(te3, 0).toString());
                print(s.GetColumn(te3, 1).toString());
                print(s.Logout(te3));

                //remove edge cases - all should fail
                print(s.RemoveColumn(te3, 1));
                print(s.Login(te3, tp3));
                print(s.RemoveColumn(te2, 1));
                print(s.RemoveColumn(te3, 1));
                print(s.AddColumn(te3, 2, "2").toString());
                print(s.AddColumn(te3, 3, "3").toString());
                for (int i = 1; i <= 20; i++) print(s.AdvanceTask(te3, 1, i));
                print(s.LimitColumnTasks(te3, 1, 1));
                print(s.LimitColumnTasks(te3, 3, 1));
                print(s.RemoveColumn(te3, 2));
                print(s.Logout(te3));
                Console.WriteLine("___________________________________________________________");

                //// delete all
                //print(s.DeleteData());
                //print(s.Login(te3, tp3));
                //print(s.GetBoard(te3).toString());
                //print(s.GetColumn(te3, 0).toString());
                //print(s.GetColumn(te3, 1).toString());
                //print(s.GetColumn(te3, 2).toString());
                //print(s.GetColumn(te3, 3).toString());
                //print(s.DeleteData());
                //print(s.GetBoard(te3).toString());
                //print(s.GetColumn(te3, 0).toString());
                //print(s.GetColumn(te3, 1).toString());
                //print(s.GetColumn(te3, 2).toString());
                //print(s.GetColumn(te3, 3).toString());
                //print(s.Logout(te3));
                //print(s.Login(te3, tp3));
                //print(s.Login(te2, tp2));
                //print(s.Login(te, tp));

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
