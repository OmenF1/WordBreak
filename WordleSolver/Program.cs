using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordleSolver
{
    class Program
    {


        public static string wordlist = @"Lists\wordlelist.csv";

        static void Main(string[] args)
        {
            Game g = new Game(wordlist);
            g.PlayWordle();
            Console.ReadKey();
        }


    }
}
