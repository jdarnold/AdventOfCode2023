// Pretty much every AoC project starts with this code, so let's create a template for it.
class Program
{
    static void Main1()
    {
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            int totalWorth = 0;
            foreach (string line in lines)
            {
                string[] scratchData = line.Split(':', '|');
                if ( scratchData.Length != 3 )
                        break;

                string[] winningNumbers = scratchData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] cardNumbers = scratchData[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int worth = 0;
                foreach (string cardNumber in cardNumbers)
                {
                    if (winningNumbers.Contains(cardNumber))
                    { // we have a winnah!
                        if (worth == 0)
                            worth = 1;
                        else
                            worth *= 2;
                    }
                }

                Console.WriteLine($"card is worth {worth}");
                totalWorth += worth;

            }
            Console.WriteLine($"total points: {totalWorth}");
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

            int totalScratchCards = 0;
            int[] scratchcardCount = new int[lines.Length];
            foreach (string line in lines)
            {
                string[] scratchData = line.Split(':', '|');
                if (scratchData.Length != 3)
                    break;
                int cardNumber = int.Parse(scratchData[0].Substring(5));
                string[] winningNumbers = scratchData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] scratchNumbers = scratchData[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int matchingNumbers = 0;
                foreach (string scratchNumber in scratchNumbers)
                {
                    if (winningNumbers.Contains(scratchNumber))
                    { // we have a winnah!
                        matchingNumbers++;
                    }
                }
                int currentCardCount = scratchcardCount[cardNumber - 1] + 1;
                Console.WriteLine($"card {cardNumber} has matching {matchingNumbers}");
                for (int sc = cardNumber; sc < cardNumber+matchingNumbers; ++sc)
                {
                    scratchcardCount[sc] += currentCardCount;
               
                }

            }

            Console.WriteLine($"total points: {scratchcardCount.Sum()+scratchcardCount.Length}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }

}
