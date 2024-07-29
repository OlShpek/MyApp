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
        int urg_level;
        public Task(string content, DateTime dueDate, TimeSpan expTime, string tagName) : base(tagName)
        {
            this.content = content;
            this.dueDate = dueDate;
            this.expTime = expTime;
            completed = false;
            tags = new List<string>();
            urg_level = 0;
        }

        public Task(XElement el, string tagName, string id) : base(tagName, id)
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
                urg_level = 0;
            }
            else
            {
                urg_level = int.Parse(el.Attribute("urglevel").Value);
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
            el.SetAttributeValue("urglevel", urg_level.ToString());
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
        public int UrgencyLevel { get { return urg_level; } set { urg_level = value; } }
        public List<string> Tags { get { return tags; } }
        public bool Completed { get { return completed; } }
    }
}
