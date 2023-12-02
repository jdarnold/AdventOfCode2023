using System.Text.RegularExpressions;

internal class Program
{
    private static void Main1(string[] args)
    {

        if (args.Length != 3)
        {
            Console.WriteLine($"Arge len {args.Length} - Need 3 args : red, green, blue");
            return;
        }

        int redCount = int.Parse(args[0]);
        int greenCount = int.Parse(args[1]);
        int blueCOunt = int.Parse(args[2]);
        Console.WriteLine($"Looking for {redCount} red cubes, {greenCount} green cubes and {blueCOunt} blue cubes");

        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            int gameScore = 0;
            foreach (string game in lines)
            {
                Console.WriteLine(game);
                string[] gameData = game.Split(':', ';');
                bool okay = true;
                int gameNumber = 0;
                foreach (string handful in gameData)
                {
                    // Use regular expression to match numbers
                    if (handful.Contains("Game"))
                    { // save game number
                        MatchCollection matches = Regex.Matches(handful, @"\d+");
                        gameNumber = int.Parse(matches[0].Value);
                        Console.Write($"--> Working on Game {gameNumber}");
                    }
                    else
                    { // we have a list of cubes, get cube color and count
                        string[] cubes = handful.Split(',');
                        foreach (string cube in cubes)
                        {
                            //Console.Write($"\ncube = {cube}");
                            MatchCollection matches = Regex.Matches(cube, @"\d+");
                            int cubeCount = int.Parse(matches[0].Value);
                            if ( cube.Contains("red"))
                            {
                                if ( redCount < cubeCount )
                                    okay = false;
                            }
                            else if ( cube.Contains("green"))
                            {
                                if (greenCount < cubeCount )
                                    okay = false;
                            }
                            else if ( cube.Contains("blue"))
                            {
                                if ( blueCOunt < cubeCount )
                                    okay = false;
                            }

                            if (!okay)
                                break;
                        }
                    }
                }
                if (okay)
                {
                    gameScore += gameNumber;
                    Console.WriteLine($" - gamescore {gameScore}");
                }
                else
                {
                    Console.WriteLine(" - handful doesn't count");
                }

            }
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
    private static void Main(string[] args)
    {
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            int gameScore = 0;
            foreach (string game in lines)
            {
                Console.WriteLine(game);
                string[] gameData = game.Split(':', ';');
                int gamePower = 0;
                int maxRed = 0;
                int maxGreen = 0;
                int maxBlue = 0;
                foreach (string handful in gameData)
                {
                    // Use regular expression to match numbers
                    if (!handful.Contains("Game"))
                    { // we have a list of cubes, get cube color and count
                        string[] cubes = handful.Split(',');
                        foreach (string cube in cubes)
                        {
                            //Console.Write($"\ncube = {cube}");
                            MatchCollection matches = Regex.Matches(cube, @"\d+");
                            int cubeCount = int.Parse(matches[0].Value);
                            if (cube.Contains("red"))
                            {
                                if (maxRed < cubeCount)
                                    maxRed = cubeCount;
                            }
                            else if (cube.Contains("green"))
                            {
                                if (maxGreen < cubeCount)
                                    maxGreen = cubeCount;
                            }
                            else if (cube.Contains("blue"))
                            {
                                if (maxBlue < cubeCount)
                                    maxBlue = cubeCount;
                            }
                        }
                    }
                }
                    gameScore += (maxRed * maxGreen * maxBlue);
                    Console.WriteLine($" - gamescore {gameScore}");
            }
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}