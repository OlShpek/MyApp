using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    internal class Task : IItem
    {
        string content;
        DateTime dueDate;
        TimeSpan expTime;
        bool completed;
        List<string> tags;
        string urg_level;
        TaskList subtasks;
        public Task(string content, DateTime dueDate, TimeSpan expTime, string tagName) : base(tagName)
        {
            this.content = content;
            this.dueDate = dueDate;
            this.expTime = expTime;
            completed = false;
            tags = new List<string>();
            urg_level = "0";
            subtasks = new TaskList(new List<IItem>(), "mylist", "subtasks");
        }

        public Task(XElement el) : base(el)
        {
            this.content = el.Attribute("content").Value;
            this.dueDate = DateTime.Parse(el.Attribute("dueDate").Value);
            this.expTime = TimeSpan.Parse(el.Attribute("expTime").Value);
            this.completed = el.Attribute("completed").Value == "True";
            if (el.Attribute("tags") == null)
            {
                this.tags = new List<string>();
            }
            else
            {
                this.tags = el.Attribute("tags").Value.Split(';').ToList<string>();
                if (tags.Count == 1)
                {
                    if (tags[0] == "")
                    {
                        tags.RemoveAt(0);
                    }
                }
            }
            if (el.Attribute("urglevel") == null)
            {
                urg_level = "0";
            }
            else
            {
                urg_level = el.Attribute("urglevel").Value;
            }
            if (el.HasElements)
            {
                XElement mainel = el.Element("subtasks");
                subtasks = new TaskList(mainel);
            }
            else
            {
                subtasks = new TaskList(new List<IItem>(), "mylist", "subtasks");
            }
        }
        public bool IsOverdue()
        {
            return DateTime.Now > dueDate;
        }

        public void SpecifyTime(int h, int m, int s)
        {
            dueDate = new DateTime(dueDate.Year, dueDate.Month, dueDate.Day, h, m, s);
        }

        public override XElement GetXml()
        {
            XElement el = new XElement(tagName);
            el.SetAttributeValue("id", id);
            el.SetAttributeValue("content", content);
            el.SetAttributeValue("dueDate", dueDate.ToString());
            el.SetAttributeValue("expTime", expTime.ToString());
            el.SetAttributeValue("completed", completed.ToString());
            el.SetAttributeValue("tags", TagsToString());
            el.SetAttributeValue("urglevel", urg_level);
            el.Add(subtasks.GetXml());
            return el;
        }

        public void AddTag(string t)
        {
            tags.Add(t);
        }

        public void RemoveTag(string t)
        {
            tags.Remove(t);
        }

        public void AddSubTask(Task t)
        {
            subtasks.AddItem(t);
        }

        public void RemoveSubTask(Task t)
        {
            subtasks.RemoveItem(t.Id);
        }

        public void AutoCreate(string mainCont, int count, int chPart)
        {
            for (int i = chPart; i < count + chPart; i++)
            {
                Task t = new Task(mainCont + chPart.ToString(), dueDate, expTime, tagName);
                subtasks.AddItem(t);
            }
        }
        private string TagsToString()
        {
            return string.Join(';', tags.ToArray());
        }

        public void ChangeComp()
        {
            completed = !completed;
        }
        public string Content { get { return content; }  set { content = value; } }
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; } }
        public TimeSpan ExpTime { get { return expTime; } set { expTime = value; } }
        public string UrgencyLevel { get { return urg_level; } set { urg_level = value; } }
        public List<string> Tags { get { return tags; } }
        public TaskList Subtasks { get { return subtasks; } }
        public bool Completed { get { return completed; } }
    }
}
