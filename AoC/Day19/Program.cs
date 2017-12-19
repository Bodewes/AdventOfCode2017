using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using System.IO;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 19...");

            // Read Input
            //var lines = Util.Tools.readFileToList("dummyInput.txt");
            var lines = Util.Tools.readFileToList("part1.txt");


            // Letters in input maze: SJETMGPKV

            // Setup
            var Solution = new Solution() { Maze = lines.Select(x => x.ToCharArray()).ToArray() };

            // Solve
            Solution.SolvePart1();

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public char[][] Maze;  // y,x; first is row, second is pos in row, row increase downward.

        public void SolvePart1()
        {
            int x = 0;
            int y = 0;
            int steps = 0;

            // Find start (in line 0)
            for(int i =0; i < Maze[0].Length; i++)
            {
                if (Maze[0][i] == '|')
                {
                    x = i;
                    break;
                }
            }
            Console.WriteLine($"Pos: {x},{y}");

            string path = "";
            int dx = 0;
            int dy = 1;
            bool done = false;
            while (!done)
            {
                steps++;
                var c = Maze[y][x];
                Console.WriteLine($"Pos: [{x},{y}] -> [{c}]");
               
                // walk the maze.
                switch (c)
                {
                    case '+':
                        // turn
                        if (dy == 0)
                        {
                            dy = Maze[y - 1][x] != ' ' ? -1 : 1;
                            dx = 0;
                        }
                        else //dx == 0
                        {
                            dx = Maze[y][x-1] != ' ' ? -1 : 1;
                            dy = 0;
                        }
                        x += dx;
                        y += dy;
                        break;

                    case '|':
                    case '-':
                        // Continue
                        x += dx;
                        y += dy;
                        break;
                    case ' ':
                        Console.WriteLine($"PANIC, lost path, letters seen {path} in {steps-1}");
                        done = true;
                        break;

                    default:
                        // Found letter
                        Console.WriteLine($"LETTER: {c}");
                        path += c;
                        x += dx;
                        y += dy;
                        break;
                }
            }
        }
    }
}
