using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 21...");

            // Read Input
            var lines = Util.Tools.readFileToList("dummyInput.txt");

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

        string[] art = new string[3] { ".#.", "..#", "###" };

        public List<string> Data { get; set; }


        private Dictionary<string, string> transformations = new Dictionary<string, string>();

        public void SolvePart1()
        {
            Data.ForEach(line =>
            {
                var tokens = line.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);
                var match = tokens[0];

                // Permutations
                addTransformation(match, tokens[1]);
                addTransformation(rotate(match), tokens[1]);
                addTransformation(rotate(rotate(match)), tokens[1]);
                addTransformation(rotate(rotate(rotate(match))), tokens[1]);

                addTransformation(flip(match), tokens[1]);
                addTransformation(flip(rotate(match)), tokens[1]);
                addTransformation(flip(rotate(rotate(match))), tokens[1]);
                addTransformation(flip(rotate(rotate(rotate(match)))), tokens[1]);
            });

            Console.WriteLine("Iteration 0:");
            printArt(art);

            for(int i = 0; i < 18; i++)
            {
                //Console.WriteLine($"Iteration {i+1}:");
                //Console.WriteLine("Slices");
                var slices = slice(art);
                var newSlices = new List<string>();
                foreach (var slice in slices)
                {
                    //Console.WriteLine(slice);
                    newSlices.Add(transfrom(slice));
                }
                //Console.WriteLine("==New Slices==");
                //newSlices.ForEach(Console.WriteLine);
                //Console.WriteLine("==New Art==");
                art = glue(newSlices);
                printArt(art);
                //Console.WriteLine("==*==");
            }
            var total = 0;
            foreach(var line in art)
            {
                total += line.Count(x => x == '#');
            }
            Console.WriteLine($"On are: {total}");



            //art = transfrom(art);
            //printArt(art);

            // slice and transform.

            // past together

        }


        private string[] glue(List<string> slices)
        {
            var sliceSize = slices[0].Length == 11?3:4; // 3*3+2  or 4*4+3
            var size = (int)Math.Sqrt(slices.Count()); // aantal slices in x of y richting
            var grid = new string[size*sliceSize];
            if (sliceSize == 3) //3x3
            {
                for (int i = 0; i < size; i++) // rows
                {

                    grid[i * sliceSize] = "";
                    grid[i * sliceSize + 1] = "";
                    grid[i * sliceSize + 2] = "";
                    for (int j = 0; j < size; j++) // cols
                    {
                        grid[i * sliceSize] += $"{slices[i * size + j][0]}{slices[i * size + j][1]}{slices[i * size + j][2]}";
                        grid[i * sliceSize + 1] += $"{slices[i * size + j][4]}{slices[i * size + j][5]}{slices[i * size + j][6]}";
                        grid[i * sliceSize + 2] += $"{slices[i * size + j][8]}{slices[i * size + j][9]}{slices[i * size + j][10]}";
                    }
                }

            }
            else //slicesize == 4, 4x4
            {
                for (int i = 0; i < size; i++) // rows
                {

                    grid[i * sliceSize] = "";
                    grid[i * sliceSize + 1] = "";
                    grid[i * sliceSize + 2] = "";
                    grid[i * sliceSize + 3] = "";
                    for (int j = 0; j < size; j++) // cols
                    {
                        grid[i * sliceSize] += $"{slices[i * size + j][0]}{slices[i * size + j][1]}{slices[i * size + j][2]}{slices[i * size + j][3]}";
                        grid[i * sliceSize + 1] += $"{slices[i * size + j][5]}{slices[i * size + j][6]}{slices[i * size + j][7]}{slices[i * size + j][8]}";
                        grid[i * sliceSize + 2] += $"{slices[i * size + j][10]}{slices[i * size + j][11]}{slices[i * size + j][12]}{slices[i * size + j][13]}";
                        grid[i * sliceSize + 3] += $"{slices[i * size + j][15]}{slices[i * size + j][16]}{slices[i * size + j][17]}{slices[i * size + j][18]}";
                    }
                }
            }
            return grid;

        }

        private List<string> slice(string[] grid)
        {
            var slices = new List<string>();
            if (grid.Length % 2 == 0)
            {
                // create 2x2 subsets

                // 0011
                // 0011
                // 2233
                // 2233
                var size = grid.Length / 2; //2 (voor 4x4)
               
                for(int i=0; i< size*size; i++)
                {
                    var row = i / size;
                    var col = i % size;
                    
                    slices.Add($"{grid[row * 2][col*2]}{grid[row * 2][col*2 + 1]}/{ grid[row * 2+1][col*2]}{ grid[row * 2+1][col*2 +  1]}");
                }
            }
            else
            {
                //create 3x3

                var size = grid.Length / 3; //2 (voor 6x6)
                for (int i = 0; i < size * size; i++)
                {
                    var row = i / size;
                    var col = i % size;

                    slices.Add($"{grid[row * 3][col*3 ]}{grid[row * 3][col*3 +  1]}{grid[row * 3][col*3 + 2]}/"+
                               $"{ grid[row * 3 + 1][col*3]}{ grid[row * 3 + 1][col*3 + 1]}{ grid[row * 3 + 1][col*3 + 2]}/"+
                               $"{ grid[row * 3 + 2][col*3]}{ grid[row * 3 + 2][col*3 + 1]}{ grid[row * 3 + 2][col*3 +  2]}");
                }
            }
            return slices;
        }


        private void printArt(string[] grid)
        {
            foreach (var line in grid)
            {
                Console.WriteLine(line);
            }
        }

        private string transfrom(string grid)
        {
            string result = transformations[grid];
            return result;

        }


        private bool addTransformation(string match, string target)
        {
            //Console.WriteLine($"adding: {match}  ->  {target}");
            try
            {
                transformations.Add(match, target);
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        // rotate clockwise
        private string rotate(string s)
        {
            if (s.Length == 5) // 2x2
            {
                // "01/34":
                // 01 ==> 30
                // 34 ==> 41
                // "31/42"
                return $"{s[3]}{s[0]}/{s[4]}{s[1]}";
            }
            else // 3x3
            {  
                // "012/456/89X"
                // 012    840
                // 456 => 951
                // 89X    X62
                return $"{s[8]}{s[4]}{s[0]}/{s[9]}{s[5]}{s[1]}/{s[10]}{s[6]}{s[2]}";
            }
        }

        // flip left-right
        private string flip(string s)
        {

            if (s.Length == 5) // 2x2
            {
                // "01/34":
                // 01 ==> 10
                // 34 ==> 43
                // "10/43"
                return $"{s[1]}{s[0]}/{s[4]}{s[3]}";
            }
            else// 3x3
            {  
                // "012/456/89X"
                // 012    210
                // 456 => 654
                // 89X    X98
                return $"{s[2]}{s[1]}{s[0]}/{s[6]}{s[5]}{s[4]}/{s[10]}{s[9]}{s[8]}";
            }
        }

    }
}
