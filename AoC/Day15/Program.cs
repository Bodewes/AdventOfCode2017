using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {

            var s = new SolutionPart1();

            //s.Solve(65, 8921);
            s.Solve(699, 124);


            var s2 = new SolutionPart2();

            //s2.Solve(65, 8921);
            s2.Solve(699, 124);

            Console.ReadLine();
        }
    }

    class SolutionPart1
    {

        public void Solve(int seedA, int seedB)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int A = seedA;
            int B = seedB;
            
            int score =0;

            for (int i =0; i< 40_000_000; i++)
            {
                A = NextValueA(A);
                B = NextValueB(B);
                //Console.WriteLine($"{A}\t{B}");
                if ( (A& 0xffff) == (B& 0xffff))
                {
                    //Console.WriteLine("Match");
                    score++;

                }
            }
            sw.Stop();
            Console.WriteLine($"Score Part1: {score}");
            Console.WriteLine($"time: {sw.Elapsed}");
        }

        private int NextValueA(int prev)
        {
            return (int) (( (Int64)prev * (Int64)16807) % 2147483647);
        }

        private int NextValueB(int prev)
        {
            return (int)(((Int64)prev * (Int64)48271) % 2147483647);
        }
    }

    class SolutionPart2
    {

        public void Solve(int seedA, int seedB)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int A = seedA;
            int B = seedB;

            int score = 0;

            for (int i = 0; i < 5_000_000; i++)
            {
                A = NextValueA(A);
                B = NextValueB(B);
                A.ToString("2");
                //Console.WriteLine($"{A}\t{B}");
                if ((A & 0b1111_1111_1111_1111) == (B & 0xffff))
                {
                    //Console.WriteLine("Match");
                    score++;

                }
            }
            sw.Stop();
            Console.WriteLine($"Score Part2 : {score}");
            Console.WriteLine($"time: {sw.Elapsed}");
        }

        private int NextValueA(int prev)
        {
            do
            {
                prev = (int)(((Int64)prev * (Int64)16807) % 2147483647);
            } while ((prev & (4-1)) != 0); // prev % 4 === prev & 0b11

            return prev;
        }

        private int NextValueB(int prev)
        {
            do
            {
                prev = (int)(((Int64)prev * (Int64)48271) % 2147483647);
            } while ( (prev & (8-1)) != 0);
            return prev;
        }
    }
}
