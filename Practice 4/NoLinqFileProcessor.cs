using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_4
{
    public class NoLinqFileProcessor : IFileProcessor
    {
        public List<string> GetLongestWords(string folderPath, int minWordLength)
        {
            List<string> longestWords = new List<string>();

            foreach (string file in Directory.EnumerateFiles(folderPath))
            {
                foreach (string line in File.ReadLines(file))
                {
                    foreach (string word in line.Split())
                    {
                        if (word.Length >= minWordLength)
                        {
                            longestWords.Add(word);
                        }
                    }
                }
            }

            longestWords.Sort((x, y) => y.Length.CompareTo(x.Length)); // сортировка по убыванию длины слов

            return longestWords.Take(10).ToList(); // вернуть 10 самых длинных слов
        }

        public List<string> GetTopNSpecificWords(string filePath, int n)
        {
            var fileContent = File.ReadAllText(filePath);
            var words = fileContent.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var specificWords = new Dictionary<string, int>();

            // Count the frequency of each word that appears more than once in the file
            foreach (var word in words)
            {
                if (specificWords.ContainsKey(word))
                {
                    specificWords[word]++;
                }
                else if (words.Count(w => w == word) > 1)
                {
                    specificWords.Add(word, 1);
                }
            }

            var specificWordsFrequency = specificWords.OrderByDescending(kv => kv.Value)
                                                     .Take(n)
                                                     .Select(kv => kv.Key)
                                                     .ToList();

            return specificWordsFrequency;
        }

        public string GetFileWithMostRepeatedWords(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);
            string fileName = "";
            int maxRepeatedWords = 0;

            foreach (string file in files)
            {
                string text = File.ReadAllText(file);
                string[] words = text.Split(' ');
                Dictionary<string, int> wordCounts = new Dictionary<string, int>();

                foreach (string word in words)
                {
                    if (wordCounts.ContainsKey(word))
                    {
                        wordCounts[word]++;
                    }
                    else
                    {
                        wordCounts[word] = 1;
                    }
                }

                int repeatedWords = wordCounts.Count(w => w.Value >= 2);

                if (repeatedWords > maxRepeatedWords)
                {
                    maxRepeatedWords = repeatedWords;
                    fileName = Path.GetFileName(file);
                }
            }

            return fileName;
        }

        public Dictionary<char, int> GetVowelsFrequency(string folderPath)
        {
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'а', 'я', 'у', 'ю', 'о', 'е', 'ё', 'э', 'и', 'ы' };
            var frequency = new Dictionary<char, int>();

            foreach (var file in Directory.EnumerateFiles(folderPath))
            {
                using (var reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        foreach (var character in line)
                        {
                            if (vowels.Contains(char.ToLower(character)))
                            {
                                if (!frequency.ContainsKey(character))
                                {
                                    frequency[character] = 1;
                                }
                                else
                                {
                                    frequency[character]++;
                                }
                            }
                        }
                    }
                }
            }

            return frequency;
        }
    }
}
