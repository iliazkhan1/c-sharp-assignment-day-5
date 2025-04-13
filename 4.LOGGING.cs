using System;
using Serilog;
using System.IO;  

namespace ErrorTrackingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dynamically get the path to the Downloads folder
            string downloadsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );

            // Set the log file path
            string logFilePath = Path.Combine(downloadsPath, "app_log.txt");

            // Configure Serilog for console and file logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}")
                .WriteTo.File(
                    logFilePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}"
                )
                .CreateLogger();

            Log.Information("Application Started");

            try
            {
                double numerator = GetValidNumber("Enter numerator: ");
                double denominator = GetValidNumber("Enter denominator: ");

                if (denominator == 0)
                {
                    Log.Warning("Input value is zero, potential division by zero warning.");
                }

                double result = numerator / denominator;

                Console.WriteLine($"\nResult: {result}");
                Log.Information("Division operation completed successfully. Result: {Result}", result);
            }
            catch (DivideByZeroException ex)
            {
                Log.Error(ex, "Division by zero exception occurred.");
                Console.WriteLine("Error: Cannot divide by zero.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                Console.WriteLine("An unexpected error occurred.");
            }
            finally
            {
                Log.Information("Application Ended");
                Log.CloseAndFlush();
            }
        }

        // Method to prompt user until a valid number is entered
        static double GetValidNumber(string prompt)
        {
            double number;

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (double.TryParse(input, out number))
                {
                    return number;
                }
                else
                {
                    Log.Warning("Invalid input: {Input}", input);
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }
    }
}
