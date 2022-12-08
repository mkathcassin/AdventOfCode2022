using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode
{
    internal class Day5
    {
        public static SortedDictionary<int,List<char>> initCrateLocations(string path)
        {
            var crateLocations = new SortedDictionary<int,List<char>>();
            var lines = File.ReadAllLines(path);
            for(int i = 0; i < 8; i++) // 8 rows of crates
            {
                var chars = lines[i].ToCharArray();
                var column = 0;
                for(int j = 1; j < chars.Length; j = j+4) //each letter is 4 chars away
                {
                    
                    if (chars[j] != ' ')
                    {
                        if (!crateLocations.ContainsKey(column))
                        {
                            crateLocations[column] = new List<char>();
                        }
                        crateLocations[column].Add(chars[j]);
                    }
                    column++;

                }

            }
            return crateLocations;
        }

        public static List<int[]> moveCommands(string path)
        {
            var lines = File.ReadAllLines(path);
            var positions = new List<int[]>();
            for (int i = 10; i < lines.Length; i++)
            {
                positions.Add(Regex.Matches(lines[i], @"\d+").Select(m => int.Parse(m.Value)).ToArray());
            }
            return positions;
        }  

        private static SortedDictionary<int, List<char>> moveCrates(SortedDictionary<int, List<char>> CrateLocations, List<int[]> moveCommands)
        {
            var cl = CrateLocations;
            foreach (var command in moveCommands)
            {
                var movingCrates = cl[command[1]-1].GetRange(0, command[0]);
                cl[command[1]-1].RemoveRange(0, command[0]);
                cl[command[2]-1].InsertRange(0, movingCrates);
                DebugCrates(cl);
            }
            return cl;
        }

        public static void GetTopCrates()
        {
            var finalLocations = moveCrates(initCrateLocations("./Day5/CrateData.txt"), moveCommands("./Day5/CrateData.txt"));
            for (int i = 1; i < finalLocations.Count; i++)
            {
                Console.Write(finalLocations[i][0]);
            }
            
        }

        public static void DebugCrates(SortedDictionary<int, List<char>> crates)
        {
            for (int i = 0; i < crates.Count; i++)
            {
                Console.Write((i+1) + ": ");
                foreach (var crate in crates[i])
                {
                    Console.Write(crate);
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------");
        }
    }
}
