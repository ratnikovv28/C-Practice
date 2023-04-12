using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_4
{
    public class LinqFileProcessor : IFileProcessor
    {
        public List<string> GetLongestWords(string folderPath, int minWordLength)
        {
            var query = from file in Directory.EnumerateFiles(folderPath)
                        from line in File.ReadLines(file)
                        from word in line.Split()
                        where word.Length >= minWordLength
                        orderby word.Length descending
                        select word;

            return query.Take(10).ToList(); // вернуть 10 самых длинных слов
        }

        public List<string> GetTopNSpecificWords(string filePath, int n)
        {
            var fileContent = File.ReadAllText(filePath);
            var specificWords = fileContent.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                           .GroupBy(w => w)
                                           .Where(g => g.Count() >= 2)
                                           .Select(g => g.Key);

            var specificWordsFrequency = specificWords.GroupBy(w => w)
                                                      .OrderByDescending(g => g.Count())
                                                      .Take(n)
                                                      .Select(g => g.Key)
                                                      .ToList();

            return specificWordsFrequency;
        }

        public string GetFileWithMostRepeatedWords(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);

            string fileName = files
                .Select(file => new
                {
                    FileName = Path.GetFileName(file),
                    Words = File.ReadAllText(file).Split(' ')
                })
                .Select(fileData => new
                {
                    fileData.FileName,
                    RepeatedWords = fileData.Words
                        .GroupBy(w => w)
                        .Where(group => group.Count() >= 2)
                        .Count()
                })
                .OrderByDescending(fileData => fileData.RepeatedWords)
                .First().FileName;

            return fileName;
        }

        public Dictionary<char, int> GetVowelsFrequency(string folderPath)
        {
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'а', 'я', 'у', 'ю', 'о', 'е', 'ё', 'э', 'и', 'ы' };
            var frequency = new Dictionary<char, int>();

            foreach (var file in Directory.EnumerateFiles(folderPath))
            {
                var fileVowels = File.ReadLines(file)
                                     .SelectMany(line => line)
                                     .Where(character => vowels.Contains(char.ToLower(character)));

                foreach (var vowel in fileVowels)
                {
                    if (!frequency.ContainsKey(vowel))
                    {
                        frequency[vowel] = 1;
                    }
                    else
                    {
                        frequency[vowel]++;
                    }
                }
            }

            return frequency;
        }
    }
}
