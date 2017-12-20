using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 20...");

            // Read Input
            //var lines = Util.Tools.readFileToList("dummyInput.txt");
            var lines = Util.Tools.readFileToList("input.txt");

            // Setup
            var Solution = new Solution() { Data = lines };

            // Solve
            Solution.SolvePart1(1_00);

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public List<string> Data { get; set; }

        private List<Particle> Particles { get; set; }

        public void SolvePart1(int step)
        {
            int id = 0;
            Particles = new List<Particle>();
            Data.ForEach(line =>
            {
                var tokens = line.Split(new char[] { '<', '>', ',' }, StringSplitOptions.RemoveEmptyEntries);
                var p = new Particle();
                p.X = long.Parse(tokens[1]);
                p.Y = long.Parse(tokens[2]);
                p.Z = long.Parse(tokens[3]);

                p.vX = long.Parse(tokens[5]);
                p.vY = long.Parse(tokens[6]);
                p.vZ = long.Parse(tokens[7]);

                p.aX = int.Parse(tokens[9]);
                p.aY = long.Parse(tokens[10]);
                p.aZ = long.Parse(tokens[11]);
                p.Id = id;
                Particles.Add(p);
                id++;
            });

            Particles.ForEach(Console.WriteLine);

            Console.WriteLine("========================");
            Console.WriteLine("Not accelerating hard and low speed");
            Particles.OrderBy(p => p.Speed).OrderBy(q=>q.Accel).Take(1).ToList().ForEach(Console.WriteLine);
            Console.WriteLine("========================");

            //Part 1 - Simulate
            /*
            for (int t = 1; t < 1000; t++)
            {
                foreach (var item in Particles)
                {
                    item.Step();
                }
                //Console.WriteLine(Particles.OrderBy(x => x.Distance).First());
            }
            Console.WriteLine(Particles.OrderBy(x => x.Distance).First());
            Console.WriteLine("========================");
            */
            //Part 1 - Simulate
            for (int t = 1; t < 1000; t++)
            {
                foreach (var item in Particles)
                {
                    item.Step();
                }

                List<int> toRemove = new List<int>();
                // Check collisions
                foreach( var item in Particles)
                {
                    toRemove.AddRange(
                        Particles.Where(p => item.X == p.X && item.Y == p.Y && item.Z == p.Z && p.Id != item.Id).Select(p => p.Id)
                    );
                }
                toRemove = toRemove.Distinct().ToList();
                Particles.RemoveAll(item => { return toRemove.Contains(item.Id); });
                Console.WriteLine($"Removed: {toRemove.Count}");
                //Console.WriteLine(Particles.OrderBy(x => x.Distance).First());
            }
            Console.WriteLine($"=Done= Particles left: {Particles.Count}");
        }


    }

    public class Particle
    {
        public int Id { get; set; }

        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public long vX { get; set; }
        public long vY { get; set; }
        public long vZ { get; set; }

        public long aX { get; set; }
        public long aY { get; set; }
        public long aZ { get; set; }

        public long Distance { get { return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z); } }

        public long Speed { get { return Math.Abs(vX) + Math.Abs(vY) + Math.Abs(vZ); } }

        public long Accel { get { return Math.Abs(aX) + Math.Abs(aY) + Math.Abs(aZ); } }


        public void Step()
        {
            vX += aX;
            vY += aY;
            vZ += aZ;
            X += vX;
            Y += vY;
            Z += vZ;
        }


        public override string ToString()
        {
            return $"{Id} -> [{X},{Y},{Z}]({vX},{vY},{vZ})<{aX},{aY},{aZ}>  -  {Distance}  -  {Speed}  -  {Accel}";
        }
    }
}
