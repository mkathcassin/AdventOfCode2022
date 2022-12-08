using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day3
    {
        private string path = "./Day3/SackContents.txt";
        private string test = "abcdfABCD";
        public List<(string, string)> ParseBagsCompartments()
        {
            var RawData =  File.ReadAllLines(path).ToList();
            List<(string, string)> Bags = new List<(string, string)>();
            foreach (var Bag in RawData)
            {
                Bags.Add((Bag.Substring(0,(Bag.Length/2)), Bag.Substring(Bag.Length / 2)));
            }
            return Bags;
        }

        private char CompareCompartments(string Comp1, string Comp2)
        {
            foreach(var item in Comp1.ToCharArray())
            {
                foreach(var otherItem in Comp2.ToCharArray()){
                    if (item.Equals(otherItem))
                    {
                        return otherItem;
                    }
                }
            }
            return ' ';
        }

        private char CompareGroups(string[] group)
        {
            foreach(var character in group[0].ToCharArray())
            {
                if (group[1].Contains(character) && group[2].Contains(character))
                {
                    return character;
                }
            }
            return ' ';
        }

        public void RunComparison()
        {
            var data = File.ReadAllLines(path).ToList();
            List<char> matchingChars = new List<char>();
            var componentTotal = 0;
            foreach(var bag in ParseBagsCompartments())
            {
                matchingChars.Add(CompareCompartments(bag.Item1, bag.Item2));
                componentTotal = componentTotal + CharToInt(matchingChars.Last());
            }
            var GroupTotal = 0;
            for (int i = 0; i < data.Count; i= i+3)
            {
                var c = CompareGroups(data.GetRange(i, 3).ToArray());
                GroupTotal = GroupTotal + CharToInt(c);
            }
            Console.WriteLine(GroupTotal);
        }

        private int CharToInt(char c)
        {
            if (c == ' ') return 0;
            if (char.IsUpper(c))
            {
                return (char.ToUpper(c) - 64)+26;
            }
            return char.ToUpper(c) - 64;
        }
    }
}
