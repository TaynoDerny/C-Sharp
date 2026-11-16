using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SystemProgrammingHW
{
    public partial class Form1 : Form
    {
        ManualResetEvent eventTask1 = new ManualResetEvent(false);
        ManualResetEvent eventTask2 = new ManualResetEvent(false);
        ManualResetEvent eventTask3 = new ManualResetEvent(false);

        object lockFile = new object();
        object lockArray = new object();

        public Form1()
        {
            InitializeComponent();
            this.Text = "Модуль 1: Синхронизация";
            this.Size = new Size(600, 500);
            SetupUI();
        }

        private void SetupUI()
        {
            TabControl tabControl = new TabControl { Dock = DockStyle.Fill };

            // ВКЛАДКА 1
            TabPage tab1 = new TabPage("Задание 1 (События)");
            Button btn1 = new Button { Text = "Запустить", Location = new Point(20, 20), Width = 150 };
            RichTextBox rtb1 = new RichTextBox { Location = new Point(20, 60), Width = 540, Height = 350, ReadOnly = true };
            btn1.Click += (s, e) => RunTask1(rtb1);
            tab1.Controls.AddRange(new Control[] { btn1, rtb1 });

            // ВКЛАДКА 2
            TabPage tab2 = new TabPage("Задание 2 (Файл)");
            Button btn2 = new Button { Text = "Запустить", Location = new Point(20, 20), Width = 150 };
            RichTextBox rtb2 = new RichTextBox { Location = new Point(20, 60), Width = 540, Height = 350, ReadOnly = true };
            btn2.Click += (s, e) => RunTask2(rtb2);
            tab2.Controls.AddRange(new Control[] { btn2, rtb2 });

            // ВКЛАДКА 3
            TabPage tab3 = new TabPage("Задание 3 (Массив)");
            Button btn3 = new Button { Text = "Запустить", Location = new Point(20, 20), Width = 150 };
            RichTextBox rtb3 = new RichTextBox { Location = new Point(20, 60), Width = 540, Height = 350, ReadOnly = true };
            btn3.Click += (s, e) => RunTask3(rtb3);
            tab3.Controls.AddRange(new Control[] { btn3, rtb3 });

            tabControl.TabPages.AddRange(new TabPage[] { tab1, tab2, tab3 });
            this.Controls.Add(tabControl);
        }

        private void Log(RichTextBox rtb, string text)
        {
            if (rtb.InvokeRequired)
                rtb.Invoke(new Action(() => rtb.AppendText(text + "\r\n")));
            else
                rtb.AppendText(text + "\r\n");
        }

        private void RunTask1(RichTextBox rtb)
        {
            rtb.Clear();
            int[] numbers = new int[1000];
            eventTask1.Reset();

            Thread generator = new Thread(() =>
            {
                Random rnd = new Random();
                for (int i = 0; i < numbers.Length; i++) numbers[i] = rnd.Next(0, 5001);
                Log(rtb, "[Генератор] 1000 чисел сгенерировано!");
                eventTask1.Set();
            });

            Thread findMax = new Thread(() =>
            {
                eventTask1.WaitOne();
                Log(rtb, $"[Поток 2] Максимум: {numbers.Max()}");
            });

            Thread findMin = new Thread(() =>
            {
                eventTask1.WaitOne();
                Log(rtb, $"[Поток 3] Минимум: {numbers.Min()}");
            });

            Thread findAvg = new Thread(() =>
            {
                eventTask1.WaitOne();
                Log(rtb, $"[Поток 4] Среднее: {numbers.Average():F2}");
            });

            generator.Start(); findMax.Start(); findMin.Start(); findAvg.Start();
        }

        private void RunTask2(RichTextBox rtb)
        {
            rtb.Clear();
            string filePath = "test.txt";
            File.WriteAllText(filePath, "Привет! Как дела? Хорошо. Это тест! Конец.");
            Log(rtb, "Файл создан.");
            eventTask2.Reset();

            Thread t1 = new Thread(() =>
            {
                lock (lockFile)
                {
                    string text = File.ReadAllText(filePath);
                    int sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
                    Log(rtb, $"[Поток 1] Количество предложений: {sentences}");
                }
                eventTask2.Set();
            });

            Thread t2 = new Thread(() =>
            {
                eventTask2.WaitOne();
                lock (lockFile)
                {
                    string text = File.ReadAllText(filePath).Replace('!', '#');
                    File.WriteAllText(filePath, text);
                    Log(rtb, "[Поток 2] Файл изменен (! заменены на #).");
                }
            });

            t1.Start(); t2.Start();
        }

        private void RunTask3(RichTextBox rtb)
        {
            rtb.Clear();
            int[] arr = { 5, 2, 9, 1, 5, 6, -3, 0, 8, 4 };
            Log(rtb, "Исходный: " + string.Join(", ", arr));
            eventTask3.Reset();

            Thread t1 = new Thread(() =>
            {
                lock (lockArray)
                {
                    Array.Sort(arr);
                    Log(rtb, "[Поток 1] Отсортировано: " + string.Join(", ", arr));
                }
                eventTask3.Set();
            });

            Thread t2 = new Thread(() =>
            {
                eventTask3.WaitOne();
                lock (lockArray)
                {
                    int search = 5;
                    bool found = arr.Contains(search);
                    Log(rtb, found ? $"[Поток 2] Число {search} найдено!" : $"[Поток 2] Число {search} не найдено.");
                }
            });

            t1.Start(); t2.Start();
        }
    }
}