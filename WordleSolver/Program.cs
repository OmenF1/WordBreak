using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordleSolver
{
    class Program
    {


        public static string wordlist = @"Lists\wordlist.csv";

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                using (Game g = new Game(wordlist))
                {
                    g.PickWord();
                    var result = g.PlaySelf();
                    Console.WriteLine($"Word: {result[result.Count - 1]} - Attempts: {result.Count}");
                }
            }
            Console.ReadKey();
        }


    }
}
