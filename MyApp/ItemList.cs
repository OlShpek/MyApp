using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyApp
{
    internal class ItemList : IItem
    {
        protected List<IItem> tasks;
        protected string name;
        protected List<bool> deleted;
        public ItemList(List<IItem> tasks, string name, string tagName) : base(tagName)
        {
            this.tasks = tasks;
            this.name = name;
            FillBoolList(tasks.Count);
        }

        protected ItemList(XElement el) : base(el)
        {
        }
        public void AddItem(IItem t)
        {
            tasks.Add(t);
            deleted.Add(false);
        }

        public void RemoveItem(string id) 
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                {
                    deleted[i] = true;
                }
            }
        }

        public void ChangeItem(IItem t)
        {
            for (int i = 0; i < tasks.Count; i++) 
            {
                if (tasks[i].Id == t.Id)
                {
                    tasks[i] = t;
                    return;
                }
            }
        }

        public IItem GetItemById(string id)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                {
                    return tasks[i];
                }
            }
            return null;
        }

        public override XElement GetXml()
        {
            XElement list = new XElement(tagName);
            list.SetAttributeValue("name", name);
            list.SetAttributeValue("id", id);
            for (int i = 0; i < tasks.Count; i++)
            {
                if (!deleted[i])
                {
                    list.Add(tasks[i].GetXml());
                }
            }
            return list;
        }

        protected void FillBoolList(int n)
        {
            deleted = new List<bool>();
            for (int i = 0; i < n; i++)
            {
                deleted.Add(false);
            }
        }

        public bool IsDeleted(int i)
        {
            return deleted[i];
        }
        public List<IItem> Tasks { get { return tasks; } }
        public string Name { get { return name; } set { name = value; } }
        public int Count { get { return tasks.Count; } }
    }
}
