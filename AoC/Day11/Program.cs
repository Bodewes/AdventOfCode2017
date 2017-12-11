using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Solution();
            s.SolvePart1("ne,ne,ne");
            s.SolvePart1("ne,ne,sw,sw");
            s.SolvePart1("ne,ne,s,s");
            s.SolvePart1("se,sw,se,sw,sw");
            s.SolvePart1(File.ReadAllText("input.txt"));
            Console.ReadLine();
        }
    }

    class Solution
    {

        public void SolvePart1(string input)
        {
            //https://www.redblobgames.com/grids/hexagons/

            (int, int, int) pos = (0, 0, 0);


            var steps = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var maxDistance = 0;
            var distance = 0;

            foreach(var step in steps)
            {
                switch (step)
                {
                    case "n":
                        pos = (pos.Item1 + 0, pos.Item2 + 1, pos.Item3 + -1);
                        break;
                    case "ne":
                        pos = (pos.Item1 + 1, pos.Item2 + 0, pos.Item3 + -1);
                        break;
                    case "se":
                        pos = (pos.Item1 + 1, pos.Item2 + -1, pos.Item3 + 0);
                        break;
                    case "s":
                        pos = (pos.Item1 + 0, pos.Item2 + -1, pos.Item3 + 1);
                        break;
                    case "sw":
                        pos = (pos.Item1 + -1, pos.Item2 + 0, pos.Item3 + 1);
                        break;
                    case "nw":
                        pos = (pos.Item1 + -1, pos.Item2 + 1, pos.Item3 + 0);
                        break;
                }
                distance = Math.Max(Math.Max(Math.Abs(pos.Item1), Math.Abs(pos.Item2)), Math.Abs(pos.Item3));
                maxDistance = Math.Max(maxDistance, distance);
            }

            
            Console.WriteLine($"Afstand: {distance}");
            Console.WriteLine($"Max Afstand: {maxDistance}");
        }

    }
}
