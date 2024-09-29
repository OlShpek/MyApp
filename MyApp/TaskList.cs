using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace MyApp
{
    class TaskList : ItemList
    {
        public TaskList(List<IItem> tasks, string name, string tagName) : base(tasks, name, tagName)
        { }
        public TaskList(XElement el) : base(el)
        {
            List<XElement> els = el.Elements().ToList<XElement>();
            tasks = new List<IItem>();
            for (int i = 0; i < els.Count; i++)
            {
                tasks.Add(new Task(els[i]));
            }
            FillBoolList(tasks.Count);
            name = el.Name.LocalName;
        }

        public Task GetElementAt(int i)
        {
            return (Task)tasks[i];
        }

        public Task GetElementAt(List<int> paths, int curr)
        {
            if (curr == paths.Count - 1)
            {
                return GetElementAt(paths[curr]);
            }
            else
            {
                return GetElementAt(paths[curr]).Subtasks.GetElementAt(paths, curr + 1);
            }
        }
        public void RemoveTags(string id)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                GetElementAt(i).RemoveTag(id);
            }
        }

        public void RemoveULevel(string id)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (GetElementAt(i).UrgencyLevel == id)
                {
                    GetElementAt(i).UrgencyLevel = "0";
                }
            }
        }

        public bool RemoveTask(string id)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                {
                    deleted[i] = true;
                    return true;
                }
                if (GetElementAt(i).Subtasks.RemoveTask(id))
                {
                    return true;
                }
            }
            return false;
        }
        public TaskList Sort()
        {
            List<Task> nl = ConvertToListTask();
            nl.Sort(delegate (Task t1, Task t2) 
            {
                return t1.DueDate.CompareTo(t2.DueDate);
            });
            TaskList tl = new TaskList(nl.Cast<IItem>().ToList<IItem>(), name, tagName);
            return tl;
        }

        public TaskList SortByTimeSpan()
        {
            List<Task> nl = ConvertToListTask();
            nl.Sort(delegate (Task t1, Task t2) { return t1.ExpTime.CompareTo(t2.ExpTime); });
            return new TaskList(nl.Cast<IItem>().ToList<IItem>(), name, tagName);
        }

        public TaskList SortByUrgLevel(ColouredItemList urgLevels)
        {
            List<Task> nl = ConvertToListTask();
            nl.Sort(delegate (Task t1, Task t2)
            {
                int U1, U2;
                if (t1.UrgencyLevel == "0")
                {
                    U1 = 0;
                }
                else
                {
                    UrgencyLevel u1 = (UrgencyLevel)urgLevels.GetItemById(t1.UrgencyLevel);
                    U1 = u1.ULevel;
                }

                if (t2.UrgencyLevel == "0")
                {
                    U2 = 0;
                }
                else
                {
                    UrgencyLevel u2 = (UrgencyLevel)urgLevels.GetItemById(t2.UrgencyLevel);
                    U2 = u2.ULevel;
                }
                return U2.CompareTo(U1);
            });
            return new TaskList(nl.Cast<IItem>().ToList<IItem>(), name, tagName);
        }

        public List<IItem> GetAll()
        {
            List<IItem> fullList = new List<IItem>();
            for (int i = 0; i < tasks.Count; i++)
            {
                fullList.Add(tasks[i]);
                if (GetElementAt(i).HasSubtasks())
                {
                    fullList.AddRange(GetElementAt(i).Subtasks.GetAll());
                }
            }
            return fullList;
        }

        public List<IItem> GetAllByTag(string id)
        {
            List<IItem> fullList = new List<IItem>();
            for (int i = 0; i < tasks.Count; i++)
            {
                if (GetElementAt(i).HasTag(id))
                {
                    fullList.Add(tasks[i]);
                }
                if (GetElementAt(i).HasSubtasks())
                {
                    fullList.AddRange(GetElementAt(i).Subtasks.GetAllByTag(id));
                }
            }
            return fullList;
        }

        public List<IItem> GetAllByUrgencyLevel(string id)
        {
            List<IItem> fullList = new List<IItem>();
            for (int i = 0; i < tasks.Count; i++)
            {
                if (GetElementAt(i).UrgencyLevel == id)
                {
                    fullList.Add(tasks[i]);
                }
                if (GetElementAt(i).HasSubtasks())
                {
                    fullList.AddRange(GetElementAt(i).Subtasks.GetAllByUrgencyLevel(id));
                }
            }
            return fullList;
        }

        public List<IItem> GetAllByDueDate(DateTime due)
        {
            List<IItem> fullList = new List<IItem>();
            for (int i = 0; i < tasks.Count; i++)
            {
                if (GetElementAt(i).EqualDueDate(due))
                {
                    fullList.Add(tasks[i]);
                }
                if (GetElementAt(i).HasSubtasks())
                {
                    fullList.AddRange(GetElementAt(i).Subtasks.GetAllByDueDate(due));
                }
            }
            return fullList;
        }
        private List<Task> ConvertToListTask()
        {
            List<Task> conv = new List<Task>();
            for (int i = 0; i < tasks.Count; i++)
            {
                conv.Add((Task)tasks[i]);
            }
            return conv;
        }
    }
}
