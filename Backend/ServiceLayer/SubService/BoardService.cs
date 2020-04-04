using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.SubService
{
    public class BoardService
    {
        private BusinessLayer.TaskControl.Board board;
        public BoardService(string email)
        {
            board = new BusinessLayer.TaskControl.Board(email);
        }
        public BoardService()
        {
            //tester constractor
            String email = "yaki@gmail.com";
            BusinessLayer.TaskControl.Column a = new BusinessLayer.TaskControl.Column(email, "a");
            BusinessLayer.TaskControl.Column b = new BusinessLayer.TaskControl.Column(email, "b");
            BusinessLayer.TaskControl.Column c = new BusinessLayer.TaskControl.Column (email, "c");
            board = new BusinessLayer.TaskControl.Board(email, a, b, c);
        }
        public Response LimitColumnTask(string email, int ColumnOrdinal, int limit)
        {
            try
            {
                board.LimitColumnTask(email, ColumnOrdinal, limit);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response AddTask(string email, string title, string desciption, DateTime dueTime)
        {
            try
            {
                board.AddTask(email, title, desciption, dueTime);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskID, DateTime Due)
        {
            try
            {
                board.UpdateTaskDueDate(email, columnOrdinal, taskID, Due);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskID, string title)
        {
            try
            {
                board.UpdateTaskTitle(email, columnOrdinal, taskID, title);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskID, string Desc)
        {
            try
            {
                board.UpdateTaskDescription(email, columnOrdinal, taskID, Desc);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response<Task> GetTask(int ID)
        {
            try { return new Response<Task>(chengeType(board.GetTask(ID))); }
            catch (Exception e) { return (new Response<Task>(new Task(), e.Message)); }
        }
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            try
            {
                board.AdvanceTask(email, columnOrdinal, taskId);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response<Column> GetColumn(string email, string columnName)
        {
            try
            {
                BusinessLayer.TaskControl.Column columnBL = board.GetColumn(email, columnName);
                return new Response<Column>(chengeType(columnBL));
            }
            catch (Exception e) { return (new Response<Column>(new Column(), e.Message)); }
        }
        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            try
            {
                BusinessLayer.TaskControl.Column columnBL = board.GetColumn(email, columnOrdinal);
                return new Response<Column>(chengeType(columnBL));
            }
            catch (Exception e) { return (new Response<Column>(new Column(), e.Message)); }
        }
        public Response<Board> GetBoard(string email)
        {
            Dictionary<string, BusinessLayer.TaskControl.Column> listColumnBL = board.getColumns(email);
            List<string> listNames = new List<string>();
            foreach (BusinessLayer.TaskControl.Column a in listColumnBL.Values) { listNames.Add(a.getName()); }
            return new Response<Board>(new Board(listNames));
        }
        private Task chengeType(BusinessLayer.TaskControl.Task taskBL)
        {
            return new Task(taskBL.getID(), taskBL.getCreation(), taskBL.getTitle(), taskBL.getDesc());
        }
        private Column chengeType(BusinessLayer.TaskControl.Column columnBL)
        {
            List<Task> TaskListSL = new List<Task>();
            foreach (BusinessLayer.TaskControl.Task taskBL in columnBL.getListTask())
            { TaskListSL.Add(chengeType(taskBL)); }
            return new Column(TaskListSL, columnBL.getName(), columnBL.getLimit());
        }
    }
}