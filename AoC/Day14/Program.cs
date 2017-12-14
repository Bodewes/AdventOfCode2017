using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.BufferWidth = 150;
            Console.WindowWidth = 150;

            var s = new Solution();
            var test = "flqrgnkx";
            s.SolvePart1(test);
            s.SolvePart2();

            
            s.SolvePart1("oundnydw");
            s.SolvePart2();
            



            Console.ReadLine();
        }
    }

    class Solution
    {
        char[][] disk = new char[128][];


        public void SolvePart1(string seed)
        {

            //Console.WriteLine(Hex2Bits("a0c2017"));
            //return;
            var total = 0;

            for(int i = 0; i< 128; i++)
            {
                var input = seed + "-" + i;
                var hash = KnotHash(256,input);
                var bits = Hex2Bits(hash);
                disk[i] = bits.ToArray();
                var linecount = bits.Count(c => c == '1');
                total += linecount;
                Console.WriteLine(i+"\t"+linecount+"\t"+bits);
                
            }
            Console.WriteLine("Total: " + total);

        }


        public void SolvePart2()
        {
            int groupCount = 0;
            Neighbours = new List<(int, int)>();
            // loop over all positions (i,j).
            for (int i=0; i < 128; i++)
            {
                for(int j=0; j< 128; j++)
                {
                    if (disk[i][j] == '1')
                    {
                        FindNeighbours(i, j); // findneighbours and add to a list
                        MarkNeighbours(); // mark as visited.
                        groupCount++;
                        
                        //WriteDisk(10);
                        //return;
                    }

                }
            }
            Console.WriteLine("groupcount:" + groupCount);
            
        }

        private void WriteDisk(int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(disk[i][j]);
                }
                Console.WriteLine();
            }
        }

        private List<(int, int)> Neighbours;

        private void FindNeighbours(int i, int j)
        {
            if (i < 0 || i > 127 || j < 0 || j > 127) return;

            if (disk[i][j] == '1') // found one.
            {
                if (!Neighbours.Contains((i, j))) { // do i know about it yet?
                    Neighbours.Add((i, j)); // no, add it.

                    FindNeighbours(i +1, j); // and visit its neighbours.
                    FindNeighbours(i -1, j);
                    FindNeighbours(i , j+1);
                    FindNeighbours(i , j-1);
                }
            }
        }

        private void MarkNeighbours()
        {
            foreach(var n in Neighbours)
            {
                disk[n.Item1][n.Item2] = 'X';
            }
        }



        public string Hex2Bits(string hex)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i< hex.Length; i++)
            {
                switch (hex[i])
                {
                    case '0': builder.Append("0000"); break;
                    case '1': builder.Append("0001"); break;
                    case '2': builder.Append("0010"); break;
                    case '3': builder.Append("0011"); break;
                    case '4': builder.Append("0100"); break;
                    case '5': builder.Append("0101"); break;
                    case '6': builder.Append("0110"); break;
                    case '7': builder.Append("0111"); break;
                    case '8': builder.Append("1000"); break;
                    case '9': builder.Append("1001"); break;
                    case 'a': builder.Append("1010"); break;
                    case 'b': builder.Append("1011"); break;
                    case 'c': builder.Append("1100"); break;
                    case 'd': builder.Append("1101"); break;
                    case 'e': builder.Append("1110"); break;
                    case 'f': builder.Append("1111"); break;
                }
            }
            return builder.ToString();
        }


        public string KnotHash(int size, string input)
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

            for (var round = 0; round < 64; round++)
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
            for (int d = 0; d < dense.Length; d++)
            {
                dense[d] = list[0 + d * 16] ^ list[1 + d * 16] ^ list[2 + d * 16] ^ list[3 + d * 16] ^
                           list[4 + d * 16] ^ list[5 + d * 16] ^ list[6 + d * 16] ^ list[7 + d * 16] ^
                           list[8 + d * 16] ^ list[9 + d * 16] ^ list[10 + d * 16] ^ list[11 + d * 16] ^
                           list[12 + d * 16] ^ list[13 + d * 16] ^ list[14 + d * 16] ^ list[15 + d * 16];
            }

            string hash = string.Concat(dense.Select(d => d.ToString("x2")));

            //Console.WriteLine(input + ": " + hash );
            return hash;
            //Console.WriteLine("Answer: " + (list[0] * list[1]));

        }

    }
}
