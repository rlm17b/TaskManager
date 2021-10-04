using System;
using System.Collections.Generic;

namespace Library.ManageTasks
{
    public class Appointments: Task
    {
        public Appointments(): base()
        {
            
        }



        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public List<string> Attendees = new List<string>();


        public override string ToString() {
            String n = Id + ". " + Name + " : " + Description + " -  start: " + Start + "\tend: " + Stop + " ( " + isCompleted + " )  Attendees: ";
            foreach (var x in Attendees)
            {
                n += x + ", ";
            }
            return n;
            
        }


    }
}
