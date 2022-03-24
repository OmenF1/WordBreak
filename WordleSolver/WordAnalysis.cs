using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WordleSolver
{
    class WordAnalysis
    {
        private string wordList;

        public Dictionary<String, int> letters = new Dictionary<string, int>() {
                {"a", 0},{"b", 0},{"c", 0},{"d", 0},{"e", 0},{"f", 0},{"g", 0},{"h", 0},{"i", 0},{"j", 0},{"k", 0},{"l", 0},{"m", 0},{"n", 0},{"o", 0},
                {"p", 0},{"q", 0},{"r", 0},{"s", 0},{"t", 0},{"u", 0},{"v", 0},{"w", 0},{"x", 0},{"y", 0},{"z", 0}
            };

        public Dictionary<String, int> words = new Dictionary<string, int>();

        public WordAnalysis(string _wordList)
        {
            wordList = _wordList;
            GradeLetters();
            GradeWords();

        }

        //  Give each word a score based on the letters within the word.
        //  Word score is increased based on each distinct letter within the word.
        private void GradeWords()
        {
            List<string> keys = new List<string>(words.Keys);
            foreach (string key in keys)
            {
                foreach (char letter in key.Distinct().ToArray())
                {
                    words[key] += letters[letter.ToString()];
                }
            }
        }

        //  Check frequency of letters within given wordlist.
        //  Each letter score only increases once per word.
        private void GradeLetters()
        {
            foreach (string line in System.IO.File.ReadLines(wordList))
            {
                words.Add(line.ToLower(), 0);
                foreach (char letter in line.ToLower().Distinct().ToArray())
                {
                    letters[letter.ToString()] += 1;
                }
            }
        }
    }
}
