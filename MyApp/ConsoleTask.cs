using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class ConsoleTask
    {
        public static DateTime EnterDate()
        {
            Console.WriteLine("Enter the date; Enter day first, then month, then year");
            List<int> nums = EnterListOfNum(3);
            return new DateTime(nums[2], nums[1], nums[0], 23, 59, 59);
        }
        public static TimeSpan EnterTimeSpan()
        {
            Console.WriteLine("You can Enter the Time span; Enter hours first, then minutes");
            List<int> nums = EnterListOfNum(2);
            return new TimeSpan(nums[0], nums[1], 0);
        }

        public static List<int> SpecifyTimeSpan()
        {
            List<int> nums = new List<int>();
            Console.WriteLine("You can now specify the due time of the task");
            nums.Add(GetTaskId());
            Console.WriteLine("Enter hours, minutes and seconds next");
            nums.AddRange(EnterListOfNum(3));
            return nums;
        }
        public static Task CreateTask(string welcome)
        {
            Console.WriteLine(welcome);
            Console.Write("Enter the content: ");
            string cont = Console.ReadLine();
            Console.Write("Enter the date: ");
            DateTime dt = EnterDate();
            Console.Write("Enter the time span: ");
            TimeSpan ts = EnterTimeSpan();
            Task nt = new Task(cont, dt, ts, "task");
            return nt;
        }

        public static void TaskListDisplay(TaskList tl, ColouredItemList uL, ColouredItemList tags, string welcome, bool all)
        {
            Console.WriteLine("\n\n\n\n\n");
            Console.WriteLine(welcome);
            for (int i = 0; i < tl.Tasks.Count; i++)
            {
                if ((tl.GetElementAt(i).Completed && !all) || tl.IsDeleted(i))
                {
                    continue;
                }
                Console.WriteLine("Task id: " + i.ToString());
                TaskDisplay(tl.GetElementAt(i), tags, uL);
            }
        }
        private static void TaskDisplay(Task t, ColouredItemList tags, ColouredItemList urglevs)
        {
            if (t.Completed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (t.IsOverdue())
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(t.Content + " " + t.DueDate.ToString() + " " + t.ExpTime.ToString() + " ");
            ConsoleTags.DisplayULevel(t.UrgencyLevel, urglevs);
            ConsoleTags.DisplayTag(t.Tags, tags);
            Console.WriteLine();
            Console.ResetColor();
        }

        private static List<int> EnterListOfNum(int n)
        {
            List<int> nums = new List<int>();
            for (int i = 0; i < n; i++)
            {
                nums.Add(int.Parse(Console.ReadLine()));
            }
            return nums;
        }
        public static int GetTaskId()
        {
            Console.WriteLine("Enter task id");
            return int.Parse(Console.ReadLine());
        }
        public static int GetTaskId(string welcome)
        {
            Console.WriteLine(welcome);
            return GetTaskId();
        }
    }
}
