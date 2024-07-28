using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    class ColouredItem : IItem
    {
        protected string content;
        protected string colour;
        public ColouredItem(string content, string colour, string tagName) : base(tagName)
        {
            this.colour = colour;
            this.content = content;
        }

        public ColouredItem(XElement el, string tagName, string id) : base(tagName, id)
        {
            content = el.Attribute("content").Value;
            colour = el.Attribute("colour").Value;
        }

        public override XElement GetXml()
        {
            XElement el = new XElement(tagName);
            el.SetAttributeValue("id", id);
            el.SetAttributeValue("content", content);
            el.SetAttributeValue("colour", colour);
            return el;
        }

        public string Content { get { return content; } }
        public string Colour { get { return colour; } set { colour = value; } }
    }
}
