using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Library.ManageTasks;

namespace ManageTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ItemBase> taskList = null;
            if (File.Exists("SaveData.json"))
            {
                //deserialize the list
                taskList = JsonConvert.DeserializeObject<List<ItemBase>>(File.ReadAllText("SaveData.json"));
            }
            taskList = new List<ItemBase>();
            var tNavigator = new ListNavigator<ItemBase>(taskList, 5);

            Console.WriteLine("Welcome to the help desk!");
            bool cont = true;

            while (cont)
            {
                Console.WriteLine("Menu:\n1. Create a Task\n2. Delete Task or Appointment\n3. Edit task\n4. Complete a task\n5. List outstanding " +
                    "tasks and appointments\n6. List all tasks and appointments\n7. Create an appointment\n8. edit and appointment\n9. search\n10.exit");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            //new task
                            createOrEditTask(taskList, null);
                            break;
                        case 2:
                            //delete task
                            PrintList(tNavigator);

                            Console.WriteLine("\nEnter task Id to be deleted");
                            if (int.TryParse(Console.ReadLine(), out int delete))
                            {
                                var tDelete = taskList.FirstOrDefault(t => t.Id == delete);
                                taskList.Remove(tDelete);
                            }
                            else
                            {
                                Console.WriteLine("Unknown Id");
                            }


                            break;
                        case 3:
                            //edit task
                            PrintList(tNavigator);

                            Console.WriteLine("\nEnter Id to be edited");
                            if (int.TryParse(Console.ReadLine(), out int edit))
                            {
                                var tEdit = taskList.FirstOrDefault(t => t.Id == edit);
                                createOrEditTask(taskList, tEdit);

                            }
                            else
                            {
                                Console.WriteLine("Unknown Id");
                            }


                            break;
                        case 4:
                            //complete task
                            PrintList(tNavigator);

                            Console.WriteLine("\nEnter Id to be completed");
                            if (int.TryParse(Console.ReadLine(), out int complete))
                            {
                                var tComplete = taskList.FirstOrDefault(t => t.Id == complete);
                                tComplete.isCompleted = true;
                            }
                            else
                            {
                                Console.WriteLine("Unknown Id");
                            }

                            break;
                        case 5:
                            //list outstanding tasks
                            foreach (var t in taskList)
                            {
                                if (t.isCompleted == false)
                                {
                                    Console.WriteLine(t.ToString());
                                }
                            }


                            break;
                        case 6:
                            //list all tasks
                            PrintList(tNavigator);

                            break;
                        case 7:
                            //create appointment
                            createOrEditAppointment(taskList, null);

                            break;
                        case 8:
                            //edit appointment
                            PrintList(tNavigator);

                            Console.WriteLine("\nEnter Id to be edited");
                            if (int.TryParse(Console.ReadLine(), out int edita))
                            {
                                var tEdit = taskList.FirstOrDefault(t => t.Id == edita);
                                createOrEditAppointment(taskList, tEdit);

                            }
                            else
                            {
                                Console.WriteLine("Unknown Id");
                            }

                            break;
                        case 9:
                            //search
                            Console.WriteLine("Enter number of category to search\n 1. name\n 2. description\n 3. attendees\n");
                            if (int.TryParse(Console.ReadLine(), out int sel))
                            {
                                switch (sel)
                                {
                                    case 1:
                                        //search by name
                                        Console.WriteLine("Enter name to search");
                                        var n = Console.ReadLine();
                                        var res = from v in taskList where v.Name == n select v;
                                        foreach(var p in res)
                                        {
                                            Console.WriteLine(p.ToString());
                                        }
                                        break;
                                    case 2:
                                        //search by description
                                        Console.WriteLine("Enter description to search");
                                        var n2 = Console.ReadLine();
                                        var res2 = from v2 in taskList where v2.Description == n2 select v2;
                                        foreach (var p2 in res2)
                                        {
                                            Console.WriteLine(p2.ToString());
                                        }
                                        break;
                                    case 3:
                                        //search by name
                                        Console.WriteLine("Enter attendee to search");
                                        var n3 = Console.ReadLine();

                                        var res3 = from v3 in taskList where v3 is Appointments from v4 in (v3 as Appointments).Attendees where v4 == n3 select v3;
                                        foreach (var p3 in res3)
                                        {
                                            Console.WriteLine((p3 as Appointments).ToString());
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Invalid option");
                                        break;
                                }


                            }
                            else
                            {
                                Console.WriteLine("invalid option");
                            }

                            break;
                        case 10:
                            //exit
                            cont = false;
                            File.WriteAllText("SaveData.json", JsonConvert.SerializeObject(taskList));

                            break;
                        default:
                            Console.WriteLine("Try again");
                            break;


                    }
                }
                else
                {
                    Console.WriteLine("Try again");

                }


            };

        }
        public static void createOrEditTask(List<ItemBase> taskList, ItemBase newTask = null)
        {
            bool newT = false;
            if (newTask == null)
            {
                newTask = new Task();
                newT = true;

            }
            Console.WriteLine("Enter new task");
            newTask.Name = Console.ReadLine();

            Console.WriteLine("Enter the description");
            newTask.Description = Console.ReadLine();
            if(newTask is Task)
            {
                Console.WriteLine("Enter the deadline");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime deadline))
                {
                    (newTask as Task).Deadline = deadline;
                }
                else
                {
                    Console.WriteLine("Invalid deadline; deadline is now for today");
                    (newTask as Task).Deadline = DateTime.Today;
                }
            }
            

            if (newT == true)
            {
                taskList.Add(newTask);
            }


        }
        public static void createOrEditAppointment(List<ItemBase> taskList, ItemBase newApp = null)
        {
            bool newT = false;
            if (newApp == null)
            {
                newApp = new Appointments();
                newT = true;

            }
            Console.WriteLine("Enter new Appointment");
            newApp.Name = Console.ReadLine();

            Console.WriteLine("Enter the description");
            newApp.Description = Console.ReadLine();
            if (newApp is Appointments)
            {
                Console.WriteLine("Enter Start");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime start))
                {
                    (newApp as Appointments).Start = start;
                }
                else
                {
                    Console.WriteLine("Invalid Start; Start is now for today");
                    (newApp as Appointments).Start = DateTime.Today;
                }

                Console.WriteLine("Enter Stop");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime stop))
                {
                    (newApp as Appointments).Stop = stop;
                }
                else
                {
                    Console.WriteLine("Invalid Stop; Stop is now for today");
                    (newApp as Appointments).Stop = DateTime.Today;

                }
                (newApp as Appointments).Deadline = (newApp as Appointments).Stop;

                //attendees list loop
                Console.WriteLine("Enter attendees, then enter e");

                (newApp as Appointments).Attendees.Clear();
                var l = true;
                while (l)
                {
                    var temp = Console.ReadLine();
                    if(temp.Equals("E", StringComparison.InvariantCultureIgnoreCase))
                    {
                        l = false;
                    }
                    else
                    {
                        ((Appointments)newApp).Attendees.Add(temp);
                    }
                }

            }
            


            if (newT == true)
            {
                taskList.Add(newApp);
            }
        }

        public static void PrintList(ListNavigator<ItemBase> listNavigator)
        {
            bool navigating = true;
            while (navigating)
            {
                var pg = listNavigator.GetCurrentPg();
                foreach (var item in pg)
                {
                    Console.WriteLine($"{item.Value}");
                }

                if (listNavigator.hasPreviousPg)
                {
                    Console.WriteLine("P. For Previous");
                }

                if (listNavigator.hasNextPg)
                {
                    Console.WriteLine("N. For Next");
                }

                var select = Console.ReadLine();
                if(select.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                {
                    listNavigator.GoBackward();
                }
                else if(select.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    listNavigator.GoForward();
                }
                else
                {
                    navigating = false;
                }
            }
            Console.WriteLine();
        }


    }
}