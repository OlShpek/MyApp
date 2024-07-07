using System;

namespace MyApp
{
    class Program
    { 
        public static void Main(string[] args)
        {
            Task t = new Task("Skething Curves OppOx", new DateTime(2024, 7, 8), new TimeSpan(2, 0, 0));
            Console.WriteLine(t.Content);
            Console.WriteLine(t.getXml().OuterXml);
        }
    }

}

