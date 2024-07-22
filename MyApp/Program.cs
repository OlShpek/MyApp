using System;
using System.Collections.Generic;

namespace MyApp
{
    class Program
    { 
        public static void Main(string[] args)
        {
            Task t = new Task("Skething Curves OppOx", new DateTime(2024, 7, 8), new TimeSpan(2, 0, 0), "task");
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
            xml.UpdateData();
        }
    }

}

