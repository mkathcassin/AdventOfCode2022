using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day6
    {
        static string parseData()
        {
            var dataString = File.ReadAllText("Day6/radioSubroutine.txt");
            return dataString;
        }

        public static void FindStartOfMessage()
        {
           var origString = parseData();
           var charArray= origString.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                var group = charArray.ToList().GetRange(i,4);
                var isUnique = group.Distinct().ToList().Count == group.Count;
                if (isUnique)
                {
                    var position = i + 4;
                    Console.Write("you found it at: "+ position + "combo is:");
                    group.ForEach(g => Console.Write(g));
                    return; // 1908 > x < 19084
                }
            }
           Console.WriteLine("no solution found");
        }

        public static void FindMessage()
        {
            var origString = parseData();
            var charArray = origString.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                var group = charArray.ToList().GetRange(i, 14);
                var isUnique = group.Distinct().ToList().Count == group.Count;
                if (isUnique)
                {
                    var position = i + 14;
                    Console.Write("you found it at: " + position + "combo is:");
                    group.ForEach(g => Console.Write(g));
                    return; 
                }
            }
            Console.WriteLine("no solution found");
        }


    }
}
