using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Day00
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 01...");

            // Read Input
            var lines = Input.readFileToList("dummyInput.txt");

            // Setup
            var Solution = new Solution() { Data = lines };

            // Solve
            Solution.SolvePart1();

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution{

        public List<string> Data { get; set; }

        public void SolvePart1()
        {
            Data.ForEach(Console.WriteLine);
        }

    }

}
