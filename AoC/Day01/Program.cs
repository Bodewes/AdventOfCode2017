using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 01...");

            // Read Input
            var part1 = Input.readFileToList("part1.txt").First();

            var test1 = "1122";
            var test2 = "1111";
            var test3 = "1234";
            var test4 = "91212129";

            // Setup en Solve
            new Solution() { Data = test1 }.SolvePart1(); ;
            new Solution() { Data = test2 }.SolvePart1(); ;
            new Solution() { Data = test3 }.SolvePart1(); ;
            new Solution() { Data = test4 }.SolvePart1(); ;

            new Solution() { Data = part1 }.SolvePart1(); ;


            new Solution() { Data = "1212" }.SolvePart2(); ;
            new Solution() { Data = "1221" }.SolvePart2(); ;
            new Solution() { Data = "123425" }.SolvePart2(); ;
            new Solution() { Data = "123123" }.SolvePart2(); ;
            new Solution() { Data = "12131415" }.SolvePart2(); ;


            new Solution() { Data = part1 }.SolvePart2(); ;
            // Solve
            //Solution.SolvePart1();

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public string Data { get; set; }

        public void SolvePart1()
        {
            Data = Data + Data[0];// Paste first at the end.
            int sum = 0;
            for(int i = 0; i < Data.Length-1; i++) {
                if (Data[i] == Data[i + 1])
                {
                    sum += Convert.ToInt32(Data.Substring(i,1));
                }
            }
            Console.WriteLine(sum);
        }

        public void SolvePart2()
        {
            
            int sum = 0;
            int fwd = Data.Length / 2;
            for (int i = 0; i < Data.Length ; i++)
            {
                if (Data[i] == Data[ (i + fwd)%Data.Length] )
                {
                    sum += Convert.ToInt32(Data.Substring(i, 1));
                }
            }
            Console.WriteLine(sum);
        }

    }
}
