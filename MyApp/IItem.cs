using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyApp
{
    internal abstract class IItem
    {
        protected long id;

        protected IItem()
        {
            set_id();
        }
        protected void set_id()
        {
            TimeSpan sec = DateTime.Now.Subtract(new DateTime(2024, 7, 7));
            //potential error of repeating ids if created to fast
            id = (long)sec.TotalSeconds;
        }
        public abstract XmlElement getXml();
        
    }
}
