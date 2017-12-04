using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 01...");

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
            Console.WriteLine(
                Data.Where(x => {
                    var words = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    return words.Count() == words.Distinct().Count(); })
                .Count()
            );
        }


        public void SolvePart2()
        {
            Console.WriteLine(
                Data.Where(x => {
                    var words = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(w => String.Concat(w.OrderBy(c=>c)) );
                    return words.Count() == words.Distinct().Count(); })
                .Count()
            );
        }



    }
}
