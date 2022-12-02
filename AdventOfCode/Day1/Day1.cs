using System.Linq;
class Day1
{
	private static List<int> ProcessData(string path)
	{
		var RawData = File.ReadAllText(path);

		var ElfValues = RawData.Trim().Split("\n\n").ToList();
		List<int> TotalCaloriesPerElf = new List<int>();
		foreach(var Elf in ElfValues)
		{

			TotalCaloriesPerElf.Add(Elf.Split("\n")
										.Select(int.Parse)
										.Sum());
          
		}
		return TotalCaloriesPerElf;
	}

	public static void FindAnswers()
	{
		var ElvesCaloriesList = ProcessData("./ElfCalorieData.txt");
        Console.WriteLine("The Max Calories are:"+ ElvesCaloriesList.Max().ToString());
		ElvesCaloriesList.Sort((a, b) => b.CompareTo(a));
		Console.WriteLine("Top 3 added together:" + ElvesCaloriesList.Take(3).Sum());
	}
}