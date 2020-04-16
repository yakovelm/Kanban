using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL = IntroSE.Kanban.Backend.BusinessLayer.TaskControl;

namespace IntroSE.Kanban.Backend.ServiceLayer.SubService
{
    public class BoardService
    {
        private BL.BoardController BC;
        public BoardService()
        {
            BC = new BL.BoardController();
        }

        public void LoadData()
        {
            BC.LoadData();
        }

        public Response LimitColumnTask(string email, int ColumnOrdinal, int limit)
        {
            try
            {
                BC.LimitColumnTask(email, ColumnOrdinal, limit);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response<Task> AddTask(string email, string title, string desciption, DateTime dueTime)
        {
            try
            {
                BL.Task task=BC.AddTask(email, title, desciption, dueTime);
                return new Response<Task>(new Task(task.getID(),task.getCreation(),title, desciption));
            }
            catch (Exception e) { return (new Response<Task>(e.Message)); }
        }
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskID, DateTime Due)
        {
            try
            {
                BC.UpdateTaskDueDate(email, columnOrdinal, taskID, Due);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskID, string title)
        {
            try
            {
                BC.UpdateTaskTitle(email, columnOrdinal, taskID, title);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskID, string Desc)
        {
            try
            {
                BC.UpdateTaskDescription(email, columnOrdinal, taskID, Desc);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response<Task> GetTask(int ID)
        {
            try { return new Response<Task>(chengeType(BC.GetTask(ID))); }
            catch (Exception e) { return (new Response<Task>(new Task(), e.Message)); }
        }
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            try
            {
                BC.AdvanceTask(email, columnOrdinal, taskId);
                return new Response();
            }
            catch (Exception e) { return (new Response(e.Message)); }
        }
        public Response<Column> GetColumn(string email, string columnName)
        {
            try
            {
                BL.Column columnBL = BC.GetColumn(email, columnName);
                return new Response<Column>(chengeType(columnBL));
            }
            catch (Exception e) { return (new Response<Column>(new Column(), e.Message)); }
        }
        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            try
            {
                BL.Column columnBL = BC.GetColumn(email, columnOrdinal);
                return new Response<Column>(chengeType(columnBL));
            }
            catch (Exception e) { return (new Response<Column>(new Column(), e.Message)); }
        }
        public Response<Board> GetBoard(string email)
        {
            Dictionary<string, BL.Column> listColumnBL = BC.getColumns(email);
            List<string> listNames = new List<string>();
            foreach (BL.Column a in listColumnBL.Values) { listNames.Add(a.getName()); }
            return new Response<Board>(new Board(listNames));
        }
        private Task chengeType(BL.Task taskBL)
        {
            return new Task(taskBL.getID(), taskBL.getCreation(), taskBL.getTitle(), taskBL.getDesc());
        }
        private Column chengeType(BL.Column columnBL)
        {
            List<Task> TaskListSL = new List<Task>();
            foreach (BL.Task taskBL in columnBL.getListTask())
            { TaskListSL.Add(chengeType(taskBL)); }
            return new Column(TaskListSL, columnBL.getName(), columnBL.getLimit());
        }
    }
}