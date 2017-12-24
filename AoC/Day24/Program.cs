using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 24...");

            // Read Input
            var lines = Util.Tools.readFileToList("dummyInput.txt");

            var part1Lines = Util.Tools.readFileToList("input.txt");

            // Setup
            var Solution = new Solution() { Data = lines };

            // Solve
            Solution.SolvePart1();

            var Part1 = new Solution() { Data = part1Lines };
            Part1.SolvePart1();

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public List<string> Data { get; set; }

        public List<(int x, int y)> pieces = new List<(int x, int y)>();

        private int max = 0;
        private int longest = 0;
        private int longest_max = 0;

        public void SolvePart1()
        {

            Data.ForEach(line =>
            {
                var tokens = line.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                pieces.Add((Math.Min(int.Parse(tokens[0]), int.Parse(tokens[1])), Math.Max(int.Parse(tokens[0]), int.Parse(tokens[1]))));
            });

            var start = pieces.Where(p => p.x == 0 || p.y == 0);
            foreach(var s in start)
            {
                var selectlist = pieces.ConvertAll(p => (p.x, p.y));
                selectlist.Remove((s.x, s.y));
                var newList = new List<(int x, int y)>();
                if (s.x == 0)
                    newList.Add((s.x, s.y));
                else
                    newList.Add((s.y, s.x));
                Grow(newList, selectlist);
            }

            Console.WriteLine($"  == Max Bridge: {max} ==");
            Console.WriteLine($"  == Longest Bridge:{longest} has strength {longest_max} ==");
        }

        public void Grow(List<(int x, int y)> list, List<(int x, int y)> availablePieces)
        {
            //printList(list);
            // find piece
            int end = list.Last().y;

            var selection = availablePieces.Where(p => p.x == end || p.y == end);
            if (selection.Count() == 0)
            {
                var value = list.Select(p => p.x + p.y).Sum();
                //Console.WriteLine($" BRIDGE DONE -> {value}");
                max = Math.Max(max, value);

                // part2
                if (list.Count() > longest)
                {
                    longest = list.Count();
                    longest_max = value;
                }else if (list.Count() == longest)
                {
                    longest_max = Math.Max(longest_max, value);
                }
                else
                {
                    // noop.
                }

            }
            // append
            foreach (var s in selection)
            {
                // copy to new lists
                var newlist = list.ConvertAll(p => (p.x, p.y));
                var selectlist = availablePieces.ConvertAll(p => (p.x, p.y));
                selectlist.Remove((s.x, s.y));

                if (s.x == end)
                {
                    newlist.Add((s.x, s.y));
                }
                else
                {
                    newlist.Add((s.y, s.x));
                }
                Grow( newlist, selectlist  );

            }
        }

        public void printList( List<(int x, int y)> x)
        {
            Console.WriteLine(string.Join("--", x.Select(p => $"{p.x}/{p.y}")));
        }

    }

}
