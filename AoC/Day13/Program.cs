using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {

            var s = new Solution();
            var firewallTest = new Dictionary<int, int>();
            firewallTest.Add(0, 3);
            firewallTest.Add(1, 2);
            firewallTest.Add(4, 4);
            firewallTest.Add(6, 4);

            //s.SolvePart1(firewallTest);
            

            var lines = File.ReadAllLines("input.txt");
            var firewall = new Dictionary<int, int>();
            foreach (var line in lines)
            {
                var split = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                firewall.Add(int.Parse(split[0].Trim()), int.Parse(split[1].Trim()));
            }
            //s.SolvePart1(firewall);


            s.SolvePart2(firewallTest);
            s.SolvePart2(firewall);
            Console.ReadKey();
        }
    }

    class Solution
    {

        public void SolvePart1(Dictionary<int, int> firewall)
        {
            int severity = 0;
            var max = firewall.Keys.Max();
            for(int i=0; i< max + 1; i++)
            {
                Console.Write($"At: {i} ");
                if (firewall.ContainsKey(i)) 
                {
                    var depth = firewall[i];
                    Console.Write($"depth: {depth}, scanner at {i%(2*depth-2)}");

                    if (i % (2 * depth - 2) == 0)
                    {
                        Console.WriteLine($"\tCaught at {i} with depth {depth}");
                        severity += i * depth; 
                    }

                }
                Console.WriteLine("");
            }

            Console.WriteLine($"Severity: {severity}");
        }



        // Naive
        public void SolvePart2(Dictionary<int, int> firewall)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var max = firewall.Keys.Max();
            int delay = 0;
            bool caught = false;
            do {
                caught = false;
                //Console.WriteLine($"Delay: {delay}");
                for (int i = delay; i < max + 1+delay; i++)
                {
                    //Console.Write($"At: {i} ");
                    if (firewall.ContainsKey(i-delay))
                    {
                        var depth = firewall[i-delay];
                        //Console.Write($"depth: {depth}, scanner at {i % (2 * depth - 2)}");

                        if (i % (2 * depth - 2) == 0)
                        {
                            //Console.WriteLine($"\tCaught at {i} with depth {depth}");
                            caught = true;
                            break; //<- stop as soon as caugth! from 7 to 0.4 seconds!
                        }

                    }
                    //Console.WriteLine("");
                }

                //Console.WriteLine($"=====================");
                delay++;
            } while (caught);

            Console.WriteLine($"Final Delay: {delay-1}");

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());

        }
    }
}
