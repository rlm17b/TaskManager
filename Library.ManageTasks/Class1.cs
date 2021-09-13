using System;

namespace Library.ManageTasks
{
    public class Task
    {
        private static int currentId = 1;

        public Task()
        {
            Id = currentId++;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool isCompleted { get; set; }

        public override string ToString()
        {
            return Id + ". " + Name + " - " + Description + " - " + Deadline + " - " + isCompleted;
        }
    }
}
