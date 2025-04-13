using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }    public double Salary { get; set; }
}


class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>();

        Console.WriteLine("Enter number of employees:");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.WriteLine("Invalid input. Enter a valid positive number:");
        }

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"\nEnter details for Employee #{i + 1}");

            // Name input
            string name;
            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(name))
                    Console.WriteLine("Name cannot be empty.");
            } while (string.IsNullOrWhiteSpace(name));

            // Department input
            string department;
            do
            {
                Console.Write("Department: ");
                department = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(department))
                    Console.WriteLine("Department cannot be empty.");
            } while (string.IsNullOrWhiteSpace(department));

            // Salary input
            double salary;
            Console.Write("Salary: ");
            while (!double.TryParse(Console.ReadLine(), out salary) || salary < 0)
            {
                Console.WriteLine("Invalid salary. Enter a valid positive number:");
            }

            employees.Add(new Employee { Name = name, Department = department, Salary = salary });
        }



        // 1. Find employees in a specific department
        Console.Write("\nEnter department to search: ");
        string searchDept = Console.ReadLine().Trim();

        var filtered = employees.Where(e => e.Department.Equals(searchDept, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine($"\nEmployees in {searchDept} Department:");
        if (!filtered.Any())
        {
            Console.WriteLine("No employees found in this department.");
        }
        else
        {
            foreach (var emp in filtered)
            {
                Console.WriteLine($"{emp.Name} - {emp.Salary}");
            }
        }

        // 2. Average salary
        double averageSalary = employees.Average(e => e.Salary);
        Console.WriteLine($"\nAverage Salary of all employees: {averageSalary}");

        // 3. Sort by name
        Console.WriteLine("\nEmployees sorted by Name:");
        foreach (var emp in employees.OrderBy(e => e.Name))
        {
            Console.WriteLine($"{emp.Name} - {emp.Department} - {emp.Salary}");
        }

        // 4. Sort by salary descending
        Console.WriteLine("\nEmployees sorted by Salary (High to Low):");
        foreach (var emp in employees.OrderByDescending(e => e.Salary))
        {
            Console.WriteLine($"{emp.Name} - {emp.Salary}");
        }

        // 5. Group by department
        var grouped = employees.GroupBy(e => e.Department);
        Console.WriteLine("\nEmployees Grouped by Department:");
        foreach (var group in grouped)
        {
            Console.WriteLine($"\nDepartment: {group.Key}");
            foreach (var emp in group)
            {
                Console.WriteLine($"- {emp.Name} (${emp.Salary})");
            }
        }

        Console.WriteLine("\nPress Enter to exit.");
        Console.ReadLine();
    }
}
