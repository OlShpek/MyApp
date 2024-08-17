using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    internal abstract class IItem
    {
        protected string id;
        protected string tagName;

        protected IItem(string tagName)
        {
            this.tagName = tagName;
            SetId();
        }

        protected IItem(string tagName, string id)
        {
            this.tagName = tagName;
            this.id = id;
        }

        protected IItem(XElement el)
        {
            this.tagName = el.Name.LocalName;
            this.id = el.Attribute("id").Value;
        }

        protected void SetId()
        {
            id = Guid.NewGuid().ToString();
        }
        public abstract XElement GetXml();
        public string Id { get { return id; } }
        public string TagName { get { return tagName; } }
    }
}
