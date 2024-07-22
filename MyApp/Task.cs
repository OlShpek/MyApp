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
        public Task(string content, DateTime dueDate, TimeSpan expTime, string tagName) : base(tagName)
        {
            this.content = content;
            this.dueDate = dueDate;
            this.expTime = expTime;
            completed = false;
        }

        public bool IsOverdue()
        {
            return DateTime.Now < dueDate;
        }

        public override XElement GetXml()
        {
            XElement el = new XElement(tagName);
            el.SetAttributeValue("id", id);
            el.SetAttributeValue("content", content);
            el.SetAttributeValue("dueDate", dueDate.ToString());
            el.SetAttributeValue("expTime", expTime.ToString());
            el.SetAttributeValue("completed", completed.ToString());
            return el;
        }

        public void ChangeComp()
        {
            completed = !completed;
        }
        public string Content { get { return content; }  set { content = value; } }
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; } }
        public TimeSpan ExpTime { get { return expTime; } set { expTime = value; } }
        public bool Completed { get { return completed; } }
    }
}
