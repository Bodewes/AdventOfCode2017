using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 02...");

            // Read Input
            var lines = Util.Tools.readFileToList("input.txt");

            // Setup
            var Solution = new Solution() { Data = lines };

            // Solve
            Solution.SolvePart1();

            Solution.SolvePart2();

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public List<string> Data { get; set; }

        public void SolvePart1()
        {

            var sum = 
                Data
                .ToListOfIntArray()
                .Select(x =>
                {
                    return x.Max() - x.Min();
                }).Sum();
            Console.WriteLine(sum);
        }

        internal void SolvePart2()
        {
            var sum =
               Data
               .ToListOfIntArray()
               .Select(x =>
               {
                   for(int i =0; i< x.Count(); i++)
                   {
                       for(int j =i+1; j < x.Count(); j++)
                       {
                           if (Math.Max(x[i], x[j]) % Math.Min(x[i], x[j]) == 0)
                               return Math.Max(x[i], x[j])/ Math.Min(x[i], x[j]);
                       }
                   }
                   return 0;
               })
               .Sum();
            Console.WriteLine(sum);
        }
    }
}
