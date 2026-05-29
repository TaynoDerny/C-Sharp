using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            // Исходный текст
            string text = "Вот дом, Который построил Джек. А это пшеница, Которая в темном чулане хранится в доме, Который построил Джек. А это веселая птица-синица, Которая часто ворует пшеницу, Которая в темном чулане хранится в доме, Который построил Джек.";

            // Удаляем знаки препинания (заменяем на пробелы)
            string cleanedText = Regex.Replace(text, @"[^\w\s]", " "); 
            
            // Разбиваем на слова, убираем пустые строки и приводим к нижнему регистру
            string[] words = cleanedText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            words = words.Select(w => w.ToLower()).ToArray();

            // Создаем словарь для подсчета слов
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            // Подсчитываем каждое слово
            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word)) // Если слово уже есть
                {
                    wordCount[word]++; // Увеличиваем счетчик
                }
                else // Если слово новое
                {
                    wordCount[word] = 1; // Добавляем со значением 1
                }
            }

            // Вывод заголовка таблицы
            Console.WriteLine("Статистика по тексту:");
            Console.WriteLine(new string('-', 35)); 
            Console.WriteLine($"{"№",-3} | {"Слово:",-15} | {"Кол-во:",5}");
            Console.WriteLine(new string('-', 35));

            int index = 1; // Номер строки
            int totalWords = 0; // Общее количество слов

            // Выводим каждую пару (слово - количество)
            foreach (var pair in wordCount)
            {
                Console.WriteLine($"{index,-3} | {pair.Key,-15} | {pair.Value,5}");
                totalWords += pair.Value; // Суммируем общее количество
                index++;
            }

            Console.WriteLine(new string('-', 35));

            // Итоговая статистика
            Console.WriteLine($"Всего слов: {totalWords}  из них уникальных: {wordCount.Count}");

            Console.ReadKey(); // Задержка консоли
        }
    }
}