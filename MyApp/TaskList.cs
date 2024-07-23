using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace MyApp
{
    class TaskList : ItemList
    {
        public TaskList(List<IItem> tasks, string name, string tagName) : base(tasks, name, tagName)
        { }
        public TaskList(XElement el, string id, string tagName) : base(tagName, id)
        {
            List<XElement> els = el.Elements().ToList<XElement>();
            tasks = new List<IItem>();
            for (int i = 0; i < els.Count; i++)
            {
                tasks.Add(new Task(els[i], els[i].Name.LocalName, els[i].Attribute("id").Value));
            }
            deleted = new List<bool>();
            for (int i = 0; i < tasks.Count; i++)
            {
                deleted.Add(false);
            }
            name = el.Name.LocalName;
        }

        public Task GetElementAt(int i)
        {
            return (Task)tasks[i];
        }
    }
}
