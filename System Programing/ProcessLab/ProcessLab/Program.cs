using System;
using System.Diagnostics;
using System.Linq; // Нужно для сортировки (OrderByDescending)

namespace ProcessLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nЗадание 1\n");
            Process currentProcess = Process.GetCurrentProcess();

            double memoryMb = currentProcess.WorkingSet64 / (1024.0 * 1024.0);

            // Выводим информацию
            Console.WriteLine($"Имя процесса: {currentProcess.ProcessName}");
            Console.WriteLine($"Уникальный идентификатор (PID): {currentProcess.Id}");
            Console.WriteLine($"Время запуска: {currentProcess.StartTime}");
            Console.WriteLine($"Объем используемой физической памяти: {memoryMb:F2} МБ");
            Console.WriteLine($"Количество потоков: {currentProcess.Threads.Count}");

            var top5Processes = Process.GetProcesses().OrderByDescending(p => p.WorkingSet64).Take(5);
           
            foreach (Process p in top5Processes)
            {
                try
                {

                    double pMemoryMb = p.WorkingSet64 / (1024.0 * 1024.0);
                    Console.WriteLine($"Процесс: {p.ProcessName,-30} | Память: {pMemoryMb:F2} МБ");
                }
                catch (Exception)
                {

                }
            }
            Console.WriteLine("\nЗадание 2\n");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c ping 8.8.8.8 -n 4";

            //свойства
            startInfo.UseShellExecute = false;       // Отключаем графическую оболочку
            startInfo.RedirectStandardOutput = true; // Перенаправляем вывод в нашу программу
            startInfo.CreateNoWindow = true;         // Не открывать лишнее окно cmd

            Process pingProcess = new Process();
            pingProcess.StartInfo = startInfo;

            Console.WriteLine("Запуск ping...");
            pingProcess.Start();

            ///Считываем весь вывод(результат работы) дочернего процесса
            string output = pingProcess.StandardOutput.ReadToEnd();

            Console.WriteLine(" Начало вывода от cmd.exe ");
            Console.WriteLine(output);
            Console.WriteLine(" Конец вывода от cmd.exe ");

            // Ждем завершения дочернего процесса 
            pingProcess.WaitForExit();
            Console.WriteLine("Дочерний процесс успешно завершился.");

            Console.WriteLine("\nЗадание 3\n");

            Process longProcess = new Process();
            longProcess.StartInfo.FileName = "cmd.exe";

            longProcess.StartInfo.Arguments = "/c timeout /t 60";
            longProcess.StartInfo.CreateNoWindow = true; 
            longProcess.Start();

            Console.WriteLine("Процесс запущен, ожидаем 3 секунды...");

            bool finishedInTime = longProcess.WaitForExit(3000);


            if (finishedInTime == false) 
            {
                Console.WriteLine("ВНИМАНИЕ: Процесс не завершился за 3 секунды. Принудительно завершаем");
                longProcess.Kill(); 
            }
            else
            {
                Console.WriteLine("Процесс завершился сам за 3 секунды.");
            }

            longProcess.WaitForExit();
            Console.WriteLine("Ресурсы процесса успешно освобождены. Идем дальше.");

            Console.WriteLine("\nЗадание 4 - Б\n");

            Console.WriteLine("1. Для чего использовался AppDomain в старом .NET Framework?\r\nДля логической изоляции кода внутри одного процесса. Применялся для:\r\n\r\n    Плагинов: чтобы падение или вредоносный код плагина не ломал основное приложение.\r\n\r\n    ASP.NET: чтобы хостить несколько сайтов в одном процессе, но изолированно.\r\n\r\n    Выгрузки кода: позволял выгружать ненужные сборки из памяти без перезапуска всей программы.\r\n\r\n2. Чем AppDomain отличался от процесса?\r\n\r\n    Процесс — это объект ОС (свое адресное пространство памяти, тяжеловесный).\r\n\r\n    AppDomain — это «легковесный процесс» внутри CLR (логическая изоляция внутри одного процесса ОС). Он быстрее создается и потребляет меньше ресурсов, но не дает такой же аппаратной защиты памяти, как полноценный процесс.\r\n\r\n3. Какой механизм пришел на смену в современном .NET?\r\nAssemblyLoadContext (Контекст загрузки сборок). Он позволяет изолированно загружать разные версии сборок в один процесс и корректно выгружать их из памяти (сборный контекст), когда они больше не нужны." +
                "" +
                
                "\n");


        }
    }
}