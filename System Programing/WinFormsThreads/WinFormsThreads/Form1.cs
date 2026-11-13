using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsThreads
{
    public partial class Form1 : Form
    {
        TabControl tabControl;
        TabPage tab6, tab7;

        TextBox txtStart, txtEnd, txtThreads;
        Button btnTask6;
        RichTextBox rtbTask6;

        Button btnTask7;
        RichTextBox rtbTask7;

        static int maxVal = 0;
        static int minVal = 0;
        static double avgVal = 0;

        public Form1()
        {
            this.Text = "Домашнее задание: Многопоточность";
            this.Size = new Size(650, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            SetupUI();
        }

        private void SetupUI()
        {
            tabControl = new TabControl { Dock = DockStyle.Fill };
            tab6 = new TabPage("Задание 6 (Диапазон)");
            tab7 = new TabPage("Задание 7 (Массив)");

            // ЗАДАНИЕ 6
            Label lblStart = new Label { Text = "Начало:", Location = new Point(20, 25), AutoSize = true };
            txtStart = new TextBox { Location = new Point(100, 22), Width = 100 };

            Label lblEnd = new Label { Text = "Конец:", Location = new Point(20, 55), AutoSize = true };
            txtEnd = new TextBox { Location = new Point(100, 52), Width = 100 };

            Label lblThreads = new Label { Text = "Кол-во потоков:", Location = new Point(20, 85), AutoSize = true };
            txtThreads = new TextBox { Location = new Point(130, 82), Width = 70 };

            btnTask6 = new Button { Text = "Запустить потоки", Location = new Point(20, 120), Width = 180 };
            btnTask6.Click += BtnTask6_Click;

            rtbTask6 = new RichTextBox { Location = new Point(20, 160), Width = 580, Height = 300, ReadOnly = true };

            tab6.Controls.AddRange(new Control[] { lblStart, txtStart, lblEnd, txtEnd, lblThreads, txtThreads, btnTask6, rtbTask6 });

            // ЗАДАНИЕ 7
            btnTask7 = new Button { Text = "Сгенерировать 10000 чисел и найти Max/Min/Avg", Location = new Point(20, 20), Width = 400, Height = 30 };
            btnTask7.Click += BtnTask7_Click;

            rtbTask7 = new RichTextBox { Location = new Point(20, 60), Width = 580, Height = 400, ReadOnly = true };

            tab7.Controls.AddRange(new Control[] { btnTask7, rtbTask7 });

            tabControl.TabPages.Add(tab6);
            tabControl.TabPages.Add(tab7);
            this.Controls.Add(tabControl);
        }

        // ЗАДАНИЕ 6
        private void BtnTask6_Click(object sender, EventArgs e)
        {
            rtbTask6.Clear();
            try
            {
                int start = int.Parse(txtStart.Text);
                int end = int.Parse(txtEnd.Text);
                int threadCount = int.Parse(txtThreads.Text);

                if (start > end) { int temp = start; start = end; end = temp; }
                if (threadCount < 1) threadCount = 1;

                int totalNumbers = end - start + 1;
                if (threadCount > totalNumbers) threadCount = totalNumbers;

                int numbersPerThread = totalNumbers / threadCount;
                Thread[] threads = new Thread[threadCount];

                for (int i = 0; i < threadCount; i++)
                {
                    int currentStart = start + (i * numbersPerThread);
                    int currentEnd = (i == threadCount - 1) ? end : currentStart + numbersPerThread - 1;
                    int threadId = i + 1;

                    threads[i] = new Thread(() => PrintNumbers(threadId, currentStart, currentEnd));
                    threads[i].Start();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Введите корректные целые числа.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void PrintNumbers(int threadId, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                string text = $"[Поток {threadId}] число: {i}";
                if (Application.OpenForms[0] != null)
                {
                    Application.OpenForms[0].Invoke(new Action(() =>
                    {
                        ((Form1)Application.OpenForms[0]).rtbTask6.AppendText(text + "\r\n");
                    }));
                }
                Thread.Sleep(50);
            }
        }

        // ЗАДАНИЕ 7
        private void BtnTask7_Click(object sender, EventArgs e)
        {
            rtbTask7.Clear();
            rtbTask7.AppendText("Генерация массива из 10000 элементов...\n");
            rtbTask7.AppendText("Запуск 3-х потоков для поиска...\n\n");

            int[] numbers = new int[10000];
            Random rnd = new Random();
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rnd.Next(1, 10000);
            }

            btnTask7.Enabled = false;

            Task.Run(() =>
            {
                Thread maxThread = new Thread(() => FindMax(numbers));
                Thread minThread = new Thread(() => FindMin(numbers));
                Thread avgThread = new Thread(() => FindAverage(numbers));

                maxThread.Start();
                minThread.Start();
                avgThread.Start();

                maxThread.Join();
                minThread.Join();
                avgThread.Join();

                UpdateUI7("\n=== РЕЗУЛЬТАТЫ ===");
                UpdateUI7($"Максимум: {maxVal}");
                UpdateUI7($"Минимум:  {minVal}");
                UpdateUI7($"Среднее:  {avgVal:F2}");
            });
        }

        private void UpdateUI7(string text)
        {
            if (rtbTask7.InvokeRequired)
            {
                rtbTask7.Invoke(new Action(() => rtbTask7.AppendText(text + "\r\n")));
            }
            else
            {
                rtbTask7.AppendText(text + "\r\n");
            }
        }

        static void FindMax(int[] arr)
        {
            int max = arr[0];
            foreach (int num in arr) { if (num > max) max = num; }
            maxVal = max;
            if (Application.OpenForms[0] != null) Application.OpenForms[0].Invoke(new Action(() => ((Form1)Application.OpenForms[0]).UpdateUI7("[Поток] Максимум найден.")));
        }

        static void FindMin(int[] arr)
        {
            int min = arr[0];
            foreach (int num in arr) { if (num < min) min = num; }
            minVal = min;
            if (Application.OpenForms[0] != null) Application.OpenForms[0].Invoke(new Action(() => ((Form1)Application.OpenForms[0]).UpdateUI7("[Поток] Минимум найден.")));
        }

        static void FindAverage(int[] arr)
        {
            long sum = 0;
            foreach (int num in arr) { sum += num; }
            avgVal = (double)sum / arr.Length;
            if (Application.OpenForms[0] != null) Application.OpenForms[0].Invoke(new Action(() => ((Form1)Application.OpenForms[0]).UpdateUI7("[Поток] Среднее найдено.")));
        }
    }
}