using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    internal class Date : ItemList
    {
        DateTime realDate;
        TaskList tl;
        public Date(DateTime rD, List<IItem> events, TaskList t, string name, string tagName) : base(events, name, tagName)
        {
            realDate = rD;
            tl = t;
        }
        public Date(XElement el) : base(el)
        {
            realDate = DateTime.Parse(el.Attribute("date").Value);
            List<XElement> events = el.Elements("event").ToList<XElement>();
            this.tasks = new List<IItem>();
            for (int i = 0; i < events.Count; i++)
            {
                this.tasks.Add(new Event(events[i]));
            }
            FillBoolList(tasks.Count);
            tl = new TaskList(el.Element("tasks"));
        }

        public bool IsEqual(DateTime d)
        {
            return d.Day == realDate.Day && d.Month == realDate.Month && d.Year == realDate.Year;
        }
        public bool IsEmpty()
        {
            return tl.Count == 0 && tasks.Count == 0;
        }
        public Event GetElementAt(int i)
        {
            return (Event)tasks[i];
        }
        public TaskList Subtasks { get { return tl; } }
        public DateTime RealDate { get { return realDate; } }
    }
}
