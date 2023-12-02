// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main1(string[] args)
    {
        string filePath = "../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);
            int calibrationValue = 0;
            // Display the content
            Console.WriteLine("File Content:");
            foreach (string line in lines)
            {
                Console.Write("Line:");
                string firstDigit = "";
                string secondDigit = "";
                foreach (char c in line)
                {
                    if (char.IsDigit(c))
                    {
                        if (firstDigit.Length == 0)
                        {
                            firstDigit = c.ToString();
                            secondDigit = c.ToString(); 
                        }
                        else
                            secondDigit = c.ToString();
                    }
                }
                calibrationValue += int.Parse(firstDigit + secondDigit);
                Console.WriteLine(calibrationValue);
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
            int calibrationValue = 0;
            // Display the content
            //Console.WriteLine("File Content:");
            int lineNum = 0;
            foreach (string line in lines)
            {
                ++lineNum;
                Console.Write($"Line {lineNum}: ");
                string firstDigit = "";
                string secondDigit = "";
                for (int index = 0; index < line.Length; ++index)
                {
                    char c = line[index];
                    if (char.IsDigit(c))
                    {
                        if (firstDigit.Length == 0)
                        {
                            firstDigit = c.ToString();
                            secondDigit = c.ToString();
                        }
                        else
                            secondDigit = c.ToString();
                    }
                    else
                    {
                        string digit = WordToDigit(line, ref index);
                        if ( digit.Length > 0)
                        {
                            if ( firstDigit.Length == 0 )
                            {
                                firstDigit = digit;
                                secondDigit = firstDigit;
                            }
                            else
                                secondDigit = digit;

                        }
                    }
                }
                Console.WriteLine(firstDigit+ secondDigit);
                calibrationValue += int.Parse(firstDigit + secondDigit);
            }
            Console.WriteLine(calibrationValue);
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }

    private static string[] numberStrings = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    private static string[] digitStrings = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    private static string WordToDigit( string line, ref int offset )
    {
        // Use Zip to iterate over both arrays in parallel
        foreach (var pair in digitStrings.Zip(numberStrings, (num, word) => new { Number = num, Word = word }))
        {
            if ((offset + pair.Word.Length) > line.Length)
                continue;

            string token = line.Substring(offset, pair.Word.Length);
            if (token == pair.Word)
            {
                offset += 1;
                //Console.WriteLine($"Found number {pair.Word}");
                return pair.Number;
                    
            }
        }

        return "";
    }

}