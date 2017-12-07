using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Util;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 07");
            List<string> testInput = Util.Tools.readFileToList("testInput.txt");
            List<string> input = Util.Tools.readFileToList("input.txt");

            var s = new Solution();
            s.Solve(testInput);
            Console.WriteLine("--------------------");
            s.Solve(input);

            Console.ReadLine();
        }
    }

    class Solution
    {

        List<Prog> roots;

        public void Solve(List<string> input)
        {

            roots = new List<Prog>();

            // Read all lines to progs
            foreach (string line in input)
            {
                //Console.WriteLine(line);
                //https://regex101.com/
                var r = new Regex(@"(\w+) \((\d+)\)( \-> ([\w\s,]+))?");
                var match = r.Match(line);

                var p = new Prog();
                p.Name = match.Groups[1].ToString(); 
                p.Weight = int.Parse(match.Groups[2].ToString());
                p.TotalWeight = p.Weight; // Set total initial to self.

                if (match.Groups.Count > 3)
                {
                    p.unresolvedSubs = match.Groups[4].ToString().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                roots.Add(p);
                //Console.WriteLine($"{p}");
            }

            // reorder, link all fixed substrees to parent.
            while (roots.Count() > 1)
            {
                // get all leaves (with no unresovedSubs)
                var leaves = roots.Where(p => p.unresolvedSubs.Count == 0).ToList();
                // place each leave at parent
                foreach(var leave in leaves)
                {
                    // find parent.
                    var parent = roots.Where(p => p.unresolvedSubs.Contains(leave.Name)).First();
                    parent.Subs.Add(leave); 
                    parent.TotalWeight += leave.TotalWeight;
                    parent.unresolvedSubs.Remove(leave.Name);



                    // check balance, for PART 2
                    if ( parent.Subs.Select(x=>x.TotalWeight).Distinct().Count() != 1)  // do all subs have the same weight?
                    {
                        // unbalanced, only balance if all subs all present.
                        if (parent.unresolvedSubs.Count() == 0)
                        {
                            Console.WriteLine($"Balance children of: {parent.Name}");
                            foreach (var sub in parent.Subs) // loop over all subs to check them (againt the other subs)
                            {
                                Console.WriteLine($"{sub.Name}: {sub.TotalWeight}");
                                if(parent.Subs.Where(x=>x.TotalWeight == sub.TotalWeight).Count()==1)
                                {
                                    Console.WriteLine($"THIS ONE IS OFF: {sub.Name} {sub.Weight} {sub.TotalWeight}");
                                    var correct = parent.Subs.Find(x => x.TotalWeight != sub.TotalWeight).TotalWeight;
                                    var delta = sub.TotalWeight- correct;
                                    Console.WriteLine($"CORRECT WEIGHT is {sub.Weight-delta}");

                                    // FIX this one
                                    sub.Weight -= delta;
                                    sub.TotalWeight -= delta;
                                    parent.TotalWeight -= delta;
                                }
                            }
                           
                        }

                    } 

                    roots.Remove(leave);
                    //Console.WriteLine($"Added {leave.Name} to {parent.Name}");
                }
            }

            Console.WriteLine($"Aantal roots: {roots.Count}");
            Console.WriteLine($"Root: {roots[0]}");
            
        }

    }

    class Prog
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int TotalWeight { get; set; }
        public List<Prog> Subs { get; set; }
        public List<String> unresolvedSubs { get; set; }
        public Prog()
        {
            Subs = new List<Prog>();
        }

        public override string ToString()
        {
            return $"{Name} \t ({Weight})\t{TotalWeight} -> {string.Join(",", unresolvedSubs.ToArray())}";
        }
    }
}
