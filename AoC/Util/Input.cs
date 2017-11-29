using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Util
{
    static public class Input
    {
        static public List<string> readFileToList(string filePath)
        {
            return File.ReadAllLines(filePath).ToList();
        }
    }
}
