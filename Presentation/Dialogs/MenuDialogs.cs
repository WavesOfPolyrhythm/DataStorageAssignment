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
            Console.WriteLine("2. Create Customer");
            Console.WriteLine("3. Create Employee");
            Console.WriteLine("4. Create Customer Contact");
            Console.WriteLine("5. Show all projects");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateProjectDialog();
                    break;
                case "2":
                    await CreateCustomerDialog();
                    break;
                case "3":
                    await CreateEmployeeDialog();
                    break;
                case "4":
                    await CreateCustomerContactDialog();
                    break;
                case "5":
                    await ShowAllProjectsDialog();
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

        /*
        // Skapa kund
        Console.WriteLine("\n-- Create a new customer --");
        var newCustomer = await CreateCustomerDialog();
        if (newCustomer == null)
        {
            Console.WriteLine("Customer creation failed. Returning to menu.");
            return;
        }
        form.CustomerId = newCustomer.Id;
        */

        Console.Write("Enter Employee ID (Project Manager): ");
        form.EmployeeId = int.Parse(Console.ReadLine()!);

        Console.Write("Enter a Customer ID: ");
        form.CustomerId = int.Parse(Console.ReadLine()!);

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

    public async Task<EmployeeModel?> CreateEmployeeDialog()
    {
        var form = new EmployeeRegistrationForm();
        Console.Clear();
        Console.WriteLine("--- Creating New Employee ---");

        Console.Write("Name: ");
        form.Name = Console.ReadLine()!;

        Console.Write("Email: ");
        form.Email = Console.ReadLine()!;
   
        Console.WriteLine("\nAvailable Roles:");
        Console.WriteLine("1 - Project Manager");
        Console.Write("\nEnter Role Id: ");

        if (!int.TryParse(Console.ReadLine(), out int roleId))
        {
            Console.WriteLine("Invalid input. Returning to menu.");
            return null;
        }
        form.RoleId = roleId;

        var result = await _employeeService.CreateEmployeeAsync(form);

        if (result != null)
        {
            Console.WriteLine($"\n Employee created successfully with Id: {result.Id}");
            Console.WriteLine($"Name: {result.Name} | Email: {result.Email}");
            return result;
        }
        else
        {
            Console.WriteLine("\n Failed to create Employee. Email may already exist.");
            return null;
        }
    }

    public async Task<CustomerContactModel?> CreateCustomerContactDialog()
    {
        var form = new CustomerContactRegistrationForm();
        Console.Clear();
        Console.WriteLine("--- Creating New Customer Contact ---");

        var customers = await _customerService.GetAllCustomersAsync();

        Console.WriteLine("\nAvailable Customers:");
        foreach (var customer in customers)
        {
            Console.WriteLine($"Id: {customer.Id} | Name: {customer.CustomerName}");
        }

        Console.Write("\nEnter Customer Id: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("Invalid input. Returning to menu.");
            return null;
        }

        form.CustomerId = customerId;

        Console.Write("Name: ");
        form.Name = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        form.PhoneNumber = Console.ReadLine()!;

        Console.Write("Email: ");
        form.Email = Console.ReadLine()!;

        var result = await _customerContactService.CreateCustomerContactAsync(form);

        if (result != null)
        {
            Console.WriteLine($"\n Customer Contact created successfully with Id: {result.Id}");
            Console.WriteLine($"Name: {result.Name} | Email: {result.Email} | Customer ID: {result.CustomerId}");
            return result;
        }
        else
        {
            Console.WriteLine("\n Failed to create Customer Contact.");
            return null;
        }
    }

    public async Task ShowAllProjectsDialog()
    {
        Console.Clear();
        Console.WriteLine("=== All Projects ===");

        var projects = await _projectService.GetAllProjectsAsync();

        foreach (var project in projects)
        {
            Console.WriteLine($"\nProject ID: P-{project.Id}");
            Console.WriteLine($"Title: {project.Title}");
            Console.WriteLine($"Manager: {project.ProjectManager} - {project.Role}");
            Console.WriteLine($"Start: {project.StartDate:yyyy-MM-dd} | End: {project.EndDate:yyyy-MM-dd}");
            Console.WriteLine($"Customer: {project.CustomerName}");
            Console.WriteLine($"Customer Contact: {project.CustomerContact}");
            Console.WriteLine($"Status: {project.StatusName}");
            Console.WriteLine("----------------------------------------");
        }

    }



}
