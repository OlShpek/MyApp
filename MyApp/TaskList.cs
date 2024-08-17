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

        public Task GetLast()
        {
            return (Task)tasks.Last<IItem>();
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
    }
}
