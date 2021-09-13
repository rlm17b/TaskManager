using System;
using System.Collections.Generic;
using System.Linq;
using Library.ManageTasks;

namespace ManageTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskList = new List<Task>();

            Console.WriteLine("Welcome to the help desk!");
            bool cont = true;

            while (cont)
            {
                Console.WriteLine("Menu:\n1. Create Task\n2. Delete Task\n3. Edit task\n4. Complete a task\n5. List outstanding " +
                    "tasks\n6. List all tasks\n7. exit");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            //new task
                            createTask(taskList, null);
                            break;
                        case 2:
                            //delete task
                            foreach (var task in taskList)
                            {
                                Console.WriteLine(task.ToString());
                            }

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
                            foreach (var task in taskList)
                            {
                                Console.WriteLine(task.ToString());
                            }

                            Console.WriteLine("\nEnter Id to be edited");
                            if (int.TryParse(Console.ReadLine(), out int edit))
                            {
                                var tEdit = taskList.FirstOrDefault(t => t.Id == edit);
                                createTask(taskList, tEdit);

                            }
                            else
                            {
                                Console.WriteLine("Unknown Id");
                            }


                            break;
                        case 4:
                            //complete task
                            foreach (var task in taskList)
                            {
                                Console.WriteLine(task.ToString());
                            }

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
                            foreach (var task in taskList)
                            {
                                if (task.isCompleted == false)
                                {
                                    Console.WriteLine(task.ToString());
                                }
                            }


                            break;
                        case 6:
                            //list all tasks
                            foreach (var task in taskList)
                            {
                                Console.WriteLine(task.ToString());
                            }

                            break;
                        case 7:
                            //exit
                            cont = false;
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
        public static void createTask(List<Task> taskList, Task newTask = null)
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

            Console.WriteLine("Enter the deadline");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime deadline))
            {
                newTask.Deadline = deadline;
            }
            else
            {
                Console.WriteLine("Invalid deadline; deadline is now for today");
                newTask.Deadline = DateTime.Today;
            }

            if (newT == true)
            {
                taskList.Add(newTask);
            }


        }


    }
}