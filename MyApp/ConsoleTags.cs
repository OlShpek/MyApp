using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class ConsoleTags
    {
        public static void DisplayTag(List<string> tags, ColouredItemList globalTags)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                ColouredItem tag = (ColouredItem)globalTags.GetItemById(tags[i]);
                ConsoleColor cl = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), tag.Colour);
                Console.ForegroundColor = cl;
                Console.Write(tag.Content + " ");
            }
        }

        public static List<string> EnterStr(string welcome, int n)
        {
            Console.WriteLine(welcome);
            List<string> strs = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string s = Console.ReadLine();
                strs.Add(s);
            }
            return strs;
        }
        public static void DisplayULevel(string uLevel, ColouredItemList globalUrgLev)
        {
            if (uLevel == "0")
            {
                return;
            }
            DisplayTag(new List<string>() { uLevel }, globalUrgLev);
        }
    }
}
