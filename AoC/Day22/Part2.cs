using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    public class Solution2
    {

        public List<string> Data { get; set; }

        private Dictionary<(int, int), char> grid = new Dictionary<(int, int), char>();
        int infectionCount = 0;


        public void SolvePart2(int steps)
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
                    grid.Add((x, y), line[x + inputSize]);
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

                var s = get(px, py);
                // change direction
                if (s=='#') // turn right
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
                else if( s=='.') // turn left;
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
                else if (s == 'F') // reverse
                {
                    if (dx != 0)
                        dx = -dx;
                    else // dy != 0
                        dy = -dy;

                } // s==w -> Noop

                // toggle
                toggle(px, py);

                // move
                px += dx;
                py += dy;
            }

            Console.WriteLine("------------------");
            printGrid();
            Console.WriteLine($"-- Infected {infectionCount} --[{px},{py}]");

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
                    Console.Write(get(x, y));
                }
                Console.WriteLine();
            }


        }

        private char get(int x, int y)
        {
            if (grid.ContainsKey((x, y)))
                return grid[(x, y)];
            else
                return '.';
        }

        private bool infected(int x, int y)
        {
            if (grid.ContainsKey((x, y)))
                return grid[(x, y)] == '#';
            else
                return false;
        }

        private void toggle(int x, int y)
        {
            char s = '.';
            if (grid.ContainsKey((x, y))){
                s = grid[(x, y)];
            }
            if (s == '.')
                set(x, y, 'W');
            if (s == 'W')
                set(x, y, '#');
            if (s == '#')
                set(x, y, 'F');
            if (s == 'F')
                set(x, y, '.');
        }

        private void set(int x, int y, char infect)
        {
            if (infect=='#')
                infectionCount++;
            if (grid.ContainsKey((x, y)))
                grid[(x, y)] = infect;
            else
                grid.Add((x, y), infect);
        }

    }
}
