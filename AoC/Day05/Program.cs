using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 05...");

            // Read Input
            var lines = Util.Tools.readFileToList("input.txt");
            var testLines = Util.Tools.readFileToList("test.txt");

            // Setup
            var Solution = new Solution() { Data = testLines };
            // Solve
            Solution.SolvePart1();
            Solution.SolvePart2();

            // Setup
            Solution.Data = lines;
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
            int[] instructions = Data.Select(x => int.Parse(x)).ToArray();

            int i = 0; // pointer;
            int step = 0;
            while (true)
            {
                try
                {
                    
                    var offset = instructions[i];
                    instructions[i]++;
                    i = i + offset;
                    step++;
                    //Console.WriteLine($"Step: {step} i:{i}");
                }
                catch (IndexOutOfRangeException ioore)
                {
                    Console.WriteLine(step);
                    break;
                }

            }
        }

        public void SolvePart2()
        {
            int[] instructions = Data.Select(x => int.Parse(x)).ToArray();

            int i = 0; // pointer;
            int step = 0;
            while (true)
            {
                try
                {

                    var offset = instructions[i];
                    if (offset >= 3)
                        instructions[i]--;
                    else
                        instructions[i]++;
                    i = i + offset;
                    step++;
                    //Console.WriteLine($"Step: {step} i:{i}");
                }
                catch (IndexOutOfRangeException ioore)
                {
                    Console.WriteLine(step);
                    //instructions.ToList().ForEach(Console.Write);
                    //Console.WriteLine();
                    break;
                }

            }
        }

    }
}
