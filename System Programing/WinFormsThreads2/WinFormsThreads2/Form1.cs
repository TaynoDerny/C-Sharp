using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsThreads2
{
    public partial class Form1 : Form
    {
        Thread t1, t2, t3;
        volatile bool isRunning1 = false;

        long fileBytesCopied = 0;
        long fileTotalSize = 0;
        int fileActiveThreads = 0;
        ManualResetEvent filePauseEvent;
        volatile bool fileIsStopped = false;

        long dirBytesCopied = 0;
        long dirTotalSize = 0;
        int dirActiveThreads = 0;
        ManualResetEvent dirPauseEvent;
        volatile bool dirIsStopped = false;

        public Form1()
        {
            this.Text = "Системное программирование: Все задания";
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            SetupUI();
        }

        private void SetupUI()
        {
            TabControl tabControl = new TabControl { Dock = DockStyle.Fill };

            // ВКЛАДКА 1: ЗАДАНИЕ 1
            TabPage tab1 = new TabPage("Задание 1 (Приоритеты)");
            ComboBox cb1 = new ComboBox { Location = new Point(150, 20), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            ComboBox cb2 = new ComboBox { Location = new Point(150, 50), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            ComboBox cb3 = new ComboBox { Location = new Point(150, 80), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cb1.Items.AddRange(Enum.GetNames(typeof(ThreadPriority))); cb1.SelectedIndex = 2;
            cb2.Items.AddRange(Enum.GetNames(typeof(ThreadPriority))); cb2.SelectedIndex = 2;
            cb3.Items.AddRange(Enum.GetNames(typeof(ThreadPriority))); cb3.SelectedIndex = 2;
            Button btnStart1 = new Button { Text = "Старт", Location = new Point(300, 20), Width = 100 };
            Button btnStop1 = new Button { Text = "Стоп", Location = new Point(300, 70), Width = 100, Enabled = false };
            RichTextBox rtb1 = new RichTextBox { Location = new Point(20, 120), Width = 640, Height = 350, ReadOnly = true };

            btnStart1.Click += (s, e) => {
                rtb1.Clear(); isRunning1 = true;
                btnStart1.Enabled = false; btnStop1.Enabled = true;
                t1 = new Thread(() => GenNums(rtb1)); t1.Priority = (ThreadPriority)Enum.Parse(typeof(ThreadPriority), cb1.Text);
                t2 = new Thread(() => GenLetters(rtb1)); t2.Priority = (ThreadPriority)Enum.Parse(typeof(ThreadPriority), cb2.Text);
                t3 = new Thread(() => GenSymbols(rtb1)); t3.Priority = (ThreadPriority)Enum.Parse(typeof(ThreadPriority), cb3.Text);
                t1.Start(); t2.Start(); t3.Start();
            };
            btnStop1.Click += (s, e) => { isRunning1 = false; btnStart1.Enabled = true; btnStop1.Enabled = false; };
            tab1.Controls.AddRange(new Control[] { new Label { Text = "Цифры:", Location = new Point(20, 22) }, cb1,
                                                   new Label { Text = "Буквы:", Location = new Point(20, 52) }, cb2,
                                                   new Label { Text = "Символы:", Location = new Point(20, 82) }, cb3, btnStart1, btnStop1, rtb1 });

            // ВКЛАДКА 2: ЗАДАНИЯ 2-3 (Файл)
            TabPage tab2 = new TabPage("Зад. 2-3 (Файл)");
            TextBox txtSrcFile = new TextBox { Location = new Point(150, 20), Width = 500, Text = @"C:\test\file.bin" };
            TextBox txtDstFile = new TextBox { Location = new Point(150, 50), Width = 500, Text = @"C:\test\copy.bin" };
            TextBox txtThreadsFile = new TextBox { Location = new Point(150, 80), Width = 100, Text = "4" };
            Button btnStartFile = new Button { Text = "Старт", Location = new Point(20, 120), Width = 100 };
            Button btnPauseFile = new Button { Text = "Пауза", Location = new Point(130, 120), Width = 100, Enabled = false };
            Button btnStopFile = new Button { Text = "Стоп", Location = new Point(240, 120), Width = 100, Enabled = false };
            ProgressBar pbFile = new ProgressBar { Location = new Point(20, 170), Width = 630, Height = 30, Maximum = 100 };

            btnStartFile.Click += (s, e) => {
                if (!File.Exists(txtSrcFile.Text)) { MessageBox.Show("Файл не найден!"); return; }
                fileBytesCopied = 0; pbFile.Value = 0; fileIsStopped = false; filePauseEvent = new ManualResetEvent(true);
                btnStartFile.Enabled = false; btnPauseFile.Enabled = true; btnStopFile.Enabled = true;
                fileTotalSize = new FileInfo(txtSrcFile.Text).Length;
                using (File.Create(txtDstFile.Text)) { }
                int tCount = int.Parse(txtThreadsFile.Text);
                long chunk = fileTotalSize / tCount; fileActiveThreads = tCount;
                for (int i = 0; i < tCount; i++)
                {
                    long offset = i * chunk; long len = (i == tCount - 1) ? fileTotalSize - offset : chunk;
                    new Thread(() => CopyFileChunk(txtSrcFile.Text, txtDstFile.Text, offset, len, pbFile, btnStartFile, btnPauseFile, btnStopFile)).Start();
                }
            };
            btnPauseFile.Click += (s, e) => {
                if (btnPauseFile.Text == "Пауза") { filePauseEvent.Reset(); btnPauseFile.Text = "Продолжить"; }
                else { filePauseEvent.Set(); btnPauseFile.Text = "Пауза"; }
            };
            btnStopFile.Click += (s, e) => {
                fileIsStopped = true; filePauseEvent.Set();
                btnStartFile.Enabled = true; btnPauseFile.Enabled = false; btnStopFile.Enabled = false; btnPauseFile.Text = "Пауза";
            };
            tab2.Controls.AddRange(new Control[] { new Label { Text = "Исходный файл:", Location = new Point(20, 22) }, txtSrcFile,
                                                   new Label { Text = "Куда:", Location = new Point(20, 52) }, txtDstFile,
                                                   new Label { Text = "Потоков:", Location = new Point(20, 82) }, txtThreadsFile,
                                                   btnStartFile, btnPauseFile, btnStopFile, pbFile });

            // ВКЛАДКА 3: ЗАДАНИЯ 4-5 (Папка)
            TabPage tab3 = new TabPage("Зад. 4-5 (Папка)");
            TextBox txtSrcDir = new TextBox { Location = new Point(150, 20), Width = 500, Text = @"C:\test\source_dir" };
            TextBox txtDstDir = new TextBox { Location = new Point(150, 50), Width = 500, Text = @"C:\test\dest_dir" };
            TextBox txtThreadsDir = new TextBox { Location = new Point(150, 80), Width = 100, Text = "3" };
            Button btnStartDir = new Button { Text = "Старт", Location = new Point(20, 120), Width = 100 };
            Button btnPauseDir = new Button { Text = "Пауза", Location = new Point(130, 120), Width = 100, Enabled = false };
            Button btnStopDir = new Button { Text = "Стоп", Location = new Point(240, 120), Width = 100, Enabled = false };
            ProgressBar pbDir = new ProgressBar { Location = new Point(20, 170), Width = 630, Height = 30, Maximum = 100 };

            btnStartDir.Click += (s, e) => {
                if (!Directory.Exists(txtSrcDir.Text)) { MessageBox.Show("Папка не найдена!"); return; }
                dirBytesCopied = 0; pbDir.Value = 0; dirIsStopped = false; dirPauseEvent = new ManualResetEvent(true);
                btnStartDir.Enabled = false; btnPauseDir.Enabled = true; btnStopDir.Enabled = true;

                string[] allFiles = Directory.GetFiles(txtSrcDir.Text, "*.*", SearchOption.AllDirectories);
                dirTotalSize = 0;
                foreach (var f in allFiles) dirTotalSize += new FileInfo(f).Length;
                if (dirTotalSize == 0) { MessageBox.Show("Папка пуста!"); return; }

                Directory.CreateDirectory(txtDstDir.Text);
                int tCount = int.Parse(txtThreadsDir.Text);
                dirActiveThreads = tCount;
                int filesPerThread = allFiles.Length / tCount;

                for (int i = 0; i < tCount; i++)
                {
                    int start = i * filesPerThread;
                    int end = (i == tCount - 1) ? allFiles.Length : start + filesPerThread;
                    string[] myFiles = new string[end - start];
                    Array.Copy(allFiles, start, myFiles, 0, myFiles.Length);
                    new Thread(() => CopyDirFiles(myFiles, txtSrcDir.Text, txtDstDir.Text, pbDir, btnStartDir, btnPauseDir, btnStopDir)).Start();
                }
            };
            btnPauseDir.Click += (s, e) => {
                if (btnPauseDir.Text == "Пауза") { dirPauseEvent.Reset(); btnPauseDir.Text = "Продолжить"; }
                else { dirPauseEvent.Set(); btnPauseDir.Text = "Пауза"; }
            };
            btnStopDir.Click += (s, e) => {
                dirIsStopped = true; dirPauseEvent.Set();
                btnStartDir.Enabled = true; btnPauseDir.Enabled = false; btnStopDir.Enabled = false; btnPauseDir.Text = "Пауза";
            };
            tab3.Controls.AddRange(new Control[] { new Label { Text = "Исходная папка:", Location = new Point(20, 22) }, txtSrcDir,
                                                   new Label { Text = "Куда:", Location = new Point(20, 52) }, txtDstDir,
                                                   new Label { Text = "Потоков:", Location = new Point(20, 82) }, txtThreadsDir,
                                                   btnStartDir, btnPauseDir, btnStopDir, pbDir });

            // ВКЛАДКА 4: ЗАДАНИЕ 6 (Факториал)
            TabPage tab4 = new TabPage("Задание 6 (Факториал)");
            TextBox txtFactInput = new TextBox { Location = new Point(150, 20), Width = 100 };
            Button btnFact = new Button { Text = "Вычислить асинхронно", Location = new Point(20, 60), Width = 200 };
            Label lblFactResult = new Label { Text = "Результат: ", Location = new Point(20, 100), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };

            btnFact.Click += async (s, e) => {
                if (!int.TryParse(txtFactInput.Text, out int n) || n < 0) { MessageBox.Show("Введите положительное число!"); return; }
                btnFact.Enabled = false; lblFactResult.Text = "Вычисление...";
                double result = await Task.Run(() => CalculateFactorial(n));
                lblFactResult.Text = $"Результат: {result}";
                btnFact.Enabled = true;
            };
            tab4.Controls.AddRange(new Control[] { new Label { Text = "Введите число:", Location = new Point(20, 22) }, txtFactInput, btnFact, lblFactResult });

            // ВКЛАДКА 5: ЗАДАНИЕ 7 (Степень)
            TabPage tab5 = new TabPage("Задание 7 (Степень)");
            TextBox txtBase = new TextBox { Location = new Point(150, 20), Width = 100 };
            TextBox txtExp = new TextBox { Location = new Point(150, 50), Width = 100 };
            Button btnPow = new Button { Text = "Возвести в степень асинхронно", Location = new Point(20, 90), Width = 230 };
            Label lblPowResult = new Label { Text = "Результат: ", Location = new Point(20, 130), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };

            btnPow.Click += async (s, e) => {
                if (!double.TryParse(txtBase.Text, out double b) || !double.TryParse(txtExp.Text, out double exp)) { MessageBox.Show("Введите числа!"); return; }
                btnPow.Enabled = false; lblPowResult.Text = "Вычисление...";
                double result = await Task.Run(() => Math.Pow(b, exp));
                lblPowResult.Text = $"Результат: {result}";
                btnPow.Enabled = true;
            };
            tab5.Controls.AddRange(new Control[] { new Label { Text = "Основание:", Location = new Point(20, 22) }, txtBase,
                                                   new Label { Text = "Степень:", Location = new Point(20, 52) }, txtExp, btnPow, lblPowResult });

            // ВКЛАДКА 6: ЗАДАНИЕ 8 (Анализ текста)
            TabPage tab6 = new TabPage("Задание 8 (Текст)");
            TextBox txtText = new TextBox { Location = new Point(20, 20), Width = 640, Height = 200, Multiline = true, ScrollBars = ScrollBars.Vertical };
            Button btnAnalyze = new Button { Text = "Анализировать асинхронно", Location = new Point(20, 230), Width = 200 };
            Label lblAnalyzeResult = new Label { Text = "Гласных: 0, Согласных: 0, Символов: 0", Location = new Point(20, 270), AutoSize = true, Font = new Font("Arial", 12, FontStyle.Bold) };

            btnAnalyze.Click += async (s, e) => {
                string text = txtText.Text;
                btnAnalyze.Enabled = false; lblAnalyzeResult.Text = "Анализ...";
                var result = await Task.Run(() => AnalyzeText(text));
                lblAnalyzeResult.Text = $"Гласных: {result.vowels}, Согласных: {result.consonants}, Всего символов: {result.total}";
                btnAnalyze.Enabled = true;
            };
            tab6.Controls.AddRange(new Control[] { txtText, btnAnalyze, lblAnalyzeResult });

            tabControl.TabPages.AddRange(new TabPage[] { tab1, tab2, tab3, tab4, tab5, tab6 });
            this.Controls.Add(tabControl);
        }

        private void GenNums(RichTextBox rtb) { int i = 0; while (isRunning1) { UpdateUI(rtb, $"[Цифры] {i++ % 10}"); Thread.Sleep(100); } }
        private void GenLetters(RichTextBox rtb) { char c = 'A'; while (isRunning1) { UpdateUI(rtb, $"[Буквы] {c}"); c++; if (c > 'Z') c = 'A'; Thread.Sleep(100); } }
        private void GenSymbols(RichTextBox rtb) { char s = '!'; while (isRunning1) { UpdateUI(rtb, $"[Символы] {s}"); s++; if (s > '/') s = '!'; Thread.Sleep(100); } }

        private void UpdateUI(RichTextBox rtb, string txt)
        {
            if (rtb.InvokeRequired) rtb.Invoke(new Action(() => rtb.AppendText(txt + "\r\n")));
            else rtb.AppendText(txt + "\r\n");
        }

        private void CopyFileChunk(string src, string dst, long offset, long len, ProgressBar pb, Button btnS, Button btnP, Button btnSt)
        {
            try
            {
                using (FileStream fr = new FileStream(src, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (FileStream fw = new FileStream(dst, FileMode.Open, FileAccess.Write, FileShare.Write))
                {
                    fr.Seek(offset, SeekOrigin.Begin); fw.Seek(offset, SeekOrigin.Begin);
                    byte[] buffer = new byte[81920]; long left = len;
                    while (left > 0 && !fileIsStopped)
                    {
                        filePauseEvent.WaitOne(); if (fileIsStopped) break;
                        int r = fr.Read(buffer, 0, (int)Math.Min(buffer.Length, left));
                        if (r == 0) break;
                        fw.Write(buffer, 0, r); left -= r;
                        long current = Interlocked.Add(ref fileBytesCopied, r);
                        Invoke(new Action(() => pb.Value = (int)Math.Min(100, (current * 100) / fileTotalSize)));
                    }
                }
            }
            catch { }
            if (Interlocked.Decrement(ref fileActiveThreads) == 0 && !fileIsStopped)
            {
                Invoke(new Action(() => { MessageBox.Show("Файл скопирован!"); btnS.Enabled = true; btnP.Enabled = false; btnSt.Enabled = false; }));
            }
        }

        private void CopyDirFiles(string[] files, string srcRoot, string dstRoot, ProgressBar pb, Button btnS, Button btnP, Button btnSt)
        {
            try
            {
                foreach (var file in files)
                {
                    if (dirIsStopped) break;
                    string relPath = file.Substring(srcRoot.Length).TrimStart('\\');
                    string newPath = Path.Combine(dstRoot, relPath);
                    Directory.CreateDirectory(Path.GetDirectoryName(newPath));

                    using (FileStream fr = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (FileStream fw = new FileStream(newPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        byte[] buffer = new byte[81920]; int r;
                        while ((r = fr.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            dirPauseEvent.WaitOne(); if (dirIsStopped) break;
                            fw.Write(buffer, 0, r);
                            long current = Interlocked.Add(ref dirBytesCopied, r);
                            Invoke(new Action(() => pb.Value = (int)Math.Min(100, (current * 100) / dirTotalSize)));
                        }
                    }
                }
            }
            catch { }
            if (Interlocked.Decrement(ref dirActiveThreads) == 0 && !dirIsStopped)
            {
                Invoke(new Action(() => { MessageBox.Show("Папка скопирована!"); btnS.Enabled = true; btnP.Enabled = false; btnSt.Enabled = false; }));
            }
        }

        private double CalculateFactorial(int n)
        {
            double res = 1;
            for (int i = 2; i <= n; i++) res *= i;
            return res;
        }

        private (int vowels, int consonants, int total) AnalyzeText(string text)
        {
            int v = 0, c = 0;
            string vowels = "аеёиоуыэюяaeiouy";
            string consonants = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxz";
            foreach (char ch in text.ToLower())
            {
                if (vowels.Contains(ch)) v++;
                else if (consonants.Contains(ch)) c++;
            }
            return (v, c, text.Length);
        }
    }
}