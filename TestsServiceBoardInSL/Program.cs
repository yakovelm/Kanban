using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer.TaskControl;
using IntroSE.Kanban.Backend.ServiceLayer.SubService;

namespace IntroSE.Kanban.Backend.ServiceLayer.SubService
{
    class Program
    {
        static void Main(string[] args)
        {
            BoardService a = new BoardService();
            String email = "yaki@gmail.com";
            int i = 1;
            //test 1: add task
            Console.WriteLine(a.AddTask(email, "first task", "asd", new DateTime(2020 + i, 6, 10)).ErrorMessage+" test " +i);
            Console.WriteLine(" test " + i + " need to seccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.AddTask("asd", "" + i, "asd", new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            a.AddTask(email, "" + i, "asd", new DateTime(2020 + i, 6, 10));
            i++;
            a.AddTask(email, "" + i, "asd", new DateTime(2020 + i, 6, 10));
            i++;
            a.AddTask(email, "" + i, "asd", new DateTime(2020 + i, 6, 10));
            i++;
            a.AddTask(email, "" + i, "asd", new DateTime(2020 + i, 6, 10));
            i++;
            //test limit
            a.LimitColumnTask(email, 1, 8);
            if (a.GetColumn(email,1).Value.Limit == 8)
            { Console.WriteLine("set Limit secceesed"); }
            else { Console.WriteLine("set Limit unsecceesed"); }
            i++;
            //test 1: add task
            Console.WriteLine(a.LimitColumnTask("asd", 2, 89).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.LimitColumnTask(email, 6, 56).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.LimitColumnTask(email, -2, 56).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.AddTask("asd", "" + i, "asd", new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
                Console.WriteLine(a.UpdateTaskDueDate(email,9, 5, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
                Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.UpdateTaskDueDate("asdasd", 9, 5, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
                Console.WriteLine(a.UpdateTaskDueDate(email,2, 0, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
                Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
             Console.WriteLine(a.UpdateTaskDueDate(email,0, 5, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
             Console.WriteLine(" test " + i + " need to unseccess");
            i++;
             Console.WriteLine(a.UpdateTaskDueDate("asd", 0, 5, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
             Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task

             Console.WriteLine(a.UpdateTaskDueDate(email,3, 5, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
             Console.WriteLine(" test " + i + " need to unseccess");
            i++;
             Console.WriteLine(a.UpdateTaskDueDate("asdasdasd", 3, 5, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
             Console.WriteLine(" test " + i + " need to unseccess");
             i++;
            //test 1: add task
              Console.WriteLine(a.UpdateTaskDueDate(email,2, 89, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
              Console.WriteLine(" test " + i + " need to unseccess");
            i++;
               Console.WriteLine(a.UpdateTaskDueDate("asdasdasd", 2, 89, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
               Console.WriteLine(" test " + i + " need to unseccess");
               i++;
             //test 1: add task

               Console.WriteLine(a.UpdateTaskDueDate(email,0, 5, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
              Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.UpdateTaskDueDate("asfasf", 0, 5, new DateTime(2020 + i, 6, 10)).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
             //test 1: add task
             Console.WriteLine(a.UpdateTaskDescription(email,9, 5, "sdf").ErrorMessage + " test " + i);
             Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.UpdateTaskDescription("asdas", 9, 5, "sdf").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
             //test 1: add task
            Console.WriteLine(a.UpdateTaskDescription(email,2, 0, "ds").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
        Console.WriteLine(a.UpdateTaskDescription("asass", 2, 0, "ds").ErrorMessage + " test " + i);
           Console.WriteLine(" test " + i + " need to unseccess");
          i++;
            Console.WriteLine(a.UpdateTaskDescription(email,0, 5, "sdf").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
          i++;
          Console.WriteLine(a.UpdateTaskDescription("asdas", 0, 5, "sdf").ErrorMessage + " test " + i);
          Console.WriteLine(" test " + i + " need to unseccess");
             i++;
        //test 1: add task
           Console.WriteLine(a.UpdateTaskDescription(email,3, 5, "asf").ErrorMessage + " test " + i);
          Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.UpdateTaskDescription("asd@as", 3, 5, "asf").ErrorMessage + " test " + i);
          Console.WriteLine(" test " + i + " need to unseccess");
                i++;
               //test 1: add task
           Console.WriteLine(a.UpdateTaskDescription(email,2, 89, "sdf").ErrorMessage + " test " + i);
                Console.WriteLine(" test " + i + " need to unseccess");
            i++;
          Console.WriteLine(a.UpdateTaskDescription("asdas", 2, 89, "sdf").ErrorMessage + " test " + i);
             Console.WriteLine(" test " + i + " need to unseccess");
           i++;
                        //test 1: add task
         Console.WriteLine(a.UpdateTaskDescription(email,0, 5, "asd").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
          Console.WriteLine(a.UpdateTaskDescription("asdasdas", 0, 5, "asd").ErrorMessage + " test " + i);
          Console.WriteLine(" test " + i + " need to unseccess");
          i++;
           //test 1: add task
            Console.WriteLine(a.UpdateTaskTitle(email,9, 5, "sdf").ErrorMessage + " test " + i);
          Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.UpdateTaskTitle("asd", 9, 5, "sdf").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.UpdateTaskTitle(email,2, 0, "ds").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.UpdateTaskTitle("asdasd", 2, 0, "ds").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.UpdateTaskTitle(email,0, 5, "sdf").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            //test 1: add task
            Console.WriteLine(a.UpdateTaskTitle(email,3, 5, "asf").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.UpdateTaskTitle(email,2, 89, "sdf").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.UpdateTaskTitle(email,0, 5, "asd").ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            if(a.GetTask(55).ErrorMessage==null)
            { Console.WriteLine("test " + i + " fall"); }
            else {
                Console.WriteLine("test " + i + " seccess");
            }
            i++;
            //test 1: add task
            if (a.GetTask(-5).ErrorMessage == null)
            { Console.WriteLine("test " + i + " fall"); }
            else
            {
                Console.WriteLine("test " + i + " seccess");
            }
            i++;
            if (a.GetTask(5).ErrorMessage != null)
            { Console.WriteLine("test " + i + " fall"); }
            else
            {
                Console.WriteLine(a.GetTask(5).Value);
            }
            i++;
            if (a.GetTask(3).ErrorMessage != null)
            { Console.WriteLine("test " + i + " fall"); }
            else
            {
                Console.WriteLine(a.GetTask(5).Value);
            }
            i++;
            if (a.GetTask(1).ErrorMessage != null)
            { Console.WriteLine("test " + i + " fall"); }
            else
            {
                Console.WriteLine(a.GetTask(5).Value);
            }
            i++;
            a.AdvanceTask(email,1, 1);
            a.AdvanceTask(email,1, 3);
            a.AdvanceTask(email,1, 4);
            //test 1: add task
            Console.WriteLine(a.AdvanceTask(email,3, 1).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.AdvanceTask(email, 7, 1).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.AdvanceTask("asdasd", 7, 1).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            Console.WriteLine(a.AdvanceTask(email, 2, 2).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            a.AdvanceTask(email,2, 1);
            //test 1: add task
            Console.WriteLine(a.AdvanceTask(email, 3, 1).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            //test 1: add task
            if (a.GetColumn(email,"d").ErrorMessage == null)
            { Console.WriteLine("test " + i + " fall");
                Console.WriteLine(a.GetColumn(email, 3).ErrorMessage);
            }
            else
            {
                Console.WriteLine("test " + i + " seccess");
                Console.WriteLine(a.GetColumn(email, "d").ErrorMessage);
            }
            Console.WriteLine(i);
            i++;
            if (a.GetColumn(email,"a").ErrorMessage != null)
            { Console.WriteLine("test " + i + " fall"); }
            else
            {
                Console.WriteLine("test " + i + " seccess");
                Console.WriteLine(a.GetColumn(email,"a").Value);
            }
            i++;
            //test 1: add task
            if (a.GetColumn(email,89).ErrorMessage == null)
            { Console.WriteLine("test " + i + " fall"); }
            else
            {
                Console.WriteLine("test " + i + " seccess");
                Console.WriteLine(a.GetColumn(email, 89).ErrorMessage);
            }
            i++;
            //test 1: add task
            if (a.GetColumn(email,3).ErrorMessage != null)
            { Console.WriteLine("test " + i + " fall"); }
            else
            {
                Console.WriteLine("test " + i + " seccess");
                Console.WriteLine(a.GetColumn(email, 3).Value);
            }
            i++;
            if (a.GetColumn(email, 0).ErrorMessage == null)
            { Console.WriteLine("test " + i + " fall"); }
            else
            {
                Console.WriteLine("test " + i + " seccess");
                Console.WriteLine(a.GetColumn(email, 0).ErrorMessage);
            }
            i++;
            //test 1: add task
            Console.WriteLine(a.AdvanceTask(email,1, 1).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(a.AdvanceTask(email, 3, 1).ErrorMessage + " test " + i);
            Console.WriteLine(" test " + i + " need to unseccess");
            i++;
            Console.WriteLine(i - 1);



            Console.WriteLine("asdasd");
            Console.ReadLine();

        }
    }
}
