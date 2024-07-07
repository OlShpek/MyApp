using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyApp
{
    internal class Task : IItem
    {
        private string content;
        private DateTime dueDate;
        private TimeSpan expTime;
        public Task(string content, DateTime dueDate, TimeSpan expTime) : base()
        {
            this.content = content;
            this.dueDate = dueDate;
            this.expTime = expTime;
        }

        public bool is_overdue()
        {
            return DateTime.Now < dueDate;
        }

        public override XmlElement getXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement el = doc.CreateElement("task");
            el.SetAttribute("id", id.ToString());
            el.SetAttribute("content", content);
            el.SetAttribute("dueDate", dueDate.ToString());
            el.SetAttribute("expTime", expTime.ToString());
            return el;
        }

        public string Content { get { return content; }  set { content = value; } }
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; } }
        public TimeSpan ExpTime { get { return expTime; } set { expTime = value; } }
        public long Id { get { return id; } }
    }
}
