using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_4
{
    public interface IFileProcessor
    {
        /// <summary>
        /// Возвращает самые длинные слова (длиной не менее minWordLength символов) во всех файлах в папке folderPath.
        /// </summary>
        public List<string> GetLongestWords(string folderPath, int minWordLength);

        /// <summary>
        /// Возвращает n наиболее часто встречающихся специфических слов, которые присутствуют только в каком-то одном файле.
        /// </summary>
        public List<string> GetTopNSpecificWords(string folderPath, int n);

        /// <summary>
        /// Возвращает имя файла с наибольшим количеством повторяющихся слов (слова, которые присутствуют в файле не менее двух раз).
        /// </summary>
        public string GetFileWithMostRepeatedWords(string folderPath);

        /// <summary>
        /// Возвращает распределение гласных букв по частоте во всех файлах в папке folderPath.
        /// </summary>
        public Dictionary<char, int> GetVowelsFrequency(string folderPath);
    }
}
