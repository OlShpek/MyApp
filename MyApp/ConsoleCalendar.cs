using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class ConsoleCalendar
    {
        public static void DisplayCalendarWeek(Calendar c, DateTime start)
        {
           
        }

        public static void DisplayCalendarDay(Date d)
        {
            int boundary = 21;
            WriteUpperLine(boundary + 3);
            //header
            Display(d.RealDate.DayOfWeek.ToString(), boundary, "none");
            Display(d.RealDate.ToString("G"), boundary, "none");
            Display("", boundary, "none");
            WriteUpperLine(boundary + 3);
            //tasks
            Display("Tasks are: ", boundary, "none");
            DisplayTasks(d.Subtasks, boundary);
            WriteUpperLine(boundary + 3);
            //events
            Display("Events are: ", boundary, "none");
            DisplayEvents(d, boundary);
            WriteUpperLine(boundary + 3);
        }

        private static void DisplayTasks(TaskList tl, int boundary)
        {
            for (int i = 0; i < tl.Count; i++)
            {
                if (!tl.IsDeleted(i))
                {
                    Display("Task id: " + i.ToString(), boundary, "none");
                    if (tl.GetElementAt(i).IsOverdue())
                    {
                        Display(tl.GetElementAt(i).Content, boundary, "Red");
                    }
                    else if (tl.GetElementAt(i).Completed)
                    {
                        Display(tl.GetElementAt(i).Content, boundary, "Green");
                    }
                    else
                    {
                        Display(tl.GetElementAt(i).Content, boundary, "none");
                    }
                }    
            }
        }

        private static void DisplayEvents(Date d, int boundary)
        {
            for (int i = 0; i < d.Count; i++)
            {
                Display(d.GetElementAt(i).Content, boundary, "none");
            }
        }
        private static List<string> FillToBound(string s, int boundary)
        {
            List<string> strs = BreakDown(s, boundary);

            for (int i = 0; i < strs.Count; i++)
            {
                for (int j = strs[i].Length; j < boundary; j++)
                {
                    strs[i].Append<char>(' ');
                }
            }

            return strs;
        }

        private static void Display(string s, int boundary, string colour)
        {
            List<string> strs = FillToBound(s, boundary);
            for (int i = 0; i < strs.Count; i++)
            {
                Console.Write("| ");
                if (colour != "none")
                {
                    ConsoleColor cl = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colour);
                    Console.ForegroundColor = cl;
                }
                Console.Write(strs[i].PadRight(boundary));
                Console.ResetColor();
                Console.Write(" |");
                Console.WriteLine();
            }
        }
        private static List<string> BreakDown(string s, int boundary)
        {
            if (s.Length < boundary)
            {
                return new List<string>() { s };
            }

            List<string> strs = new List<string>();
            for (int i = 0; i < s.Length; i+=boundary)
            {
                if (i + boundary < s.Length)
                {
                    strs.Add(s.Substring(i, boundary));
                }
                else
                {
                    strs.Add(s.Substring(i));
                }
            }
            return strs;
        }
        private static void WriteUpperLine(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}
