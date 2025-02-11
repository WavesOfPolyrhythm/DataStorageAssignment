using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;

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
            Console.WriteLine("1. Create Project");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateProjectDialog();
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

    public async Task CreateProjectDialog()
    {
        var form = new ProjectRegistrationForm();
        Console.Clear();
        Console.WriteLine("-- Creating New Project --");

        Console.Write("Project Title: ");
        form.Title = Console.ReadLine()!;

        Console.Write("Project Description: ");
        form.Description = Console.ReadLine()!;

        Console.Write("Start Date (yyyy-MM-dd): ");
        form.StartDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("End Date (yyyy-MM-dd): ");
        form.EndDate = DateTime.Parse(Console.ReadLine()!);

        // Skapa kund
        Console.WriteLine("\n-- Create a new customer --");
        var newCustomer = await CreateCustomerDialog();
        if (newCustomer == null)
        {
            Console.WriteLine("Customer creation failed. Returning to menu.");
            return;
        }
        form.CustomerId = newCustomer.Id;

        Console.Write("Enter Employee ID (Project Manager): ");
        form.EmployeeId = int.Parse(Console.ReadLine()!);

        Console.Write("Enter Service ID: ");
        form.ServiceId = int.Parse(Console.ReadLine()!);

        Console.Write("Enter Status ID (1 - Not Started, 2 - In Progress, 3 - Completed): ");
        form.StatusId = int.Parse(Console.ReadLine()!);

        Console.Write("Enter a unit: ");
        form.Units = int.Parse(Console.ReadLine()!);

        
        Console.Write("Enter Total Price for the project: ");
        form.TotalPrice = decimal.Parse(Console.ReadLine()!);

        var result = await _projectService.CreateProjectAsync(form);

        if (result != null)
        {
            Console.WriteLine($"\n Project created successfully with Id: {result.Id}");
            Console.WriteLine($"Title: {result.Title}");
            Console.WriteLine($"Start Date: {result.StartDate:yyyy-MM-dd} | End Date: {result.EndDate:yyyy-MM-dd}");
        }
        else
        {
            Console.WriteLine("\n Failed to create project.");
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    public async Task<CustomerModel?> CreateCustomerDialog()
    {
        var form = new CustomerRegistrationForm();
        Console.Clear();
        Console.WriteLine("--- Creating New Customer ---");

        Console.Write("Customer Name: ");
        form.CustomerName = Console.ReadLine()!;

        var result = await _customerService.CreateCustomerAsync(form);

        if (result != null)
        {
            Console.WriteLine($"\n Customer created successfully with Id: {result.Id}");
            Console.WriteLine($"Customer Name: {result.CustomerName}");
            return result;
        }
        else
        {
            Console.WriteLine("\n Failed to create customer. A customer with this name may already exist.");
            return null;
        }
    }

}
