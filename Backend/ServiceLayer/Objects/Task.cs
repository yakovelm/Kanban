using System;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Task
    {
        public readonly int Id;
        public readonly DateTime CreationTime;
        public readonly string Title;
        public readonly string Description;
        internal Task(int id, DateTime creationTime, string title, string description)
        {
            this.Id = id;
            this.CreationTime = creationTime;
            this.Title = title;
            this.Description = description;
        }
        // You can add code here
        public override string ToString()
        {
            string ret = "";
            ret += "-TASK-"+"\n";
            ret += "id: " + Id+"\n";
            ret += "title: " + Title+"\n";
            ret += "description: " + Description+"\n";
            ret += "creation time: " + CreationTime+"\n";
            return ret;
        }
    }
}
