using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {

            var s = new Solution();

            //s.solvePart1(3, 2017);

            //s.solvePart1(356, 2017);

            s.solvePart1(3, 100);
            s.solvePart2(3, 100);
            Console.WriteLine(" Now for real...");
            s.solvePart1(356,2017);
            s.solvePart2(356, 50_000_000);

            Console.ReadLine();
        }
    }

    public class Solution
    {

        public void solvePart1(int step, int max)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Init
            var b = new Buffer() { Value = 0 };
            b.Next = b;
            var start = b;

            for(int i = 1; i< max+1; i++)
            {
                // do steps
                for (int s =0; s< step; s++)
                {
                    b = b.Next;
                }
                // insert
                var k = new Buffer() { Value = i, Next = b.Next };
                b.Next = k;
                b = k;
                //printBuffer(start, 10);
                if (i % 100_000 == 0) Console.WriteLine(i);
            }

            Console.WriteLine($"Buffer {b.Value} and the next buffer {b.Next.Value}");
            Console.WriteLine($"{sw.Elapsed}");
            Console.WriteLine();

        }

        public void printBuffer(Buffer start, int count)
        {
            var p = start;
            do
            {
                Console.Write(p.Value + ", ");
                p = p.Next;
                count--;
            } while (p != start && count >0 );
            Console.WriteLine();
        }

        public void solvePart2(int step, int max)
        {

            // find when current pos is zero.
            int bufferSize = 1;
            int currentPos = 0;
            int lastAdded = 0;
            for(int i = 1; i < max+1; i++)
            {
                currentPos = (currentPos + step) % bufferSize;
                bufferSize++;
                currentPos++;
                //Console.WriteLine(currentPos);
                if (currentPos == 1)
                {
                    lastAdded = i;
                    Console.WriteLine($"Adding {i} (bufferSize={bufferSize}");
                }
            }
            Console.WriteLine($"After Zero is: {lastAdded}");


        }


    }

    public class Buffer
    {
        public int Value { get; set; }
        public Buffer Next { get; set; }
    }

}
