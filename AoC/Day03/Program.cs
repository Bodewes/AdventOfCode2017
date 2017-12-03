using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 03...");

            // Setup
            var Solution = new Solution();

            // Solve
            Solution.SolvePart1(1);
            Solution.SolvePart1(12);
            Solution.SolvePart1(23);
            Solution.SolvePart1(1024);
            Solution.SolvePart1(347991);

            Console.WriteLine("== PART 2 ==.");
            Solution.SolvePart2(347991);


            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        Dictionary<(int, int), int> cache;
        (int,int) right = (1, 0);
        (int, int) up = (0, 1);
        (int, int) left = (-1, 0);
        (int, int) down = (0, -1);
        int lastAddedValue;

        public void SolvePart1(int input)
        {
            // Determine start ring (rings numberd 1..n)
            Console.Write(input + ": ");
            int ring = 1;
            while ((ring + ring - 1) * (ring + ring - 1) < input)
            {
                ring++;
            }
            Console.Write(" r:" + ring);

            // 

            var start = (2 * ring - 3) * (2 * ring - 3) + 1;
            var min_step = (ring - 1);
            var max_step = (ring - 1) * 2;
            Console.Write($"\t min step:{min_step}");
            Console.Write($"\t max step:{max_step}");
            Console.Write($"\t start:{start}");

            var dir = -1; // initial step change direction at first in ring.
            var dist = (ring - 1) * 2 - 1; // initial distance at first in ring
            Console.WriteLine();
            Console.Write("Start: " + start);
            while (start <= input)
            {

                // Console.Write($" d:{dist}");
                dist += dir;
                start++;
                if (dist == min_step || dist == max_step) dir *= -1;
            }
            Console.Write("\tSTEPS" + (dist - 1)); // one-off. Way?
            Console.WriteLine();
        }


        public void SolvePart2(int input)
        {
            cache = new Dictionary<(int, int), int>(); // init cache.
            cache.Add((0, 0), 1); // start value.
            var counter = 1; // Memory cell number

            var current = (0, 0);
            var size = 1; // size

            // First step right.
            current = current.Add(right);
            size += 2;
            counter++;
            Console.Write($"c:{counter} size:{size}");
            calculateValueAtCurrentAndAddToCache(current);



            // repeat.
            while(true)
            {
                // One loop around the sqaure. position: already stepped out to the right.

                // move up size-1 times
                for (int k = 0; k < size-1-1; k++)
                {
                    counter++;
                    current = current.Add(up);
                    Console.Write($"c:{counter} size:{size}");
                    calculateValueAtCurrentAndAddToCache(current);
                    if (lastAddedValue > input) goto end;
                }

                // move left size times
                for (int k = 0; k < size - 1; k++)
                {

                    counter++;
                    current = current.Add(left);
                    Console.Write($"c:{counter} size:{size}");
                    calculateValueAtCurrentAndAddToCache(current);
                    if (lastAddedValue > input) goto end;
                }

                // move down size times
                for (int k = 0; k < size - 1; k++)
                {
                    counter++;
                    current = current.Add(down);
                    Console.Write($"c:{counter} size:{size}");
                    calculateValueAtCurrentAndAddToCache(current);
                    if (lastAddedValue > input) goto end;
                }

                // move right size+1 times 
                for (int k = 0; k < size ; k++)
                {
                    counter++;
                    current = current.Add(right);
                    Console.Write($"c:{counter} size:{size}");
                    calculateValueAtCurrentAndAddToCache(current);
                    if (lastAddedValue > input) goto end;
                }
                size  +=2;

            }
            end:
            { }


        }

        private void calculateValueAtCurrentAndAddToCache((int, int) current)
        {
            var s = sumFor(current);
            cache.Add(current, s);
            Console.WriteLine($"{current} - {s}");
            lastAddedValue = s;
        }

        public int sumFor((int,int) location)
        {
            int sum = 0;
            sum += GetCacheValueAt(location.Add(left));
            sum += GetCacheValueAt(location.Add(left).Add(up));
            sum += GetCacheValueAt(location.Add(up));
            sum += GetCacheValueAt(location.Add(up).Add(right));
            sum += GetCacheValueAt(location.Add(right));
            sum += GetCacheValueAt(location.Add(right).Add(down));
            sum += GetCacheValueAt(location.Add(down));
            sum += GetCacheValueAt(location.Add(down).Add(left));

            (int, int) newPos = (2, 4).Add((1, -1));

            return sum;
        }

        private int GetCacheValueAt((int, int) location)
        {
            if (cache.ContainsKey(location))
            {
                return cache[location];
            }

            return 0;
        }
    }

    public static class TupleExtensions
    {
        static public (int, int) Add(this (int, int) a, (int, int)b)
        {
            return (a.Item1 + b.Item1, a.Item2 + b.Item2);
        }
    }
}
