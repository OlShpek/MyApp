using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class TaskManagerSystem
    {
        TaskList tl;
        ColouredItemList tags;
        ColouredItemList urgLevels;
        bool local;
        public TaskManagerSystem()
        {
            XmlOperator xml = new XmlOperator("tasks.xml");
            XmlOperator xmlTag = new XmlOperator("tags.xml");
            XmlOperator xmlUrg = new XmlOperator("urglev.xml");

            tl = new TaskList(xml.Load("list"));
            tags = new ColouredItemList(xmlTag.Load("tagList"));
            urgLevels = new ColouredItemList(xmlUrg.Load("LevelList"));
            local = false;
        }

        public TaskManagerSystem(TaskList tl, ColouredItemList tags, ColouredItemList urgLevels)
        {
            this.tl = tl;
            this.tags = tags;
            this.urgLevels = urgLevels;
            local = true;
        }

        public void MainProcess()
        {
            bool b = false;
            while (true)
            {
                ConsoleTask.TaskListDisplay(tl, urgLevels, tags, "Current tasks are", b);
                string command = ConsoleTags.EnterStr("Please Enter a command", 1)[0];
                b = false;
                if (command == "a")
                {
                    Addition();
                }
                else if (command == "d")
                {
                    Deletion();
                }
                else if (command == "ch")
                {
                    Change();
                }
                else if (command == "c")
                {
                    tl.GetElementAt(ConsoleTask.GetTaskId()).ChangeComp();
                }
                else if (command == "all")
                {
                    b = true;
                }
                else if (command == "move")
                {
                    Move();
                }
                else if (command == "e")
                {
                    break;
                }
                if (!local)
                {
                    DataUpdate();
                }
            }
        }

        private void Move()
        {
            string command = Console.ReadLine();
            if (command == "to task")
            {
                int id = ConsoleTask.GetTaskId("To which task do you want to move?");
                TaskManagerSystem tm = new TaskManagerSystem(tl.GetElementAt(id).Subtasks, tags, urgLevels);
                tm.MainProcess();
            }
        }
        private void Addition()
        {
            string command = ConsoleTags.EnterStr("You can add further instructions", 1)[0];
            if (command == "task")
            {
                tl.AddItem(ConsoleTask.CreateTask("You can create a task"));
            }
            else if (command == "subtask")
            {
                int id = ConsoleTask.GetTaskId("You can add a subtask to a task");
                tl.GetElementAt(id).AddSubTask(ConsoleTask.CreateTask("Enter subtask details"));
            }
            else if (command == "atc subtask")
            {
                int id = ConsoleTask.GetTaskId("You can auto create several subtask");
                List<string> strs = ConsoleTags.EnterStr("Enter how many subtasks, starting point and the common string", 3);
                tl.GetElementAt(id).AutoCreate(strs[2], int.Parse(strs[0]), int.Parse(strs[1]));
            }
            else if (command == "tag")
            {
                int id = ConsoleTask.GetTaskId();
                Tuple<ColouredItem, bool> pair = CreateTag(command, tags);
                tl.GetElementAt(id).AddTag(pair.Item1.Id);
                if (!pair.Item2)
                {
                    tags.AddItem(pair.Item1);
                }
            }
            else if (command == "urgency level")
            {
                int id = ConsoleTask.GetTaskId();
                Tuple<ColouredItem, bool> pair = CreateTag(command, urgLevels);
                tl.GetElementAt(id).UrgencyLevel = pair.Item1.Id;
                if (!pair.Item2)
                {
                    urgLevels.AddItem((UrgencyLevel)pair.Item1);
                }
            }
            else if (command == "to all")
            {
                AddToAll();
            }

        }

        private void AddToAll()
        {
            string command = Console.ReadLine();
            if (command == "tag")
            {
                Tuple<ColouredItem, bool> tg = CreateTag(command, tags);
                if (!tg.Item2)
                {
                    tags.AddItem(tg.Item1);
                }
                for (int i = 0; i < tl.Count; i++)
                {
                    tl.GetElementAt(i).AddTag(tg.Item1.Id);
                }
            }
            else if (command == "urgency level")
            {
                Tuple<ColouredItem, bool> ul = CreateTag(command, urgLevels);
                if (!ul.Item2)
                {
                    urgLevels.AddItem(ul.Item1);
                }
                for (int i = 0; i < tl.Count; i++)
                {
                    tl.GetElementAt(i).UrgencyLevel = ul.Item1.Id;
                }
            }
        }
        private void Deletion()
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

        private void Change()
        {
            string command = Console.ReadLine();
            if (command == "task")
            {
                ChangeTask();
            }
            else if (command == "tag")
            {
                ChangeTag();
            }
            else if (command == "urgency level")
            {
                ChangeULevel();
            }
        }

        private void ChangeTag()
        {
            string c1 = Console.ReadLine();
            if (c1 == "tag colour")
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
        }

        private void ChangeULevel()
        {
            string c1 = Console.ReadLine();
            if (c1 == "glob urg name")
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
                UrgencyLevel ul = (UrgencyLevel)(urgLevels.GetElementByName(cont));
                ul.ULevel = val;
            }
        }

        private void ChangeTask()
        {
            string command = Console.ReadLine();
            if (command == "gen")
            {
                int id = ConsoleTask.GetTaskId();
                Task t = ConsoleTask.CreateTask("You can update a task");
                tl.GetElementAt(id).Content = t.Content;
                tl.GetElementAt(id).DueDate = t.DueDate;
                tl.GetElementAt(id).ExpTime = t.ExpTime;
            }
            else if (command == "spec time")
            {
                List<int> comm = ConsoleTask.SpecifyTimeSpan();
                tl.GetElementAt(comm[0]).SpecifyTime(comm[1], comm[2], comm[3]);
            }
            else if (command == "content only")
            {
                int id = ConsoleTask.GetTaskId();
                Console.WriteLine("You can update the content of the already existing task");
                string cont = Console.ReadLine();
                tl.GetElementAt(id).Content = cont;
            }
            else if (command == "date only")
            {
                int id = ConsoleTask.GetTaskId();
                Console.WriteLine("You can update the due date of the task");
                tl.GetElementAt(id).DueDate = ConsoleTask.EnterDate();
            }
            else if (command == "timespan only")
            {
                int id = ConsoleTask.GetTaskId();
                Console.WriteLine("You can update timespan of the task");
                tl.GetElementAt(id).ExpTime = ConsoleTask.EnterTimeSpan();
            }    
        }
        private Tuple<ColouredItem, bool> CreateTag(string command, ColouredItemList cl)
        {
            List<string> t = ConsoleTags.EnterStr("Enter name of the element", 1);   
            if (cl.Exists(t[0]))
            {
                return new Tuple<ColouredItem, bool>(cl.GetElementByName(t[0]), true);
            }
            Console.WriteLine("Enter the colour of the tag");
            string col = Console.ReadLine();
            if (command == "tag")
            {
                ColouredItem tg = new ColouredItem(t[0], col, "tag");
                return new Tuple<ColouredItem, bool>(tg, false);
            }
            else
            {
                Console.WriteLine("Enter the importance value");
                int imp = int.Parse(Console.ReadLine());
                UrgencyLevel ul = new UrgencyLevel(t[0], col, imp, "level");
                return new Tuple<ColouredItem, bool>(ul, false);
            }
        }
        private void DataUpdate()
        {
            XmlOperator xml1 = new XmlOperator("tasks.xml", tl);
            XmlOperator xmlT = new XmlOperator("tags.xml", tags);
            XmlOperator xmlU = new XmlOperator("urglev.xml", urgLevels);

            xmlU.UpdateData();
            xmlT.UpdateData();
            xml1.UpdateData();
        }
    }
}
