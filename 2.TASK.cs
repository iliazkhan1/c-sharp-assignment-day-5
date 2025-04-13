using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        // List of file paths to read
        string[] filePaths = {
            "C:\\Users\\patha\\Downloads\\application.txt",
            "C:\\Users\\patha\\Downloads\\default.txt",
            "C:\\Users\\patha\\Downloads\\Book1.csv"
        };

        // Create a list of tasks for reading line counts
        List<Task<int>> tasks = new List<Task<int>>();

        foreach (var path in filePaths)
        {
            tasks.Add(CountLinesAsync(path));
        }

        try
        {
            // Wait for all tasks to complete
            int[] lineCounts = await Task.WhenAll(tasks);

            // Sum total lines
            int totalLines = lineCounts.Sum();

            Console.WriteLine($"Total number of lines across all files: {totalLines}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Asynchronous method to count lines in a file
    static async Task<int> CountLinesAsync(string filePath)
    {
        try
        {
            using StreamReader reader = new StreamReader(filePath);
            int lineCount = 0;

            while (await reader.ReadLineAsync() != null)
            {
                lineCount++;
            }

            Console.WriteLine($"{filePath} has {lineCount} lines.");
            return lineCount;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading {filePath}: {ex.Message}");
            return 0; 
        }
    }
}
