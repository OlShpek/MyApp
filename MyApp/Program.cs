using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MyApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.ResetColor();
            Console.WriteLine("To add a task press a \n to change a task press ch \n to delete a task press d \n to mark task as completed press c \n to show all tasks type all \n to exit press e");
            TaskManagerSystem tms = new TaskManagerSystem();
            tms.MainProcess();
            
            /*XmlOperator xml = new XmlOperator("tasks.xml");
            TaskList tl = new TaskList(xml.Load("list"));
            XmlOperator xmlTag = new XmlOperator("tags.xml");
            ColouredItemList tags = new ColouredItemList(xmlTag.Load("tagList"));
            XmlOperator xmlUrg = new XmlOperator("urglev.xml");
            ColouredItemList urgLevels = new ColouredItemList(xmlUrg.Load("LevelList"));
            while(true)
            {
                ConsoleTask.TaskListDisplay(tl, urgLevels, tags, "Current tasks are", false);
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
                        tl.AddItem(ConsoleTask.CreateTask("You can create a task"));
                    }
                    else if (c1 == "tag")
                    {
                        int id = ConsoleTask.GetTaskId("You can add a tag to the already existing task");
                        string t = ConsoleTags.EnterStr("Please Enter the tag", 1)[0];
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
                    else if (c1 == "urgency level")
                    {
                        int id = ConsoleTask.GetTaskId("You can add a tag to the already existing task");
                        string t = ConsoleTags.EnterStr("Please Enter the urgency level name", 1)[0];
                        if (!urgLevels.Exists(t))
                        {
                            Console.WriteLine("Please Specify the colour of the urgency level and its importance value");
                            string col = Console.ReadLine();
                            int impVal = int.Parse(Console.ReadLine());
                            UrgencyLevel ul = new UrgencyLevel(t, col, impVal, "level");
                            tl.GetElementAt(id).UrgencyLevel = ul.Id;
                            urgLevels.AddItem(ul);
                        }
                        else
                        {
                            tl.GetElementAt(id).UrgencyLevel = urgLevels.GetElementByName(t).Id;
                        }
                    }
                }
                if (c == "d")
                {
                    string c1 = Console.ReadLine();
                    if (c1 == "task")
                    {
                        tl.RemoveItem(tl.GetElementAt(ConsoleTask.GetTaskId()).Id);
                    }
                    else if (c1 == "tag")
                    {
                        int id = ConsoleTask.GetTaskId("You can delete a tag of the already existing task");
                        string t = ConsoleTags.EnterStr("Please Enter the tag name", 1)[0];
                        tl.GetElementAt(id).RemoveTag(tags.GetElementByName(t).Id);
                    }
                    else if (c1 == "urgency level")
                    {
                        Console.WriteLine("You can delete an urgency level of the already existing task"); 
                        tl.GetElementAt(ConsoleTask.GetTaskId()).UrgencyLevel = "0";
                    }
                    else if (c1 == "glob tag")
                    {
                        string cont = ConsoleTags.EnterStr("You can delete tag globaly; Please enter tag name", 1)[0];
                        tags.RemoveItem(tags.GetElementByName(cont).Id);
                        tl.RemoveTags(tags.GetElementByName(cont).Id);
                    }
                    else if (c1 == "glob urg level")
                    {
                        string cont = ConsoleTags.EnterStr("You can delete tag globaly; Please enter tag name", 1)[0];
                        urgLevels.RemoveItem(urgLevels.GetElementByName(cont).Id);
                        tl.RemoveULevel(urgLevels.GetElementByName(cont).Id);
                    }
                }
                if (c == "ch")
                {
                    string c1 = Console.ReadLine();
                    if (c1 == "gen")
                    {
                        int id = ConsoleTask.GetTaskId();
                        Task t = ConsoleTask.CreateTask("You can update a task");
                        tl.GetElementAt(id).Content = t.Content;
                        tl.GetElementAt(id).DueDate = t.DueDate;
                        tl.GetElementAt(id).ExpTime = t.ExpTime;
                    }
                    else if (c1 == "spec time")
                    {
                        List<int> comm = ConsoleTask.SpecifyTimeSpan();
                        tl.GetElementAt(comm[0]).SpecifyTime(comm[1], comm[2], comm[3]);
                    }
                    else if (c1 == "tag colour")
                    {
                        Console.WriteLine("You can now specify tag colour; Enter tag Name");
                        List<string> str = ConsoleTags.EnterStr("You can now specify tag colour; Enter tag Name and colour next", 2);
                        tags.SpecifyColour(str[0], str[1], "tag");
                    }
                    else if (c1 == "glob tag name")
                    { 
                        List<string> str = ConsoleTags.EnterStr("You can now change the tag name globally. Please Enter the old and the new name", 2);
                        tags.GetElementByName(str[0]).Content = str[1];
                    }
                    else if (c1 == "glob urg name")
                    {
                        Console.WriteLine("You can now change the urgency level name globally. Please Enter the old and the new name");
                        List<string> str = ConsoleTags.EnterStr("You can now change the urgency level name globally. Please Enter the old and the new name", 2);
                        urgLevels.GetElementByName(str[0]).Content = str[1];
                    }
                    else if (c1 == "glob urg value")
                    {
                        Console.WriteLine("You can now change the value of the urgency level; Enter the urgency level name and then its value");
                        string cont = Console.ReadLine();
                        int val = int.Parse(Console.ReadLine());
                        UrgencyLevel ul = (UrgencyLevel)urgLevels.GetElementByName(cont);
                        ul.ULevel = val;
                    }    
                }
                if (c == "c")
                {
                    tl.GetElementAt(ConsoleTask.GetTaskId()).ChangeComp();
                }
                if (c == "all")
                {
                    ConsoleTask.TaskListDisplay(tl, urgLevels, tags, "Your all tasks are", true);
                }
                XmlOperator xml1 = new XmlOperator("tasks.xml", tl);
                XmlOperator xmlT = new XmlOperator("tags.xml", tags);
                XmlOperator xmlU = new XmlOperator("urglev.xml", urgLevels);
                xmlU.UpdateData();
                xmlT.UpdateData();
                xml1.UpdateData();
            }*/
        }
    }

}

