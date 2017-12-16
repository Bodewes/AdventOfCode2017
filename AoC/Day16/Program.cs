using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using System.IO;
namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 16...");

            // Read Input
            var line = File.ReadAllText("input.txt");
            var testLine = "s1,x3/4,pe/b";

            // Setup
            var Solution = new Solution();


            // Solve
            //Solution.Test(testLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), 1000);
            //Solution.SolvePart1(line.Split(new char[]{ ','},StringSplitOptions.RemoveEmptyEntries));
            Solution.SolvePart1(line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), 1_000_000_000);




            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public void Test(string[] steps, int count = 2)
        {
            var dancers = new char[] { 'a', 'b', 'c', 'd', 'e' };
            string start = string.Concat(dancers);
            for (int c = 0; c < count; c++)
            {
                dancers = Dance(steps, dancers);
                
                if (string.Concat(dancers) == start)
                {
                    Console.WriteLine($" loop found at start at {c}");
                    Console.WriteLine($" Set max to c+count%c: " + ((count % c ) + c));
                    count = (count % c ) + c;
                }
                

                Console.WriteLine($"{c}\t{string.Concat(dancers)}");

            }
            Console.WriteLine("Done: " + string.Concat(dancers));
        }

        public void SolvePart1(string[] steps, int count =1)
        {

            

            var dancers = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            string start = string.Concat(dancers);
            int max = count;

            for (int c = 0; c < max; c++)
            {
                dancers = Dance(steps, dancers);
                
                if (string.Concat(dancers) == start)
                {
                    Console.WriteLine($" loop found at start at {c} --> loop size {c+1}");
                    Console.WriteLine($" Set max to c+count%c");
                    max = (count % (c+1) ) + c+1;
                }
                
             
                 Console.WriteLine($"{c}\t{string.Concat(dancers)}");
        
            }
            Console.WriteLine("Done: "+string.Concat(dancers));
        }

        private static char[] Dance(string[] steps, char[] dancers)
        {
            foreach (var step in steps)
            {
                switch (step[0])
                {
                    case 's': // s11 SPIN
                        var amount = int.Parse(step.Substring(1));
                        if (amount > 15) throw new Exception("Big S");
                        string dummy = string.Concat(dancers);
                        dancers = (dummy + dummy).Substring(dummy.Length - amount, dummy.Length).ToCharArray();
                        break;
                    case 'x': // x11/12 Swap pos
                        string[] poss = step.Substring(1).Split(new char[] { '/' });
                        int pos1 = int.Parse(poss[0]);
                        int pos2 = int.Parse(poss[1]);
                        char temp = dancers[pos1];
                        dancers[pos1] = dancers[pos2];
                        dancers[pos2] = temp;
                        break;
                    case 'p': // pX/Y  swap by name
                        char p1 = step[1];
                        char p2 = step[3];
                        int x1 = 0, x2 = 0;
                        for (int i = 0; i < dancers.Length; i++)
                        {
                            if (dancers[i] == p1)
                                x1 = i;
                            if (dancers[i] == p2)
                                x2 = i;
                        }
                        dancers[x1] = p2;
                        dancers[x2] = p1;
                        break;
                }


            }

            return dancers;
        }
    }
}
