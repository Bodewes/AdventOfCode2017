using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 01...");

            // Read Input
            var lines = Util.Tools.readFileToList("input.txt");

            // Setup
            var Solution = new Solution() { Data = lines };

            // Solve
            Solution.SolvePart1();

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public List<string> Data { get; set; }
        Dictionary<string, string[]> connections;
        HashSet<string> reachable;

        public void SolvePart1()
        {
            connections = new Dictionary<string, string[]>();

            Data.ForEach(x =>
            {
                var connection = x.Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
                var progname = connection[0];
                var others = connection[2].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                connections.Add(progname, others);
                // Console.WriteLine($"Reading: {progname} ==>" + string.Join(",", connections[progname]));
            }
            );

            // Visit all neighbours from "0"
            reachable = new HashSet<string>();
            reachable.Add("0");
            visitNeighbours(connections["0"]);

            Console.WriteLine($"Answer Part 1: {reachable.Count()}");

            // Find all other groups.
            int groupCount = 1;
            foreach (var key in connections.Keys)
            {
                if (reachable.Contains(key))
                {
                    // allready visited
                }
                else
                {
                    // visit all in this group
                    groupCount++;
                    visitNeighbours(connections[key]);
                }
            }

            Console.WriteLine($"GroupCOunt {groupCount}");


        }

        public void visitNeighbours(string[] neighbours)
        {
            // Console.WriteLine("Checking: " + string.Join(",", neighbours));
            foreach (var n in neighbours)
            {
                // Console.WriteLine("    Checking: " + n);
                if (!reachable.Contains(n))
                {
                    reachable.Add(n);
                    visitNeighbours(connections[n]);
                }
            }
        }



    }
}
