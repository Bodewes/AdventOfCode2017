using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 200;
            Console.BufferHeight = 1000;
            Console.WriteLine("Day 18...");

            // Read Input
            var testLines = Util.Tools.readFileToList("test.txt");
            var lines = Util.Tools.readFileToList("input.txt");
            var part2 = Util.Tools.readFileToList("part2.txt");

            // Setup
            //var Solution = new Solution() { Data = testLines };


            // Part 2
            var Prog00 = new Solution(0) { Data = lines };
            var Prog01 = new Solution(1) { Data = lines };

            Prog00.inBuffer = Prog01.outBuffer;
            Prog01.inBuffer = Prog00.outBuffer;

            Prog00.Other = Prog01;
            Prog01.Other = Prog00;

            // Solve
            var a= Task.Run(()=>Prog00.Run());
            var b =Task.Run(()=>Prog01.Run());

            //Task.WaitAll(new Task[] { a,b });

            // Ready.
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }

    public class Solution
    {

        public Solution(long id)
        {
            Id = id;
            reg.Add('p', id);
            Waiting = false;
            outBuffer = new ConcurrentQueue<long>();
        }

        public List<string> Data { get; set; }
        public ConcurrentQueue<long> outBuffer { get; set; }
        public ConcurrentQueue<long> inBuffer { get; set; }
        public Solution Other { get; set; }
        public bool Waiting { get; set; }

        private Dictionary<char, long> reg = new Dictionary<char, long>();
        private long Id;

        int pc = 0;
        long last = 0; // part 1
        int sendCount = 0;

        public void Run()
        {

            while(pc>=0 && pc < Data.Count)
            {
                // Move this out of the loop.
                var action = Data[pc].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var instruction = action[0];

                var x = action[1][0];
                var y = 0L;
                if (action.Length == 3)
                {
                    if(!long.TryParse(action[2], out y))
                    {
                        y = getReg(action[2][0]);
                    }
                }


                switch (instruction)
                {
                    case "snd":
                        snd(x);
                        break;
                    case "set":
                        set(x, y);
                        break;
                    case "add":
                        add(x, y);
                        break;
                    case "mul":
                        mul(x, y);
                        break;
                    case "mod":
                        mod(x, y);
                        break;
                    case "rcv":
                        rcv(x);
                        break;
                    case "jgz":
                        jgz(x, y);
                        break;
                }

                //printReg();
                pc++;
            }

        }


        private void printReg()
        {
            Console.WriteLine($"[{Id}]"+ pc+"\t"+string.Join(",", reg));
        }

        private void snd(char r)
        {
            sendCount++;
            long msg = getReg(r);
            outBuffer.Enqueue(msg);
            Console.WriteLine($"[{Id}] ({sendCount})zenden: {msg}  \t {outBuffer.Count}");
            
        }

        private void set(char x, long y)
        {
            setReg(x, y);
        }

        private void add(char x, long y)
        {
            setReg(x, getReg(x) + y);
        }

        private void mul(char x, long y)
        {
            setReg(x, getReg(x) * y);
        }


        private void mod(char x, long y)
        {
            setReg(x, getReg(x) % y);
        }

        private void rcv(char x)
        {
            
            while (inBuffer.Count == 0)
            {
                Console.WriteLine($"[{Id}] Waiting...");
                Waiting = true;
                Thread.Sleep(100);
                if (Other.Waiting && inBuffer.Count == 0)
                {
                    Console.WriteLine($"[{Id}] DEADLOCK {sendCount}");
                    Thread.CurrentThread.Abort();
                }
                    

            }
            
            if (inBuffer.Count()>0)
            {
                Waiting = false;
                var opreation = inBuffer.TryDequeue(out long val);
                Console.WriteLine($"[{Id}] Ontvangen: {val} \t\t {inBuffer.Count}");
                if (!opreation)
                    Debugger.Break();
                setReg(x, val);
            }
            else
            {
                Console.WriteLine("PANIC!");
            }

            // PART 1
            /*
            if (getReg(x) > 0)
            {
                Console.WriteLine($"Recovered {last}");
                pc = int.MaxValue-1; // exit?
                // what to do here?
            }
            */
        }

        private void jgz(char x, long y)
        {
            if (getReg(x) > 0 || x=='1')
            {
                var jmp = y;
                pc += (int)y-1;
            }
        }


        private long getReg(char r)
        {
            if (reg.ContainsKey(r))
                return reg[r];
            else
                return 0;
        }

        private void setReg(char r, long val)
        {
            if (reg.ContainsKey(r))
                reg[r] = val;
            else
                reg.Add(r, val);
        }

    }
}
