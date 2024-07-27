using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MyApp
{
    class Program
    { 
        public static DateTime EnterDate()
        {
            int d = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            return new DateTime(y, m, d, 23, 59, 59);
        }

        public static void TaskDisplay(Task t)
        {
            if (t.Completed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (t.IsOverdue())
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(t.Content + " " + t.DueDate.ToString() + " " + t.ExpTime.ToString() + " " + string.Join(", ", t.Tags));
            Console.ResetColor();

        }
        public static TimeSpan EnterTimeSpan()
        {
            int h = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            return new TimeSpan(h, m, 0);
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("To add a task press a \n to change a task press ch \n to delete a task press d \n to mark task as completed press c \n to show all tasks type all \n to exit press e");
            XmlOperator xml = new XmlOperator("tasks.xml", new TaskList(new List<IItem>(), "my list", "list"));
            XElement el = xml.Load("list");
            TaskList tl = new TaskList(el, el.Attribute("id").Value, el.Name.LocalName);
            while(true)
            {
                Console.WriteLine("Current Tasks Are: ");
                for (int i = 0; i < tl.Tasks.Count; i++)
                {
                    if (tl.GetElementAt(i).Completed || tl.IsDeleted(i))
                    {
                        continue;
                    }
                    Console.WriteLine("Task id: " + i.ToString());
                    TaskDisplay(tl.GetElementAt(i));
                }
                string c = Console.ReadLine();
                if (c == "e")
                {
                    break;
                }
                if (c == "a")
                {
                    string c1 = Console.ReadLine();
                    if (c1 == "task")
                    {
                        Console.WriteLine("You can create a task");
                        Console.Write("Enter the content: ");
                        string cont = Console.ReadLine();
                        Console.Write("Enter the date: ");
                        DateTime dt = EnterDate();
                        Console.Write("Enter the time span: ");
                        TimeSpan ts = EnterTimeSpan();
                        Task nt = new Task(cont, dt, ts, "task");
                        tl.AddItem(nt);
                    }
                    else if (c1 == "tag")
                    {
                        Console.WriteLine("You can add a tag to the already existing task; Please Enter the task ID");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please Enter the tag");
                        string t = Console.ReadLine();
                        Task nt = tl.GetElementAt(id);
                        nt.AddTag(t);
                        tl.ChangeItem(nt);
                    }
                }
                if (c == "d")
                {
                    string c1 = Console.ReadLine();
                    if (c1 == "task")
                    {
                        Console.WriteLine("Enter task id");
                        int id = int.Parse(Console.ReadLine());
                        tl.RemoveItem(tl.GetElementAt(id).Id);
                    }
                    else if (c1 == "tag")
                    {
                        Console.WriteLine("You can delete a tag to the already existing task; Please enter the task ID");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please Enter the tag");
                        string t = Console.ReadLine();
                        Task nt = tl.GetElementAt(id);
                        nt.RemoveTag(t);
                        tl.ChangeItem(nt);
                    }
                }
                if (c == "ch")
                {
                    string c1 = Console.ReadLine();
                    if (c1 == "gen")
                    {
                        Console.WriteLine("Enter task id");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("You can update a task");
                        Console.Write("Enter the content: ");
                        string cont = Console.ReadLine();
                        Console.Write("Enter the date: ");
                        DateTime dt = EnterDate();
                        Console.Write("Enter the time span: ");
                        TimeSpan ts = EnterTimeSpan();
                        tl.GetElementAt(id).Content = cont;
                        tl.GetElementAt(id).DueDate = dt;
                        tl.GetElementAt(id).ExpTime = ts;
                    }
                    else if (c1 == "spec time")
                    {
                        Console.WriteLine("You can now specify the due time of the task");
                        Console.WriteLine("Enter task id");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter hours, minuts and seconds next");
                        int h = int.Parse(Console.ReadLine());
                        int m = int.Parse(Console.ReadLine());
                        int s = int.Parse(Console.ReadLine());
                        tl.GetElementAt(id).SpecifyTime(h, m, s);
                    }
                }
                if (c == "c")
                {
                    Console.WriteLine("Enter task id");
                    int id = int.Parse(Console.ReadLine());
                    tl.GetElementAt(id).ChangeComp();
                }
                if (c == "all")
                {
                    Console.WriteLine("\n\n\n\n\n");
                    Console.WriteLine("All Tasks Are: ");
                    for (int i = 0; i < tl.Tasks.Count; i++)
                    {
                        if (tl.IsDeleted(i))
                        {
                            continue;
                        }
                        Console.WriteLine("Task id: " + i.ToString());
                        TaskDisplay(tl.GetElementAt(i));
                    }
                    Console.WriteLine("\n\n\n\n\n");
                }
                XmlOperator xml1 = new XmlOperator("tasks.xml", tl);
                xml1.UpdateData();
            }
        }
    }

}

