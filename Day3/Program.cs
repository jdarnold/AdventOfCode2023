using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

partial class Program
{
    struct enginePart
    {
        public int x;
        public int y;
        public int length;
        public int partNumber;
        /// <summary>
        /// Check if the symbol is "near" it
        /// </summary>
        /// <param name="sx">Symbol's X Position</param>
        /// <param name="sy">Symbol's Y Position</param>
        /// 
        /// <returns>true if the part number is adjacent (including diagonally) to the symbol</returns>
        public readonly bool Nearby(int sx, int sy)
        {
            if ( sy <= (y-2) || sy >= (y+2) )
            { return false; }

            List<int> sequentialList = Enumerable.Range(x-1, length+2).ToList();
            if ( sequentialList.Contains(sx))
            {
                return true;
            }

            return false;
        }
    };

    static void Main1()
    {
        string filePath = "../../../input_test1.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);
            List<enginePart> parts = new();
            List<Point> schematic = new();
            int y = 0;
            
            foreach (string line in lines)
            {
                // Use regular expression to match numbers
                MatchCollection matches = MyNumberRegex().Matches(line);

                // Iterate over matches
                foreach (Match match in matches.Cast<Match>())
                {
                    var part = new enginePart
                    {
                        partNumber = int.Parse(match.Value),
                        y = y,
                        x = match.Index,
                        length = match.Length
                    };
                    parts.Add(part);
                    Console.WriteLine($"Adding part at {part.y},{part.x}");
                }
                // Define the regex pattern to get symbols
                matches = MySymbolRegex().Matches(line);

                // Iterate over matches
                foreach (Match match in matches.Cast<Match>())
                {
                    schematic.Add(new Point { X = match.Index, Y = y });
                }

                ++y;
            }

            int partsTotal = 0;
            foreach ( var point in schematic )
            {
                Console.WriteLine($"Checking symbol at {point.Y},{point.X}");
                foreach (var part in parts)
                {
                    if (part.Nearby(point.X, point.Y))
                    {
                        partsTotal += part.partNumber;
                        Console.WriteLine($"Found part at {part.y} {part.x}");
                    }


                }

            }
            Console.WriteLine($"Parts sum: {partsTotal}");
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
            List<enginePart> parts = new();
            List<Point> schematic = new();
            int y = 0;

            foreach (string line in lines)
            {
                // Use regular expression to match numbers
                MatchCollection matches = MyNumberRegex().Matches(line);

                // Iterate over matches
                foreach (Match match in matches.Cast<Match>())
                {
                    var part = new enginePart
                    {
                        partNumber = int.Parse(match.Value),
                        y = y,
                        x = match.Index,
                        length = match.Length
                    };
                    parts.Add(part);
                    Console.WriteLine($"Adding part at {part.y},{part.x}");
                }
                // Define the regex pattern to get symbols
                matches = MyGearRegex().Matches(line);

                // Iterate over matches
                foreach (Match match in matches.Cast<Match>())
                {
                    schematic.Add(new Point { X = match.Index, Y = y });
                }

                ++y;
            }

            int gearRatioTotal = 0;
            foreach (var point in schematic)
            {
                Console.WriteLine($"Checking gear at {point.Y},{point.X}");
                List<enginePart> gearParts = [];
                foreach (var part in parts)
                {
                    if (part.Nearby(point.X, point.Y))
                    {
                        gearParts.Add(part);
                        Console.WriteLine($"Found part at {part.y} {part.x}");
                    }
                }

                if ( gearParts.Count == 2 )
                { // if exactly 2 parts near a gear symbol, we have its ratio
                    gearRatioTotal += (gearParts[0].partNumber * gearParts[1].partNumber);
                }

            }
            Console.WriteLine($"Parts sum: {gearRatioTotal}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex MyNumberRegex();
    [GeneratedRegex(@"[^a-zA-Z0-9.]")]
    private static partial Regex MySymbolRegex();
    [GeneratedRegex(@"\*")]
    private static partial Regex MyGearRegex();
}
