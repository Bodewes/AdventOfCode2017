using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 22...");

            // Read Input
            var dummyLines = Util.Tools.readFileToList("dummyInput.txt");
            var lines = Util.Tools.readFileToList("input.txt");

            // Setup
            var Solution = new Solution() { Data = lines };
            // Solve
            // Solution.SolvePart1(10_000);

            var Solution2 = new Solution2() { Data = lines };
            // Solve
            Solution2.SolvePart2(10_000_000);



            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public List<string> Data { get; set; }

        private Dictionary<(int, int), bool> grid = new Dictionary<(int, int), bool>();
        int infectionCount = 0;

        public void SolvePart1(int steps)
        {
            // Daa[0].length is odd
            var inputSize = Data[0].Length / 2;
            

            // y bottom to top
            // x left to right
            var y = inputSize;
            Data.ForEach(line =>
            {
                for (int x = -1 * inputSize; x <= inputSize; x++)
                {
                    grid.Add((x, y), line[x + inputSize] == '#');
                }
                y--;
            });
            printGrid();

            int py = 0;
            int px = 0;
            int dx = 0;
            int dy = 1; // up


            for (int i = 0; i < steps; i++)
            {

                // change direction
                if (infected(px, py))
                {
                    if (dy != 0)
                    {
                        dx = dy == 1 ? 1 : -1;
                        dy = 0;
                    }
                    else // facing left/right
                    {
                        dy = dx == 1 ? -1 : 1;
                        dx = 0;
                    }

                }
                else
                {
                    if (dy != 0)
                    {
                        dx = dy == 1 ? -1 : 1;
                        dy = 0;
                    }
                    else // facing left/right
                    {
                        dy = dx == 1 ? 1 : -1;
                        dx = 0;
                    }
                }

                // flip
                set(px, py, !infected(px, py));

                // move
                px += dx;
                py += dy;
            }

            Console.WriteLine("------------------");
            printGrid();
            Console.WriteLine($"-- Infected {infectionCount} --");

        }

        private void printGrid()
        {
            //grid.Select(x => x.ToString()).ToList().ForEach(Console.WriteLine);

            var maxx = grid.Keys.Select(x => x.Item1).Max();
            var minx = grid.Keys.Select(x => x.Item1).Min();
            var maxy = grid.Keys.Select(x => x.Item2).Max();
            var miny = grid.Keys.Select(x => x.Item2).Min();


            for (int y = maxy; y >= miny; y--)
            {
                for (int x = minx; x <= maxx; x++)
                {
                    Console.Write(infected(x, y) ? '#' : '.');
                }
                Console.WriteLine();
            }
        

        }



        private bool infected(int x, int y)
        {
            if (grid.ContainsKey((x, y)))
                return grid[(x, y)];
            else
                return false;
        }
        private void set(int x, int y, bool infect)
        {
            if (infect)
                infectionCount++;
            if (grid.ContainsKey((x, y)))
                grid[(x, y)] = infect;
            else
                grid.Add((x, y), infect);
        }

    }

}

