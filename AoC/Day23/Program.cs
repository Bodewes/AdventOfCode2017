using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 23...");

            // Read Input
            var lines = Util.Tools.readFileToList("input.txt");

            // Setup
            var Solution = new Solution() { Data = lines };

            // Solve
            Solution.SolvePart1(0, true);

            Solution.Part2(108100, 125100);

            //Solution.SolvePart1(a: 1);

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public void Part2(int from, int to)
        {
            int primes = 0;
            int total = 0;
            for(int i = from; i<=to; i = i + 17)
            {
                total++;
                if (IsPrime(i))
                {
                    primes++;
                }
            }
            Console.WriteLine($"{total}-{primes}={total - primes}");

        }

        public static bool IsPrime(int number)
        {
            if (number == 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }


        public List<string> Data { get; set; }

        public long[] reg = new long[8];

        public Instruction[] prog;

        public void SolvePart1(long a = 0, bool debug = false)
        {
            prog = new Instruction[Data.Count()];
            ParseInput();

            reg[0] = a;

            int mulCount = 0;
            int opCount = 0;

            long pc = 0;
            while(pc>=0 && pc  < prog.Length)
            {

                if (opCount % 1_000 == 0 || debug)
                {
                    Console.WriteLine($"{opCount}: \t[{pc}] -> \t{string.Join(",\t", reg)}");
                }

                opCount++;

                var i = prog[pc];
                var x = i.XIsRef ? reg[i.X_ref] : i.X_const;
                var y = i.YIsRef ? reg[i.Y_ref] : i.Y_const;
                
                switch (i.OpCode){
                    case OpCode.set:
                        reg[i.X_ref] = y;
                        break;
                    case OpCode.sub:
                        reg[i.X_ref] = x - y;
                        break;
                    case OpCode.mul:
                        mulCount++;
                        reg[i.X_ref] = x * y;
                        break;
                    case OpCode.jnz:
                        if (x != 0)
                            pc += y-1;
                        break;
                }
                pc++;


            }

            Console.WriteLine($"== MulCount: {mulCount}");


        }

        private void ParseInput()
        {
            int counter = 0;
            Data.ForEach(line =>
            {
                var tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var i = new Instruction();
                OpCode opCode;
                Enum.TryParse(tokens[0], out opCode);
                i.OpCode = opCode;
                var x = tokens[1];
                if (long.TryParse(x, out long x_const))
                {
                    i.X_const = x_const;
                    i.XIsRef = false;
                }
                else
                {
                    i.X_ref = x[0]-'a';
                    i.XIsRef = true;
                }

                var y = tokens[2];
                if (long.TryParse(y, out long y_const))
                {
                    i.Y_const = y_const;
                    i.YIsRef = false;
                }
                else
                {
                    i.Y_ref = y[0]-'a';
                    i.YIsRef = true;
                }
                prog[counter] = i;
                counter++;
            });
        }
    }

    public enum OpCode
    {
        set,
        sub,
        mul,
        jnz,

    }

    public class Instruction
    {
        public OpCode OpCode { get; set; }

        public bool XIsRef { get; set; } // is X a reference or not
        public bool YIsRef { get; set; }

        public long X_const { get; set; }
        public int X_ref { get; set; } // index in reg 'a' ==0, h='8'


        public long Y_const { get; set; }
        public int Y_ref { get; set; } // index in reg 'a' ==0, h='8'

    }
}
