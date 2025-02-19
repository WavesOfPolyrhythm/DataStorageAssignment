using Business.Dtos;
using Business.Interfaces;
using Presentation.Interfaces;
using System.Data;

namespace Presentation.Dialogs;

public class ProjectDialogs(IProjectService projectService, ICustomerService customerService, IEmployeeService employeeService, IServicesService servicesService, IStatusService statusService) : IProjectDialogs
{
    private readonly IProjectService _projectService = projectService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IServicesService _servicesService = servicesService;
    private readonly IStatusService _statusService = statusService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Projects ===");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. View all Projects");
            Console.WriteLine("3. Update Project");
            Console.WriteLine("4. Delete Project");
            Console.WriteLine("0. Back to Main Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateProjectDialog();
                    break;
                case "2":
                    await ViewAllProjectsDialog();
                    break;
                case "3":
                    await UpdateProjectDialog();
                    break;
                case "4":
                    await DeleteProjectDialog();
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

    public async Task CreateProjectDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--CREATE PROJECT--\n");

        var form = new ProjectRegistrationForm();
        Console.Write("\nEnter Title of the Project: ");
        form.Title = Console.ReadLine()!;
        Console.Write("\nWrite a short description of the project: ");
        form.Description = Console.ReadLine()!;
        Console.Write("\nStart Date: YYYY-MM-DD ");
        if (!DateTime.TryParse(Console.ReadLine(), out var startDate))
        {
            Console.WriteLine("\nInvalid Date. Returning to menu...");
            return;
        }
        form.StartDate = startDate;
        Console.Write("\nEnd Date: YYYY-MM-DD ");
        if (!DateTime.TryParse(Console.ReadLine(), out var endDate))
        {
            Console.WriteLine("\nInvalid Date. Returning to menu...");
            return;
        }
        form.EndDate = endDate;

        Console.Write("\nSelect Customer. Enter Customer Id: \n");
        Console.WriteLine();
        var customers = await _customerService.GetAllCustomersAsync();
        if (customers.Any()) 
        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.Id}. {customer.CustomerName}");
            Console.WriteLine("-----------");
        }
        else
        {
            Console.WriteLine("\nNo customers available. Please add a customer in 'Manage Customers' menu.");
        }

        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("\nInvalid Customer Id. Returning to menu...");
            return;
        }

        form.CustomerId = customerId;

        Console.Write("\nSelect Employee for the project. Enter Employee Id: \n");
        Console.WriteLine();
        var employees = await _employeeService.GetAllEmployeesAsync();
        if (employees.Any())
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id}. {employee.Name} - [{employee.RoleName}]");
                Console.WriteLine("--------------------------");
            }
        else
        {
            Console.WriteLine("\nNo Employees available. Please add a employee in 'Manage Employees' menu.");
        }

        if (!int.TryParse(Console.ReadLine(), out int employeeId))
        {
            Console.WriteLine("\nInvalid Customer Id. Returning to menu...");
            return;
        }

        form.EmployeeId = employeeId;

        Console.Write("\nSelect type of Service for the Project. Enter Id: \n");
        Console.WriteLine();
        var services = await _servicesService.GetAllServicesAsync();
        if (services.Any())
            foreach (var service in services)
            {
                Console.WriteLine($"{service.Id}. {service.Name} - {service.Price} / {service.UnitName}");
            }
        else
        {
            Console.WriteLine("\nNo Services available. Please add a service in 'Manage Services' menu.");
        }

        if (!int.TryParse(Console.ReadLine(), out int serviceId))
        {
            Console.WriteLine("\nInvalid Service Id. Returning to menu...");
            return;
        }

        form.ServiceId = serviceId;

        Console.Write("\nSelect current Status for the Project. Enter Id: \n");
        Console.WriteLine();
        var statuses = await _statusService.GetAllStatusesAsync();
        if (statuses.Any())
            foreach (var status in statuses)
            {
                Console.WriteLine($"{status.Id}. {status.StatusName}");
            }
        else
        {
            Console.WriteLine("\nNo Statuses available. Please add a status in 'Manage Statuses' menu.");
        }

        if (!int.TryParse(Console.ReadLine(), out int statusId))
        {
            Console.WriteLine("\nInvalid Status Id. Returning to menu...");
            return;
        }
        form.StatusId = statusId;

        Console.Write("\nEnter Total Price ");
        form.TotalPrice = decimal.Parse(Console.ReadLine()!);

        form = new ProjectRegistrationForm
        {
           Title = form.Title,
           Description = form.Description,
           StartDate = form.StartDate,
           EndDate = form.EndDate,
           CustomerId = form.CustomerId,
           EmployeeId = form.EmployeeId,
           ServiceId = serviceId,
           StatusId = statusId,
           TotalPrice = form.TotalPrice,
           Units = form.Units,
        };

        var result = await _projectService.CreateProjectAsync(form);
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine($"\nProject was successfully created!");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Title: '{result.Title}'");
            Console.WriteLine($"Description: '{result.Description}'");
            Console.WriteLine($"Start Date: {result.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"End Date: {result.EndDate:yyyy-MM-dd}");
            Console.WriteLine($"Total Price: {result.TotalPrice}kr");
            Console.WriteLine($"Project Manager: {result.ProjectManager} [{result.Role}]");
            Console.WriteLine($"Customer: {result.CustomerName}");
            Console.WriteLine($"Contact Person: {result.CustomerContact}");
            Console.WriteLine($"Service: {result.ServiceName} {result.ServicePrice} / {result.Unit}");
            Console.WriteLine($"Status: {result.StatusName}");
            Console.WriteLine("------------------------");
        }
        else
        {
            Console.WriteLine("\nFailed to create Project.");
        }
    }

    public async Task ViewAllProjectsDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--ALL CURRENT PROJECTS--\n");
        var projects = await _projectService.GetAllProjectsAsync();
        if (projects.Any())
        {
            foreach ( var project in projects )
            {
                Console.WriteLine("------------------------");
                Console.WriteLine($"Title: '{project.Title}'");
                Console.WriteLine($"Description: '{project.Description}'");
                Console.WriteLine($"Start Date: {project.StartDate:yyyy-MM-dd}");
                Console.WriteLine($"End Date: {project.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Total Price: {project.TotalPrice}kr");
                Console.WriteLine($"Project Manager: {project.ProjectManager} [{project.Role}]");
                Console.WriteLine($"Customer: {project.CustomerName}");
                Console.WriteLine($"Contact Person: {project.CustomerContact}");
                Console.WriteLine($"Service: {project.ServiceName} {project.ServicePrice} / {project.Unit}");
                Console.WriteLine($"Status: {project.StatusName}");
                Console.WriteLine("------------------------");
            }
        }
    }

    public async Task UpdateProjectDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--UPDATE PROJECT--\n");
        var projects = await _projectService.GetAllProjectsAsync();
        if (projects.Any())
        {
            foreach ( var project in projects )
            {
                Console.WriteLine($"ID:{project.Id} Title: {project.Title} ");
                Console.WriteLine("-----------------------------");
            }
        }
        else
        {
            Console.WriteLine("\nNo Projects available at the moment.");
        }

        Console.Write("\nEnter Project ID to update information: ");
        if (!int.TryParse(Console.ReadLine(), out var projectId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Service menu...");
            return;
        }

        Console.Write("\nEnter new Title - (leave blank to keep current): ");
        var projectTitle = Console.ReadLine()!;
        Console.Write("\nEnter new Description - (leave blank to keep current): ");
        var projectDescription = Console.ReadLine()!;
        Console.Write("\nEnter new Start Date (yyy-MM-dd) ");
        var startDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("\nEnter new End Date (yyy-MM-dd) ");
        var endDate = DateTime.Parse(Console.ReadLine()!);
        Console.Write("\nEnter new Total Price: ");
        var totalPrice = decimal.Parse(Console.ReadLine()!);

        var form = new ProjectUpdateForm
        {
           Id = projectId,
           Title = projectTitle,
           Description = projectDescription,
           StartDate = startDate,
           EndDate = endDate,
           TotalPrice = totalPrice,
        };
        Console.Clear();
        Console.WriteLine("\n--Available Employees--\n");
        var employees = await _employeeService.GetAllEmployeesAsync();
        if (employees.Any())
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id}. {employee.Name} - [ {employee.RoleName} ]");
            }
        }
        else
        {
            Console.WriteLine("\nNo available Employees. Please add employee in 'Manage Employees Menu'");
        }

        int employeeId;
        while (true)
        {
            Console.Write("\nEnter Employee ID for the updated Project: ");
            if (int.TryParse(Console.ReadLine(), out employeeId) && employees.Any(e => e.Id == employeeId))
            {
                break;
            }
            Console.WriteLine("\nInvalid Employee Id. Try again!");
        }

        form.EmployeeId = employeeId;

        Console.WriteLine("\n--Available Customers--\n");
        var customers = await _customerService.GetAllCustomersAsync();
        if (customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id}. {customer.CustomerName}");
            }
        }
        else
        {
            Console.WriteLine("\nNo available Customers. Please add Customer in 'Manage Customers Menu'");
        }

        int customerId;
        while (true)
        {
            Console.Write("\nEnter Customer ID for the updated Project: ");
            if (int.TryParse(Console.ReadLine(), out customerId) && customers.Any(c => c.Id == customerId))
            {
                break;
            }
            Console.WriteLine("\nInvalid Customer Id. Try again!");
        }

        form.CustomerId = customerId;

        Console.WriteLine("\n--Available Services--\n");
        var services = await _servicesService.GetAllServicesAsync();
        if (services.Any())
        {
            foreach (var service in services)
            {
                Console.WriteLine($"{service.Id}. {service.Name}");
            }
        }
        else
        {
            Console.WriteLine("\nNo available Services. Please add Service in 'Manage Services Menu'");
        }

        int serviceId;
        while (true)
        {
            Console.Write("\nEnter Service ID for the updated Project: ");
            if (int.TryParse(Console.ReadLine(), out serviceId) && services.Any(s => s.Id == serviceId))
            {
                break;
            }
            Console.WriteLine("\nInvalid Customer Id. Try again!");
        }

        form.ServiceId = serviceId;

        Console.WriteLine("\n---Statuses---\n");
        var statuses = await _statusService.GetAllStatusesAsync();
        if (statuses.Any())
        {
            foreach (var status in statuses)
            {
                Console.WriteLine($"{status.Id}. {status.StatusName}");
            }
        }
        else
        {
            Console.WriteLine("\nNo available Statuses. Please add Status in 'Manage Statuses Menu'");
        }

        int statusId;
        while (true)
        {
            Console.Write("\nEnter Status ID for the updated Project: ");
            if (int.TryParse(Console.ReadLine(), out statusId) && statuses.Any(s => s.Id == statusId))
            {
                break;
            }
            Console.WriteLine("\nInvalid Status Id. Try again!");
        }

        form.StatusId = statusId;

        var result = await _projectService.UpdateProjectAsync(form);

        if (result != null)
        {
            Console.WriteLine("\nProject successfully updated!");
            Console.WriteLine("------------------------");
            Console.WriteLine($"Title: '{result.Title}'");
            Console.WriteLine($"Description: '{result.Description}'");
            Console.WriteLine($"Start Date: {result.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"End Date: {result.EndDate:yyyy-MM-dd}");
            Console.WriteLine($"Total Price: {result.TotalPrice}kr");
            Console.WriteLine($"Project Manager: {result.ProjectManager} [{result.Role}]");
            Console.WriteLine($"Customer: {result.CustomerName}");
            Console.WriteLine($"Contact Person: {result.CustomerContact}");
            Console.WriteLine($"Service: {result.ServiceName} {result.ServicePrice} / {result.Unit}");
            Console.WriteLine($"Status: {result.StatusName}");
            Console.WriteLine("------------------------");
        }
        else
        {
            Console.WriteLine("\nFailed to update Project");
        }
    }

    public async Task DeleteProjectDialog()
    {
        Console.Clear();
        Console.WriteLine("--REMOVE PROJECT--");
        var projects = await _projectService.GetAllProjectsAsync();
        if (projects.Any())
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"ID:{project.Id} Title: {project.Title} ");
                Console.WriteLine("-----------------------------");
            }
        }
        else
        {
            Console.WriteLine("\nNo Projects available at the moment.");
        }

        Console.Write("\nEnter Project ID you want to remove: ");
        if (!int.TryParse(Console.ReadLine(), out var projectId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Service menu...");
            return;
        }

        var result = await _projectService.DeleteProjectAsync(projectId);

        if (result)
        {
            Console.WriteLine("\nProject was successfully removed!");
        }
        else
        {
            Console.WriteLine("Failed to remove project.");
        }
    }


}
