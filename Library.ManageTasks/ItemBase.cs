using System;
namespace Library.ManageTasks
{
    public class ItemBase
    {
        private static int currentId = 1;
        private int _id = -1;

        object _lock = new object();

        public ItemBase()
        {
            
        }

        public int Id {
            get
            {
                lock(_lock)
                {
                    if(_id < 0)
                    {
                        _id = currentId++;
                    }
                    return _id;
                }
            }

        }
        public string Name { get; set; }
        public string Description { get; set; }
   
        public bool isCompleted { get; set; }

    }
}