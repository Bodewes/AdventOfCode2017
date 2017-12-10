using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {

            var s = new Solution();
            //s.Solve(5, new int[] { 3, 4, 1, 5 });

            //s.Solve(256, new int[] { 106, 118, 236, 1, 130, 0, 235, 254, 59, 205, 2, 87, 129, 25, 255, 118 });

            s.SolvePart2(256, "");
            s.SolvePart2(256, "AoC 2017");
            s.SolvePart2(256, "1,2,3");
            s.SolvePart2(256, "1,2,4");

            s.SolvePart2(256, "106,118,236,1,130,0,235,254,59,205,2,87,129,25,255,118");

            Console.ReadLine();
        }
    }

    class Solution
    {

        public void Solve(int size, int[] lengths)
        {
            int pos = 0;
            int skip = 0;

            // Init
            int[] list = new int[size];
            for (int i = 0; i < size; i++)
                list[i] = i;

            Console.WriteLine("List:" + string.Join(",", list) + $" pos: {pos} skip:{skip}");

            lengths.ToList().ForEach(l =>
            {
                if (l <= size)
                {
                    // read to mem;
                    int[] mem = new int[l];
                    for (int mi = pos; mi < pos + l; mi++)
                    {
                        mem[mi - pos] = list[mi % size];
                    }

                    // reverse
                    mem = mem.Reverse().ToArray();

                    // write
                    for (int mi = pos; mi < pos + l; mi++)
                    {
                        list[mi % size] = mem[mi - pos];
                    }

                    // move next
                    pos = (pos + l + skip) % size;
                    skip++;

                    Console.WriteLine( "List:"+string.Join(",",list) +$" pos: {pos} skip:{skip}");

                }
            });
            Console.WriteLine("Answer: "+(list[0] * list[1]));



        }

        public void SolvePart2(int size, string input)
        {


            var lengths = input.ToCharArray();
            lengths = lengths.Concat(new char[] { (char)17, (char)31, (char)73, (char)47, (char)23 }).ToArray<char>();

            int pos = 0;
            int skip = 0;

            // Init
            int[] list = new int[size];
            for (int i = 0; i < size; i++)
                list[i] = i;

            //Console.WriteLine("List:" + string.Join(",", list) + $" pos: {pos} skip:{skip}");
            
            for(var round = 0; round< 64; round++)
            {
                lengths.ToList().ForEach(l =>
                {
                    if (l <= size)
                    {
                        // read to mem;
                        int[] mem = new int[l];
                        for (int mi = pos; mi < pos + l; mi++)
                        {
                            mem[mi - pos] = list[mi % size];
                        }

                        // reverse
                        mem = mem.Reverse().ToArray();

                        // write
                        for (int mi = pos; mi < pos + l; mi++)
                        {
                            list[mi % size] = mem[mi - pos];
                        }

                        // move next
                        pos = (pos + l + skip) % size;
                        skip++;

                        //Console.WriteLine("List:" + string.Join(",", list) + $" pos: {pos} skip:{skip}");

                    }
                });
            }

            // Make dense:
            int[] dense = new int[16];
            for(int d =0; d< dense.Length; d++)
            {
                dense[d] = list[0 + d * 16] ^ list[1 + d * 16] ^ list[2 + d * 16] ^ list[3 + d * 16] ^
                           list[4 + d * 16] ^ list[5 + d * 16] ^ list[6 + d * 16] ^ list[7 + d * 16] ^
                           list[8 + d * 16] ^ list[9 + d * 16] ^ list[10 + d * 16] ^ list[11 + d * 16] ^
                           list[12 + d * 16] ^ list[13 + d * 16] ^ list[14 + d * 16] ^ list[15 + d * 16];
            }

            Console.WriteLine(input+": "+string.Concat(dense.Select(d => d.ToString("x2"))));

            //Console.WriteLine("Answer: " + (list[0] * list[1]));

        }

    }
}
