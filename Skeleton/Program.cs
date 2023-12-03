// Pretty much every AoC project starts with this code, so let's create a template for it.
class Program
{
    static void Main()
    {
        string filePath = "../../../input.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Display the content
            Console.WriteLine("File Content:");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}
