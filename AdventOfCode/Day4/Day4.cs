using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day4
    {
        string path = "./Day4/CleanningAssignments.txt";
        
        public List<int[]> ParseData()
        {
            List<int[]> fullCoordinates = new List<int[]>();
            foreach (var line in File.ReadAllLines(path))
            {
                List<int> coordinates = new List<int>();
                var data = line.Trim().Split(',').ToList();
                foreach(var item in data)
                {
                    var sections = item.Split('-').ToList();
                    foreach (var s in sections)
                    {
                        coordinates.Add(int.Parse(s));
                    }
                }
                fullCoordinates.Add(coordinates.ToArray());
            }
            return fullCoordinates;
        }

        private bool CompareSections(int[] coors)
        {
            var absMin = Math.Min(coors[0], coors[2]);
            var absMax = Math.Max(coors[1], coors[3]);
            if(absMin >= coors[0] && absMax <= coors[1])
            {
                return true;
            }
            if(absMin >= coors[2] && absMax <= coors[3])
            {
                return true;
            }
            return false;
        }

        private bool CompareOverlap(int[] coors)
        {
            var range1 = Enumerable.Range(coors[0], coors[1] - coors[0]+1);
            var range2 = Enumerable.Range(coors[2], coors[3] - coors[2]+1);
            var intersections = range1.Intersect(range2).ToArray();
            if (intersections.Length > 0) return true;
            return false;
        }

        public void FindOverLaps() //more than 602
        {
            int count = 0;
            foreach(var pair in ParseData())
            {
                if (CompareOverlap(pair))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }
    }
}
