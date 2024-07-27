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
            return new DateTime(y, m, d);
        }

        public static TimeSpan EnterTimeSpan()
        {
            int h = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            return new TimeSpan(h, m, 0);
        }
        public static void Main(string[] args)
        {
            /*Task t = new Task("Skething Curves OppOx", new DateTime(2024, 7, 8), new TimeSpan(2, 0, 0), "task");
            Task t1 = new Task("Skething Curves OppOxA", new DateTime(2024, 8, 9), new TimeSpan(2, 0, 0), "task");
            Task t2 = new Task("Skething Curves OppOxB", new DateTime(2024, 9, 10), new TimeSpan(2, 0, 0), "task");
            ItemList l = new ItemList(new List<IItem>(), "my list", "list");
            l.AddItem(t);
            l.AddItem(t1);
            l.AddItem(t2);
            XmlOperator xml = new XmlOperator("tasks.xml", l);
            xml.UpdateData();
            t.Content = "Well-defined";
            l.ChangeItem(t);
            l.RemoveItem(t2.Id);
            xml.UpdateData();
            l.AddItem(new Task("Skething Curves OppOx1", new DateTime(2024, 7, 8), new TimeSpan(2, 0, 0), "task"));
            l.AddItem(new Task("Skething Curves OppOx2", new DateTime(2024, 7, 8), new TimeSpan(2, 0, 0), "task"));
            xml.UpdateData();*/
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
                    Console.WriteLine(tl.GetElementAt(i).Content + " " + tl.GetElementAt(i).DueDate.ToString() + " " + tl.GetElementAt(i).ExpTime.ToString() + " " + string.Join(", ", tl.GetElementAt(i).Tags));
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
                if (c == "c")
                {
                    Console.WriteLine("Enter task id");
                    int id = int.Parse(Console.ReadLine());
                    tl.GetElementAt(id).ChangeComp();
                }
                if (c == "all")
                {
                    Console.WriteLine("All Tasks Are: ");
                    for (int i = 0; i < tl.Tasks.Count; i++)
                    {
                        if (tl.IsDeleted(i))
                        {
                            continue;
                        }
                        Console.WriteLine("Task id: " + i.ToString());
                        Console.WriteLine(tl.GetElementAt(i).Content + " " + tl.GetElementAt(i).DueDate.ToString() + " " + tl.GetElementAt(i).ExpTime.ToString());
                    }
                }
                XmlOperator xml1 = new XmlOperator("tasks.xml", tl);
                xml1.UpdateData();
            }
        }
    }

}

