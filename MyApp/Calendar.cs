using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    class Calendar : ItemList
    {
        public Calendar(List<IItem> dates, string name, string tagName) : base(dates, name, tagName)
        {     }

        public Calendar(XElement el) : base(el)
        {
            List<XElement> els = el.Elements("date").ToList<XElement>();
            tasks = new List<IItem>();
            for (int i = 0; i < els.Count; i++)
            {
                tasks.Add(new Date(els[i]));
            }
            FillBoolList(tasks.Count);
        }
        public Date GetElementAt(int i)
        {
            return (Date)tasks[i];
        }
    }
}
