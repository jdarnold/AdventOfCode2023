// Pretty much every AoC project starts with this code, so let's create a template for it.
class Program
{
    class Race
    {
        long raceTime;
        long recordDistance;

        public Race(long raceTime, long recordDistance)
        {
            this.raceTime = raceTime;
            this.recordDistance = recordDistance;
        }

        public long WaysToWin()
        {
            int ways = 0;
            for (int i = 1; i < raceTime; ++i)
            {
                long distance = (raceTime - i) * i;
                if ( distance > recordDistance )
                    ways++;
            }
            return ways;
        }
    }
    static void Main1()
    {
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            string[] timeData = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] distanceData = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            List<Race> raceList = [];
            for (int i = 1; i < timeData.Length; ++i)
            {
                var rt = long.Parse(timeData[i]);
                var rd = long.Parse(distanceData[i]);

                raceList.Add(new Race(rt, rd));
            }

            long[] waysToBeatRecord = new long[raceList.Count];
            for (int i = 0; i < waysToBeatRecord.Length; ++i)
            {
                waysToBeatRecord[i] += raceList[i].WaysToWin();
            }

            long answer = waysToBeatRecord.Aggregate((current, next) => current * next);
            Console.WriteLine($"Ways to win: {answer}");
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

            string[] timeData = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] distanceData = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            timeData[0] = "";
            distanceData[0] = "";

            string actualTime = string.Join("",timeData);
            string actualDistance = string.Join("", distanceData);

            List<Race> raceList = [];
            //for (int i = 1; i < timeData.Length; ++i)
            {
                var rt = long.Parse(actualTime);
                var rd = long.Parse(actualDistance);

                raceList.Add(new Race(rt, rd));
            }

            long[] waysToBeatRecord = new long[raceList.Count];
            for (int i = 0; i < waysToBeatRecord.Length; ++i)
            {
                waysToBeatRecord[i] += raceList[i].WaysToWin();
            }

            var answer = waysToBeatRecord.Aggregate((current, next) => current * next);
            Console.WriteLine($"Ways to win: {answer}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}
