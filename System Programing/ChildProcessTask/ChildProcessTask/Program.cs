using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ChildProcessTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "--task3") { ChildTask3(args); return; }
                if (args[0] == "--task4") { ChildTask4(args); return; }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Задание 1");
                Console.WriteLine("2. Задание 2");
                Console.WriteLine("3. Задание 3");
                Console.WriteLine("4. Задание 4");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите задание: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Task1(); break;
                    case "2": Task2(); break;
                    case "3": Task3(); break;
                    case "4": Task4(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ЗАДАНИЕ 1
        static void Task1()
        {
            Console.Clear();
            Console.WriteLine(" ЗАДАНИЕ 1 ");
            Console.WriteLine("Запускаем Блокнот (notepad.exe)...");
            Console.WriteLine("Закройте Блокнот вручную, чтобы увидеть код завершения.");

            ProcessStartInfo psi = new ProcessStartInfo("notepad.exe");
            psi.UseShellExecute = true;

            Process childProcess = Process.Start(psi);

            if (childProcess != null)
            {
                childProcess.WaitForExit();
                Console.WriteLine($"\nБлокнот закрыт. Код завершения: {childProcess.ExitCode}");
                childProcess.Dispose();
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // ЗАДАНИЕ 2
        static void Task2()
        {
            Console.Clear();
            Console.WriteLine(" ЗАДАНИЕ 2 ");

            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c timeout /t 15");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;

            Console.WriteLine("Запущен дочерний процесс (будет работать 15 секунд).");
            Process childProcess = Process.Start(psi);

            Console.WriteLine("\n1. Дождаться завершения.");
            Console.WriteLine("2. Убить процесс прямо сейчас.");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            if (childProcess != null)
            {
                if (choice == "1")
                {
                    Console.WriteLine("\nОжидаем завершения...");
                    childProcess.WaitForExit();
                    Console.WriteLine($"Процесс завершился сам. Код: {childProcess.ExitCode}");
                }
                else if (choice == "2")
                {
                    if (!childProcess.HasExited)
                    {
                        childProcess.Kill();
                        childProcess.WaitForExit();
                        Console.WriteLine("\nПроцесс был принудительно убит (Kill).");
                    }
                    else
                    {
                        Console.WriteLine("\nПроцесс уже успел закрыться сам.");
                    }
                }
                childProcess.Dispose();
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // ЗАДАНИЕ 3 (Родитель)
        static void Task3()
        {
            Console.Clear();
            Console.WriteLine(" ЗАДАНИЕ 3: Калькулятор ");

            Console.Write("Введите первое число: ");
            string num1 = Console.ReadLine();
            Console.Write("Введите второе число: ");
            string num2 = Console.ReadLine();
            Console.Write("Введите операцию (+, -, *, /): ");
            string op = Console.ReadLine();

            ProcessStartInfo psi = new ProcessStartInfo(Environment.ProcessPath);
            psi.Arguments = $"--task3 {num1} {num2} {op}";
            psi.UseShellExecute = false;

            Console.WriteLine("\nЗапуск дочернего процесса для вычисления...");
            Process.Start(psi)?.WaitForExit();

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // ЗАДАНИЕ 3 (Дочерний)
        static void ChildTask3(string[] args)
        {
            Console.WriteLine(" ДОЧЕРНИЙ ПРОЦЕСС (Задание 3) ");
            Console.WriteLine($"Полученные аргументы: {args[1]}, {args[2]}, {args[3]}");

            if (double.TryParse(args[1], out double a) && double.TryParse(args[2], out double b))
            {
                string op = args[3];
                double result = 0;
                switch (op)
                {
                    case "+": result = a + b; break;
                    case "-": result = a - b; break;
                    case "*": result = a * b; break;
                    case "/": result = b != 0 ? a / b : 0; break;
                }
                Console.WriteLine($"Результат: {a} {op} {b} = {result}");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный формат чисел.");
            }
        }

        // ЗАДАНИЕ 4 (Родитель)
        static void Task4()
        {
            Console.Clear();
            Console.WriteLine(" ЗАДАНИЕ 4: Поиск слова в файле ");

            string testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");
            File.WriteAllText(testFilePath, "I bought a bicycle yesterday. My bicycle is red. I love my bicycle!");
            Console.WriteLine($"Создан тестовый файл: {testFilePath}");
            Console.WriteLine("В нем есть слово 'bicycle' (встречается 3 раза).");

            Console.WriteLine("\nВведите путь к файлу (или нажмите Enter для тестового файла):");
            string filePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(filePath)) filePath = testFilePath;

            Console.Write("Введите слово для поиска (например, bicycle): ");
            string word = Console.ReadLine();

            ProcessStartInfo psi = new ProcessStartInfo(Environment.ProcessPath);
            psi.Arguments = $"--task4 \"{filePath}\" \"{word}\"";
            psi.UseShellExecute = false;

            Console.WriteLine("\nЗапуск дочернего процесса для подсчета...");
            Process.Start(psi)?.WaitForExit();

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // ЗАДАНИЕ 4 (Дочерний)
        static void ChildTask4(string[] args)
        {
            Console.WriteLine(" ДОЧЕРНИЙ ПРОЦЕСС (Задание 4) ");
            string filePath = args[1];
            string word = args[2];

            Console.WriteLine($"Поиск слова \"{word}\" в файле: {filePath}");

            try
            {
                if (File.Exists(filePath))
                {
                    string text = File.ReadAllText(filePath);
                    MatchCollection matches = Regex.Matches(text, $@"\b{Regex.Escape(word)}\b", RegexOptions.IgnoreCase);
                    Console.WriteLine($"Количество вхождений: {matches.Count}");
                }
                else
                {
                    Console.WriteLine("Ошибка: файл не найден!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
        }
    }
}