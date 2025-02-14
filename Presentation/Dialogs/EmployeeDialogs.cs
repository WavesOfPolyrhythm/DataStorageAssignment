using Business.Interfaces;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class EmployeeDialogs(IEmployeeService employeeService) : IEmployeeDialogs
{
    private readonly IEmployeeService _employeeService = employeeService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Employees ===");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. View all Employees");
            Console.WriteLine("3. Update Employee");
            Console.WriteLine("4. Remove Employee");
            Console.WriteLine("0. Back to Main Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Adding Employee...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("View Employees...");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Update Employee...");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Remove Employee...");
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Exit to main menu...");
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
