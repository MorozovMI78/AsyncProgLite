using System;

namespace AsyncProg
{
    class Program
    {

        static int taskcounter = 0;
        static async Task Main()  
        {
            string? com;
            bool parseres;
            
            Console.WriteLine("?  ");
            do
            {
                com = Console.ReadLine();
                parseres = int.TryParse(com, out var number);
                if (parseres) ProceedAsync(number);
                
             }
            while (com != "");

            async Task ProceedAsync(int num)
            {
                Task delay = Task.Delay(num * 1000);
                int taskNum = ++taskcounter;
                DateTime taskStart = DateTime.Now;
                Console.WriteLine($"Task {taskNum}, {taskStart:hh:mm:ss.fff}, {num} secs");
                await delay; 
                Console.WriteLine($"End of task {taskNum}, {DateTime.Now:hh:mm:ss.fff}");
            }
        }
    }
}
