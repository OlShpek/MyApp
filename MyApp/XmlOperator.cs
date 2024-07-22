using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace MyApp
{
    class XmlOperator
    {
        ItemList items;
        XDocument doc;
        string name;
        XElement root;
        public XmlOperator(string name, ItemList items)
        {
            this.name = "xml/" + name;
            this.items = items;
            if (!Directory.Exists("xml"))
            {
                Directory.CreateDirectory("xml");
            }
            if (!File.Exists(this.name))
            {
                FileStream str = File.Create(this.name);
                //StreamWriter sw = new StreamWriter(str);
                str.Close();
                string quote = "\"";
                List<string> lines = new List<string>() { "<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" + "\n" + "<rootElement></rootElement>" };
                File.WriteAllLines(this.name, lines);
               // sw.WriteLine("<?xml version=" + quote + "1.0" + quote + " encoding=" + quote + "utf-8" + quote + "?>" + "\n" + "<rootElement></rootElement>");

            }
            doc = XDocument.Load(this.name);
        }

        public void UpdateData()
        {
            if (doc.Root.HasElements)
            {
                doc.Root.ReplaceAll(items.GetXml());
            }
            else
            {
                doc.Root.Add(items.GetXml());
            }
            doc.Save(name);
                
        }
    }
}
