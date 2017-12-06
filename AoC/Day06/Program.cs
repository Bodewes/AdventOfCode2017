using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Solution();
            Console.WriteLine("=====================");
            s.Solve(new int[] { 0, 2, 7, 0 });
            Console.WriteLine("=====================");
            s.Solve(new int[] { 14,0,15,12,11,11,3,5,1,6,8,4,9,1,8,4 });
            Console.WriteLine("=====================");
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    class Solution
    {
        int[] data;

        public void Solve(int[] input)
        {
            Dictionary<string, int> history = new Dictionary<string,int>();

            data = input;

            int i = 0;

            while(true)
            {
                //Console.WriteLine($"{i}: {String.Join(", ", data)}");
                history.Add(String.Join(", ", data), i);
                // find max
                int max = data.Max();
                int index = Array.IndexOf(data, max);
                int val = data[index];
                data[index] = 0;
                while(val > 0)
                {
                    index = (++index) % data.Length;
                    data[index]++;
                    val--;
                }
                i++;

                if (history.ContainsKey(String.Join(", ", data)))
                {

                    Console.WriteLine($"Seen before at "+ history[String.Join(", ", data)]);
                    Console.WriteLine("Loop size: " + (i - history[String.Join(", ", data)]));
                    Console.WriteLine($"Iteration {i}");
                    break;
                }
                
            }
        }
    }
}
