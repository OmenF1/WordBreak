﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordleSolver
{
    class Game : IDisposable
    {
        private List<string> wordlist;
        private string word;
        WordAnalysis wordAnalysis;
        private bool disposedValue;

        public Game(string _wordlist)
        {
            wordAnalysis = new WordAnalysis(_wordlist);
            wordlist = new List<string>();
            foreach (string line in System.IO.File.ReadLines(_wordlist))
            {
                wordlist.Add(line.ToLower());
            }
        }

        // Very simple random, not going to bother with anything more crazy than that.
        public void PickWord()
        {
            Random rnd = new Random();
            word = wordlist[rnd.Next(wordlist.Count)];
        }

        //  Returns List of result for each letter with the following meaning:
        //  0 = Not in word.
        //  1 = In word, not right position
        //  2 = in word, in position
        //  null = Matched word
        public List<int> CheckWord(string guess)
        {
            if (guess == word)
            {
                return null; 
            }

            List<int> result = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                
                if (word.Contains(guess[i]))
                {
                    if (word[i] == guess[i])
                    {
                        result.Add(2);
                    }
                    else
                    {
                        result.Add(1);
                    }
                }
                else
                {
                    result.Add(0);
                }
            }
            wordAnalysis.words.Remove(guess);
            return result;
        }

        public List<String> PlaySelf()
        {
            bool solved = false;
            List<string> guesses = new List<string>();
            List<List<int>> results = new List<List<int>>();
            List<string> alphabet = new List<string>()
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q",
                "r", "s", "t", "u", "v", "w", "x", "y", "z"
            };

            while (!solved)
            {
                if (guesses.Count == 0)
                {
                    guesses.Add(wordAnalysis.words.OrderByDescending(x => x.Value).First().Key);
                }
                else
                {
                    guesses.Add(FindNextGuess(results[results.Count -1], guesses[guesses.Count - 1], alphabet));
                }
                
                results.Add(CheckWord(guesses[guesses.Count - 1]));

                //  Check the results from the previous guess
                if (results[results.Count -1] == null)
                {
                    return guesses;
                }
                else
                {
                    //  loop over results and remove letters that aren't in the word from the available letters in the alphabet
                    for (int i = 0; i < 5; i++)
                    {
                        if(results[results.Count -1][i] == 0)
                        {
                            if (alphabet.Contains(guesses[guesses.Count -1][i].ToString())) { alphabet.Remove(guesses[guesses.Count - 1][i].ToString()); }
                        }
                    }
                }

            }

            return null;
        }

        // Another quick slap together to test against wordle.
        //public void PlayWordle()
        //{
        //    List<string> guesses = new List<string>();
        //    List<List<int>> results = new List<List<int>>();
        //    List<string> alphabet = new List<string>()
        //    {
        //        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q",
        //        "r", "s", "t", "u", "v", "w", "x", "y", "z"
        //    };
        //    Console.WriteLine($"Battle of wordle begins!\nWe recommend starting with {wordAnalysis.words.OrderByDescending(x => x.Value).First().Key}");


        //    for (int i = 0; i <= 5; i++)
        //    {
        //        if (i == 0)
        //        {
        //            Console.WriteLine("Value used");
        //            guesses.Add(Console.ReadLine().ToLower());
        //        }
        //        else
        //        {
        //            guesses.Add(FindNextGuess(results[results.Count - 1], guesses[guesses.Count - 1], alphabet));
        //            Console.WriteLine(guesses[guesses.Count - 1]);
        //        }

        //        Console.WriteLine("results: ");
        //        results.Add(Console.ReadLine().Split(',').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList());

        //        //  loop over results and remove letters that aren't in the word from the available letters in the alphabet
        //        for (int n = 0; n < 5; n++)
        //        {
        //            if (results[results.Count - 1][n] == 0)
        //            {
        //                if (alphabet.Contains(guesses[guesses.Count - 1][n].ToString())) { alphabet.Remove(guesses[guesses.Count - 1][n].ToString()); }
        //            }
        //        }

        //    }

        //}

        private string FindNextGuess(List<int> lastResult, string lastGuess, List<string> alphabet)
        {
            List<string> tempWordList = wordAnalysis.words.Keys.ToList();
            foreach (string _word in tempWordList)
            {

                for (int i = 0; i < 5; i++)
                {

                    if ((!alphabet.Contains(_word[i].ToString())) || (_word[i] != lastGuess[i] && lastResult[i] == 2))
                    {
                        wordAnalysis.words.Remove(_word);
                        i = 6;
                        continue;
                    }
                    if (i == 0)
                    {
                        for (int n = 0; n < 5; n++)
                        {
                            if (lastResult[n] == 1)
                            {
                                if (!_word.Contains(lastGuess[n]))
                                {
                                    wordAnalysis.words.Remove(_word);
                                    i = 6;
                                }
                            }
                        }
                    }
                }
                
                
            }



            return wordAnalysis.words.OrderByDescending(x => x.Value).First().Key;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
