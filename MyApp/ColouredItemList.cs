using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    class ColouredItemList : ItemList
    {
        public ColouredItemList(List<IItem> items, string name, string tagName) : base(items, name, tagName)
        { }

        public ColouredItemList(XElement el) : base(el)
        {
            List<XElement> els = el.Elements().ToList<XElement>();
            tasks = new List<IItem>();
            for (int i = 0; i < els.Count; i++)
            {
                if (els[i].Attribute("uLevel") != null)
                {
                    tasks.Add(new UrgencyLevel(els[i]));
                }
                else 
                {
                    tasks.Add(new ColouredItem(els[i]));
                }
            }
            FillBoolList(tasks.Count);
            name = el.Name.LocalName;
        }
        
        public bool Exists(string name)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (GetElementAt(i).Content == name)
                {
                    return true;
                }
            }
            return false;
        }

        public ColouredItem GetElementByName(string name)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (GetElementAt(i).Content == name)
                {
                    return GetElementAt(i);
                }
            }
            return null;
        }
        public void SpecifyColour(string name, string colour, string tag)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (GetElementAt(i).Content == name)
                {
                    GetElementAt(i).Colour = colour;
                    return;
                }
            }
            tasks.Add(new ColouredItem(name, colour, tag));
            deleted.Add(false);
        }
        public ColouredItem GetElementAt(int i)
        {
            return (ColouredItem)tasks[i];
        }
    }
}
