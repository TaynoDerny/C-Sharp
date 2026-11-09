using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinApiLab
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SendMessageW")]
        public static extern IntPtr SendMessageText(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

        const uint MB_OK = 0;
        const uint MB_YESNO = 4;
        const uint MB_ICONQUESTION = 32;
        const uint MB_ICONINFORMATION = 64;
        const int IDYES = 6;
        const uint WM_CLOSE = 0x0010;
        const uint WM_SETTEXT = 0x000C;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Задание 1: Вывод Hello, World!");
                Console.WriteLine("2. Задание 2: Игра 'Угадай число'");
                Console.WriteLine("3. Задание 3: Закрыть Блокнот");
                Console.WriteLine("4. Задание 4: Обновить время в заголовке Блокнота");
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

        static void Task1()
        {
            Console.Clear();
            Console.WriteLine("Запуск Задания 1...");
            MessageBox(IntPtr.Zero, "Hello, World!", "Задание 1", MB_OK);
            Console.WriteLine("Окно закрыто. Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        static void Task2()
        {
            Console.Clear();
            Console.WriteLine("Запуск Задания 2...");
            MessageBox(IntPtr.Zero, "Загадай число от 0 до 100. Я попробую его угадать!", "Игра", MB_OK | MB_ICONINFORMATION);

            bool playAgain = true;
            while (playAgain)
            {
                int low = 0;
                int high = 100;
                int attempts = 0;

                while (low <= high)
                {
                    int guess = (low + high) / 2;
                    attempts++;

                    int result = MessageBox(IntPtr.Zero, $"Твое число равно {guess}?", "Угадываю...", MB_YESNO | MB_ICONQUESTION);

                    if (result == IDYES)
                    {
                        MessageBox(IntPtr.Zero, $"Ура! Я угадал твое число {guess} за {attempts} попыток!", "Победа!", MB_OK | MB_ICONINFORMATION);
                        break;
                    }

                    result = MessageBox(IntPtr.Zero, $"Твое число больше {guess}?", "Угадываю...", MB_YESNO | MB_ICONQUESTION);

                    if (result == IDYES) low = guess + 1;
                    else high = guess - 1;
                }

                int replayResult = MessageBox(IntPtr.Zero, "Хочешь сыграть еще раз?", "Повтор", MB_YESNO | MB_ICONQUESTION);
                if (replayResult != IDYES) playAgain = false;
            }

            Console.WriteLine("Игра окончена. Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        static void Task3()
        {
            Console.Clear();
            Console.WriteLine("Запуск Задания 3...");
            Console.WriteLine("Ищем окно Блокнота...");

            IntPtr notepadHandle = FindWindow("Notepad", null);

            if (notepadHandle == IntPtr.Zero)
            {
                Console.WriteLine("Ошибка: Блокнот не найден! Откройте Блокнот и попробуйте снова.");
            }
            else
            {
                Console.WriteLine("Блокнот найден! Отправляем команду на закрытие...");
                SendMessage(notepadHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Console.WriteLine("Сообщение WM_CLOSE отправлено.");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        static void Task4()
        {
            Console.Clear();
            Console.WriteLine("Запуск Задания 4...");
            Console.WriteLine("Убедитесь, что Блокнот запущен.");
            Console.WriteLine("Для остановки обновления времени нажмите любую клавишу...\n");

            bool isRunning = true;

            while (isRunning)
            {
                IntPtr notepadHandle = FindWindow("Notepad", null);

                if (notepadHandle == IntPtr.Zero)
                {
                    Console.WriteLine("Ошибка: Блокнот не найден! Цикл остановлен.");
                    break;
                }

                string currentTime = DateTime.Now.ToString("HH:mm:ss");

                SendMessageText(notepadHandle, WM_SETTEXT, IntPtr.Zero, currentTime);
                Console.WriteLine($"Время в заголовке обновлено на: {currentTime}");

                Thread.Sleep(1000);

                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    isRunning = false;
                }
            }

            Console.WriteLine("\nОбновление остановлено. Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}