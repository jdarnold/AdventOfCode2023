// Pretty much every AoC project starts with this code, so let's create a template for it.
class Program
{
    static void Main()
    {
        string filePath = "../../../input_test.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}

/*
I've completed "" - Day  - Advent of Code 2023 https://adventofcode.com/2023/day/ #AdventOfCode 

Please join my private leaderboard! The code is 335841-f28d1b57 - it is absolutely not competitive!

https://github.com/jdarnold/AdventOfCode2023
*/
