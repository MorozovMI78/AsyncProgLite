using System;

namespace AsyncProg
{
    class Program
    {
        //string? com;
        //bool parseres;


        static int taskcounter = 0; //Почему static?? -- Потому что иначе не сможем обратиться из static task
        struct taskPars {internal int taskNum; internal DateTime taskStart; internal int taskTerm; };

        //private static void Main()
        static void Main()
        //Было static async Task Main()  
        //static - обязательно для Main
        {
            string? com;
            bool parseres;
            List<taskPars> tasks = new List<taskPars>();
            
            Console.WriteLine("?  ");
            do
            {
                com = Console.ReadLine();
                parseres = int.TryParse(com, out var number);
                if (parseres)
                {
                    Task proceed = ProceedAsync(number);
                    //for (int i = 1; proceed.Status == TaskStatus.WaitingForActivation; i++) 
                    //{
                    //    if (i % 100000000 == 0)
                    //        Console.WriteLine($"Task proceed, i = {i}, {proceed.Status}");
                    //}
                }
                else if (com=="?")
                {
                   PrintTasks();
                }
                else
                {
                    Console.WriteLine(com);
                }
            }
            while (com != "");

            async Task ProceedAsync(int num) //Первоначально было вне Main и требовало static
            {
                Task delay = Task.Delay(num * 1000);
                taskPars taskPars = new taskPars { taskNum = ++taskcounter, taskStart = DateTime.Now, taskTerm = num };
                tasks.Add(taskPars);
                Console.WriteLine($"Task {taskPars.taskNum}, {taskPars.taskStart:hh:mm:ss.fff}, {num} secs, status - {delay.Status}");
                //await Task.Delay(num * 1000); //без await не ждет. Почему??? -- Потому что Task.Delay - запуск задачи с complete через t мс,
                //а await - ожидание статуса completed
                //Thread.Sleep(num * 1000); //если так, то не работает асинхронно. Почему???

                ////Этот цикл лишает программу асинхронности.
                ////Он не даёт вернуться в метод, который вызвал ProceedAsync
                //for (int i = 1; delay.Status == TaskStatus.WaitingForActivation; i++) 
                //{
                //    if (i % 100000000 == 0)
                //        Console.WriteLine($"Task {taskPars.taskNum}, {taskPars.taskStart:hh:mm:ss.fff}, i = {i}, {delay.Status}");
                //}

                await delay; //необязательно, и так работает
                //Task.Delay(num * 1000).Wait(); //если так, то не работает асинхронно. Почему???
                Console.WriteLine($"End of task {taskPars.taskNum}, {DateTime.Now:hh:mm:ss.fff}");
                tasks.Remove(taskPars);
            }

            void PrintTasks()
            {
                DateTime now = DateTime.Now;
                Console.WriteLine($"* Active tasks ({now:hh:mm:ss.fff}):");
                foreach (var taskPars in tasks)
                {
                    Console.WriteLine($"* Task {taskPars.taskNum}, {taskPars.taskTerm} secs, start at {taskPars.taskStart:hh:mm:ss.fff}, elapsed {now.Subtract(taskPars.taskStart).ToString("ss\\.fff")}");
                }
                Console.WriteLine("");
            }


        }



    }
}
