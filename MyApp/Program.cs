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

            XmlOperator xml = new XmlOperator("tasks.xml", new TaskList(new List<IItem>(), "my list", "list"));
            XElement el = xml.Load("list");
            TaskList tl = new TaskList(el, el.Attribute("id").Value, el.Name.LocalName);
            Console.WriteLine("Current Tasks Are: ");
            for (int i = 0; i < tl.Tasks.Count; i++)
            {
                Console.WriteLine(tl.GetElementAt(i).Content + " " + tl.GetElementAt(i).DueDate.ToString() + " " + tl.GetElementAt(i).ExpTime.ToString());
            }
            while(true)
            {
                string c = Console.ReadLine();
                if (c == "e")
                {
                    break;
                }
                if (c == "a")
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
                    XmlOperator xml1 = new XmlOperator("tasks.xml", tl);
                    xml1.UpdateData();
                }
            }
        }
    }

}

