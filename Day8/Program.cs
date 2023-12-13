// Pretty much every AoC project starts with this code, so let's create a template for it.
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

partial class Program
{
    class Node
    {
        public Node(string name, string leftNode, string rightNode)
        {
            Debug.WriteLine($"Adding node: {name}, {leftNode}, {rightNode}");  
            Name = name;
            LeftNode = leftNode;
            RightNode = rightNode;
        }

        public string Name { get; set; }
        public string LeftNode { get; set; }
        public string RightNode { get; set; }
        public string Direction( char dir )
        {
            if (dir == 'L')
                return LeftNode;
            else return RightNode;
        }
        public bool IsEndNode() => Name[2] == 'Z';
    }

    static string instructions="";
    static Dictionary<string, Node> nodes = [];


    static long CountStepsToEnd( Node node )
    {
        int steps = 0;
        int direction = 0;
        do
        {
            ++steps;
            node = nodes[node.Direction(instructions[direction])];
            ++direction;
            if (direction >= instructions.Length)
                direction = 0;

        } while (!node.IsEndNode());

        return steps;
    }

    // Function to find GCD of two numbers using Euclidean Algorithm
    static long FindGCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Function to find LCM of two numbers using GCD
    static long FindLCM(long[] numbers)
    {
        if (numbers.Length == 0)
        {
            throw new ArgumentException("At least two numbers are required to find LCM.");
        }

        long lcm = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            long currentNumber = numbers[i];
            long gcd = FindGCD(lcm, currentNumber);

            // Calculate LCM using the formula: LCM(a, b) = |a * b| / GCD(a, b)
            lcm = Math.Abs(lcm * currentNumber) / gcd;
        }

        return lcm;
    }

static void Main1()
    {
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);
            instructions = lines[0];

            foreach (string line in lines.Skip(2))
            {
                string[] data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Match matches = ParseLineRegex().Match(line);

                string name = matches.Groups[1].Value;
                nodes[name] = new Node(name, matches.Groups[2].Value, matches.Groups[3].Value);
            }

            long steps = CountStepsToEnd(nodes["AAA"]);

            Console.WriteLine($"Number of steps: {steps}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
    static void Main()
    {
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            instructions = lines[0];

            foreach (string line in lines.Skip(2))
            {
                string[] data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Match matches = ParseLineRegex2().Match(line);

                string name = matches.Groups[1].Value;
                nodes[name] = new Node(name, matches.Groups[2].Value, matches.Groups[3].Value);
            }

            string[] mapNodes = new string[nodes.Count];

            int sx = 0;
            foreach (var n in nodes)
            {
                if (n.Key[2] == 'A')
                {
                    mapNodes[sx] = n.Key;
                    ++sx;
                }
            }

            long[] lcmNumbers = new long[sx];
            long totalSteps = 1;
            for (int nx = 0; nx < sx; nx++)
            {
                long routeSteps = CountStepsToEnd(nodes[mapNodes[nx]]);
                lcmNumbers[nx] = routeSteps;
                //totalSteps *= routeSteps;
                Console.WriteLine($"Number of steps: {nx} = {routeSteps}");
            }
            totalSteps = FindLCM(lcmNumbers);
            Console.WriteLine($"Number of steps: {totalSteps}");

        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }

    [GeneratedRegex("([A-Z]+)....([A-Z]+)..([A-Z]+).")]
    private static partial Regex ParseLineRegex();
    [GeneratedRegex("([0-9A-Z]+)....([0-9A-Z]+)..([0-9A-Z]+).")]
    private static partial Regex ParseLineRegex2();

}


/*
I've completed "Haunted Wasteland" - Day 8 - Advent of Code 2023 https://adventofcode.com/2023/day/8 #AdventOfCode 

Please join my private leaderboard! The code is 335841-f28d1b57 - it is absolutely not competitive!

https://github.com/jdarnold/AdventOfCode2023
*/
