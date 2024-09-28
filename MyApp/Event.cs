using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    class Event : IItem
    {
        string content;

        public Event(string content, string tagName) : base(tagName)
        {
            this.content = content;
        }

        public Event(XElement e) : base(e)
        {
            content = e.Attribute("content").Value;
        }

        public override XElement GetXml()
        {
            XElement el = new XElement(tagName);
            el.SetAttributeValue("id", id);
            el.SetAttributeValue("content", content);
            return el;
        }

        public string Content { get { return content; } set { content = value; } }
    }
}
