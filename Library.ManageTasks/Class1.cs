using System;

namespace Library.ManageTasks
{
    public class Task: ItemBase
    {

        public Task() : base()
        {
            
        }

        public DateTime Deadline { get; set; }
        

        public override string ToString()
        {
            return Id + ". " + Name + " : " + Description + " - " + Deadline + " ( " + isCompleted + " )";
        }
    }
}
