using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Directory
    {
        public string name = "";
        public Directory? parent;
        public List<Directory>? subDir;
        public List<KeyValuePair<string, int>>? files;
        public int dirValue;
    }
    internal class Day7
    {
        public static List<string> parseData()
        {
            var commands = File.ReadAllLines("Day7/terminalCommands.txt").ToList();
            var Debug = File.ReadAllLines("Day7/Sample.txt").ToList();
            return commands;
        }

        public static Directory createDirectoryStructure()
        {
            var data = parseData();
            var mainDirectory = new Directory()
            {
                name = "/",
                subDir = new List<Directory>(),
                files = new List<KeyValuePair<string, int>>()
            };

            var currentDirectory = mainDirectory;
            for(int i = 1; i < data.Count; i++)
            {

                if (data[i].Contains("$ cd"))
                {
                    if (data[i].Contains("..") && currentDirectory.parent != null)
                    {
                        currentDirectory = currentDirectory.parent;

                    }
                    else
                    {
                        var dirName = data[i].Substring(5);
                        currentDirectory = currentDirectory.subDir.Find(dir => dir.name == dirName);

                    }
                                      
                }
                if (data[i].Contains("$ ls"))
                {
                    var startCommandindex = i + 1;

                    //var endCommandindex = data.FindIndex(startCommandindex, d => d.Contains("$"));
                    //var numOfCommands = endCommandindex != -1 ? endCommandindex - startCommandindex : 1;
                    var numOfCommands = GetDataForDir(data.GetRange(i+1, data.Count - (i+1)));
                    var nextChangeOfDir = data.GetRange(startCommandindex, numOfCommands);
                    GetDirData(nextChangeOfDir, currentDirectory);
                    i = i + numOfCommands;
                }

            }
            return mainDirectory;
        }

        private static int GetDataForDir(List<string> data)
        {
            int count = 0;
            foreach(var info in data)
            {
                if (info.Contains("$"))
                {
                    return count;
                }
                count++;
            }
            return count;
        }

        public static void AddDirValueUp(Directory dir, int valueToAdd)
        {
            dir.dirValue = dir.dirValue + valueToAdd;
            Console.WriteLine(valueToAdd + " is being added to : "+ dir.name);
            if(dir.parent != null)
            {
                AddDirValueUp(dir.parent, valueToAdd);
            }
            
        }
        public static void FindDirectoryOver(int value)
        {
            var mainDir = createDirectoryStructure();
            GetDirValues(mainDir, value);
            Console.WriteLine(totalValue);
        }

        public static void whatToDelete()
        {
            var maindir = createDirectoryStructure();
            var totalCapacity = 70000000;
            var neededCapacity = 30000000;
            var freeSpace = totalCapacity - maindir.dirValue;
            var neededSpace = neededCapacity - freeSpace;
            GetDirValues(maindir, neededSpace);
            DeleteAble.Sort();
            Console.WriteLine(DeleteAble[0]);
            
        }
        private static int totalValue = 0;
        private static List<int> DeleteAble = new List<int>();
        private static void GetDirValues(Directory dir, int value)
        {
            if(dir.dirValue > value)
            {
                DeleteAble.Add(dir.dirValue);
            }
            if(dir.subDir!= null)
            {
                foreach (var d in dir.subDir)
                {
                    GetDirValues(d, value);
                }
            }
        }


        private static void GetDirData(List<string> commands, Directory currentDirectory)
        {
            if(currentDirectory == null)
            {
                throw new Exception("null directory");
            }
            foreach(var cmd in commands)
            {
                if (cmd.Contains("dir"))
                {
                    var newDir = new Directory()
                    {
                        name = cmd.Substring(4),
                        parent = currentDirectory,
                        subDir = new List<Directory>(),
                        files = new List<KeyValuePair<string, int>>()
                    };
                    if (currentDirectory.subDir == null)
                    {
                        currentDirectory.subDir = new List<Directory>();
                        currentDirectory.subDir.Add(newDir);
                    }
                    if (currentDirectory.subDir.Find(dir => dir.name == newDir.name) == null)
                    {
                        currentDirectory.subDir.Add(newDir);
                    }
                    Console.WriteLine();
                }
                else
                {
                    var fileValue = int.Parse(Regex.Match(cmd, @"(\d+)").Value);
                    var fileName = cmd.Substring(fileValue.ToString().Length + 1);
                    var fileData = new KeyValuePair<string, int>(fileName, fileValue);
                    Console.WriteLine("this file is: " + fileName + " with: " + fileValue );
                    AddDirValueUp(currentDirectory, fileValue);
                    currentDirectory.files.Add(fileData);
                }
            }
        }


    }
}
