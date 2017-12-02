using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Util
{
    static public class Tools
    {
        static public List<string> readFileToList(string filePath)
        {
            return File.ReadAllLines(filePath).ToList();
        }

        /// <summary>
        /// Convert a list of strings to a list of int[].
        /// Delims in string are ' ' (space) '\t' (tab)
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        static public List<int[]> ToListOfIntArray(this List<String> Data)
        {
            return Data
              .Select((x) => { return x.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries); })
              .Select(x =>
              {
                  return Array.ConvertAll(x, int.Parse);
              }).ToList();
        }
        /// <summary>
        /// greatest common divisor (gcd)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        static public int GCD(int a, int b)
        {
            if (a > b) return GCD(a - b, b);
            if (a < b) return GCD(a, b - a);
            return a;
        }
    }
}
