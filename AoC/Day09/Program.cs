using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 01...");

            // Read Input
            var lines = Util.Tools.readFileToList("test.txt");

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

        public void SolvePart1()
        {
            Data.ForEach(scoreLine);

        }

        private void scoreLine(string line)
        {

            int score = 0;
            int level = 0; // nesting level
            bool inGarbage = false;
            int garbageCount = 0;

            for (int i = 0; i < line.Length; i++)
            {
                //Console.Write(line[i]);
                if (line[i] == '!') // skip next char
                {
                    i++;
                    //Console.Write($"Skip {i}");
                }
                else
                {
                    if (inGarbage)
                    {

                        if (line[i] == '>')
                        {
                            inGarbage = false;
                        }
                        else
                        {
                            garbageCount++;
                        }
                    }
                    else // not in garbage, nest if { found, unnest if }  
                    {
                        if (line[i] == '{')
                        {
                            level++;
                        }
                        else if (line[i] == '}')
                        {
                            // close group and score it.
                            //Console.Write($"Score {level}");
                            score += level;
                            level--;
                        }
                        else if (line[i] == '<') // start garbage
                        {
                            inGarbage = true;
                        }
                    }
                }

            }
            Console.WriteLine($"Score: {score}, g-count: {garbageCount}");
        }
    }
}
