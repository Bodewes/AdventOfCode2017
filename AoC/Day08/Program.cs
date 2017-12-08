using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
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

        public Dictionary<string, int> reg;
        public int max;

        public void SolvePart1()
        {
            reg = new Dictionary<string, int>();
            Data.ForEach(x=>
            {
                var s = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var adres = s[0];
                var mod = s[1]=="inc"? 1: -1;
                var delta = int.Parse(s[2]);
                var refa = s[4];
                var oper = s[5];
                var test = int.Parse(s[6]);

                var testValue = getValue(refa);
                var apply = false;
                switch (oper)
                {
                    case "<":
                        apply = testValue < test;
                        break;
                    case "<=":
                        apply = testValue <= test;
                        break;
                    case ">":
                        apply = testValue > test;
                        break;
                    case ">=":
                        apply = testValue >= test;
                        break;
                    case "==":
                        apply = testValue == test;
                        break;
                    case "!=":
                        apply = testValue != test;
                        break;
                }
                if (apply) {
                    updateAddress(adres, mod * delta);
                }
            });

            Console.WriteLine("Current Max: " + reg.Values.Max());
            Console.WriteLine("All time Max: " + max);

        }


        void updateAddress(string adress, int delta)
        {
            if (reg.ContainsKey(adress))
            {
                reg[adress] += delta;
                max = Math.Max(reg[adress], max);
            }
            else
            {
                reg.Add(adress, delta);
            }
        }


        int getValue(string adress)
        {
            if (reg.ContainsKey(adress))
            {
                return reg[adress];
            }
            else
            {
                return 0;
            }
        }

    }
}
