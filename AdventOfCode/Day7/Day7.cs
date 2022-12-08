using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Directory
    {
        List<Directory>? subDir;
        List<KeyValuePair<string, int>>? files;
    }
    internal class Day7
    {
        void parseData()
        {
            var commands = File.ReadAllLines("Day7/terminalCommands.txt");
        }
    }
}
