using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class MenuDialogs(IProjectService projectService, ICustomerContactService customerContactService, IEmployeeService employeeService, ICustomerService customerService) : IMenuDialogs
{
    private readonly IProjectService _projectService = projectService;
    private readonly ICustomerContactService _customerContactService = customerContactService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly ICustomerService _customerService = customerService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Manage Projects");
            Console.WriteLine("2. Manage Customers");
            Console.WriteLine("3. Manage Employees");
            Console.WriteLine("4. Manage Roles");
            Console.WriteLine("5. Manage Services");
            Console.WriteLine("6. Manage Status");
            Console.WriteLine("7. Manage Units");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Manage Projects...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Manage Customers...");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Manage Employees..");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Mange Roles");
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Manage Services");
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Manage Status...");
                    break;
                case "7":
                    Console.Clear();
                    Console.WriteLine("Manage Units");
                    break;
                case "0":
                    Console.WriteLine("Exiting the application...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

    }

}
