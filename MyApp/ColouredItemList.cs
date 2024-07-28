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

        public ColouredItemList(XElement el, string tagName, string id) : base(tagName, id)
        {
            List<XElement> els = el.Elements().ToList<XElement>();
            tasks = new List<IItem>();
            for (int i = 0; i < els.Count; i++)
            {
                tasks.Add(new ColouredItem(els[i], els[i].Name.LocalName, els[i].Attribute("id").Value));
            }
            FillBoolList(tasks.Count);
            name = el.Name.LocalName;
        }

        public string GetColour(string name)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (GetElementAt(i).Content == name)
                {
                    return GetElementAt(i).Colour;
                }
            }
            return "None";
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
