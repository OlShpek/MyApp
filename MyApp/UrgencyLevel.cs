using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    class UrgencyLevel : ColouredItem
    {
        int uLevel;
        public UrgencyLevel(string content, string colour, int ulevel, string tagName) : base(content, colour, tagName)
        {
            this.uLevel = ulevel;
        }

        public UrgencyLevel(XElement el, string tagName, string id) : base(el, tagName, id)
        {
            uLevel = int.Parse(el.Attribute("uLevel").Value);
        }

        public override XElement GetXml()
        {
            XElement el = base.GetXml();
            el.SetAttributeValue("uLevel", uLevel);
            return el;
        }

        public int ULevel { get { return uLevel; } set { uLevel = value; } }
    }
}
