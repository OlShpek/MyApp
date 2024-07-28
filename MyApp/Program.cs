using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MyApp
{
    class Program
    { 
        public static void DisplayTags(List<string> tags, ColouredItemList globalTags)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                ColouredItem tag = (ColouredItem)globalTags.GetItemById(tags[i]);
                ConsoleColor cl = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), tag.Colour);
                Console.ForegroundColor = cl;
                Console.Write(tag.Content + " ");
            }    
        }
        public static DateTime EnterDate()
        {
            int d = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            return new DateTime(y, m, d, 23, 59, 59);
        }

        public static void TaskDisplay(Task t, ColouredItemList tags)
        {
            if (t.Completed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (t.IsOverdue())
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(t.Content + " " + t.DueDate.ToString() + " " + t.ExpTime.ToString() + " ");
            DisplayTags(t.Tags, tags);
            Console.WriteLine();
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
            Console.ResetColor();
            Console.WriteLine("To add a task press a \n to change a task press ch \n to delete a task press d \n to mark task as completed press c \n to show all tasks type all \n to exit press e");
            XmlOperator xml = new XmlOperator("tasks.xml", new TaskList(new List<IItem>(), "my list", "list"));
            XElement el = xml.Load("list");
            TaskList tl = new TaskList(el, el.Attribute("id").Value, el.Name.LocalName);
            XmlOperator xmlTag = new XmlOperator("tags.xml", new ColouredItemList(new List<IItem>(), "tags", "tagList"));
            XElement tagEl = xmlTag.LoadFromList();
            ColouredItemList tags = new ColouredItemList(tagEl, "tagList", tagEl.Attribute("id").Value);
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
                    TaskDisplay(tl.GetElementAt(i), tags);
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
                        if (!tags.Exists(t))
                        {
                            Console.WriteLine("Please Specify the colour of the tag");
                            string col = Console.ReadLine();
                            ColouredItem ci = new ColouredItem(t, col, "tag");
                            tags.AddItem(ci);
                            tl.GetElementAt(id).AddTag(ci.Id);
                        }
                        else
                        {
                            tl.GetElementAt(id).AddTag(tags.GetElementByName(t).Id);
                        }
                        
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
                        Console.WriteLine("Please Enter the tag name");
                        string t = Console.ReadLine();
                        tl.GetElementAt(id).RemoveTag(tags.GetElementByName(t).Id);
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
                    else if (c1 == "tag colour")
                    {
                        Console.WriteLine("You can now specify tag colour; Enter tag Name");
                        string cont = Console.ReadLine();
                        Console.WriteLine("Enter tag Colour");
                        string col = Console.ReadLine();
                        tags.SpecifyColour(cont, col, "tag");
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
                        TaskDisplay(tl.GetElementAt(i), tags);
                    }
                    Console.WriteLine("\n\n\n\n\n");
                }
                XmlOperator xml1 = new XmlOperator("tasks.xml", tl);
                XmlOperator xmlT = new XmlOperator("tags.xml", tags);
                xmlT.UpdateData();
                xml1.UpdateData();
            }
        }
    }

}

