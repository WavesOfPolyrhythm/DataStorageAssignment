﻿using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Presentation.Interfaces;
using System.Data;

namespace Presentation.Dialogs;

public class ProjectDialogs(IProjectService projectService, ICustomerService customerService, IEmployeeService employeeService, IServicesService servicesService, IStatusService statusService, IUnitService unitService) : IProjectDialogs
{
    private readonly IProjectService _projectService = projectService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IServicesService _servicesService = servicesService;
    private readonly IStatusService _statusService = statusService;
    private readonly IUnitService _unitService = unitService;

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
        var outputMethods = new OutputMethodsDialog();
        var form = new ProjectRegistrationForm();

        Console.Clear();
        Console.WriteLine("\n--CREATE PROJECT--\n");
        Console.Write("\nEnter Title of the Project: ");
        form.Title = Console.ReadLine()!;
        if (string.IsNullOrWhiteSpace(form.Title))
        {
            outputMethods.OutputDialog("\nTitle cannot be empty. Please try again...");
            return;
        }
        Console.Write("\nWrite a short description of the project: ");
        form.Description = Console.ReadLine()!;
        if (string.IsNullOrWhiteSpace(form.Description))
        {
            outputMethods.OutputDialog("\nDescription cannot be empty. Please try again...");
            return;
        }

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

        if (endDate <= startDate)
        {
            Console.WriteLine("\nEnd Date must be after Start Date. Returning to menu...");
            return;
        }

        Console.Write("\nSelect Customer. Enter Customer Id: \n");
        Console.WriteLine();
        var customers = await _customerService.GetAllCustomersAsync();
        if (customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id}. {customer.CustomerName}");
                Console.WriteLine("-----------");
            }
        }
        else
        {
            Console.WriteLine("\nNo customers available. Please add a customer in 'Manage Customers' menu.");
            return;
        }

        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("\nInvalid Customer Id. Returning to menu...");
            return;
        }

        form.CustomerId = customerId;

        Console.Write("\nSelect Employee for the project. Enter Employee Id: \n");
        var employees = await _employeeService.GetAllEmployeesAsync();
        if (employees.Any())
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id}. {employee.Name} - [{employee.RoleName}]");
                Console.WriteLine("--------------------------");
            }
        }
        else
        {
            Console.WriteLine("\nNo Employees available. Please add a employee in 'Manage Employees' menu.");
        }

        if (!int.TryParse(Console.ReadLine(), out int employeeId))
        {
            Console.WriteLine("\nInvalid Employee Id. Returning to menu...");
            return;
        }

        form.EmployeeId = employeeId;

        Console.Write("\nSelect type of Service for the Project. Enter Id: \n");
        var services = await _servicesService.GetAllServicesAsync();
        if (services.Any())
        {
            foreach (var service in services)
            {
                Console.WriteLine($"{service.Id}. {service.Name} - {service.Price} / {service.UnitName}");
            }
        }
        else
        {
            Console.WriteLine("\nNo Services available. Please add a service in 'Manage Services' menu.");
            return;
        }

        if (!int.TryParse(Console.ReadLine(), out int serviceId))
        {
            Console.WriteLine("\nInvalid Service Id. Returning to menu...");
            return;
        }

        var selectedService = await _servicesService.GetServiceEntityAsync(x => x.Id == serviceId);
        if (selectedService == null || selectedService.Unit == null)
        {
            Console.WriteLine("\nSelected service does not exist. Returning to menu...");
            return;
        }

        form.ServiceId = serviceId;

        Console.WriteLine($"\nYou chose Service '{selectedService.Name}' with Unit: '{selectedService.Unit.Name}' / {selectedService.Price}");

        Console.Write($"\nHow many Units ({selectedService.Unit.Name}) do you want to apply to the Project? ('{selectedService.Unit.Name}' * Service price) ");
        if (!int.TryParse(Console.ReadLine(), out int unitCount) || unitCount <= 0)
        {
            Console.WriteLine("\nInvalid input. Returning to menu...");
            return;
        }
  
        form.Units = unitCount;

        Console.Write("\nSelect current Status for the Project. Enter Id: \n");
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
            Console.WriteLine("\nNo Statuses available. Please add a status in 'Manage Statuses' menu.");
        }
        if (!int.TryParse(Console.ReadLine(), out int statusId))
        {
            Console.WriteLine("\nInvalid Status Id. Returning to menu...");
            return;
        }
        form.StatusId = statusId;

        string answer;
        do
        {
            Console.WriteLine("\n--Would you like to continue and save this project?--\n");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            answer = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(answer))
            {
                outputMethods.OutputDialog("\nAnswer cannot be empty. Please try again...");
                return;
            }

            if (answer == "1")
            {
                var result = await _projectService.CreateProjectAsync(form);
                if (result != null)
                {
                    Console.Clear();
                    Console.WriteLine($"\nProject was successfully created!");
                    DisplayProjectDetails(result);
                }
               
                else
                {
                    Console.WriteLine("\nFailed to create Project.");
                }
                break;
            }
            else if (answer == "2")
            {
                Console.WriteLine("\nReturning back to the menu...");
                return;
            }
            else
            {
                Console.WriteLine("\nInvalid input, try again.");
            }
        }
        while (true);
        
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
                DisplayProjectDetails(project);
            }
        }
        else
        {
            Console.WriteLine("\nNo projects available.");
        }
    }

    public async Task UpdateProjectDialog()
    {
        var outputMethods = new OutputMethodsDialog();
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

        Console.Write("\nEnter new Start Date (yyyy-MM-dd) ");
        if (!DateTime.TryParse(Console.ReadLine(), out var startDate))
        {
            Console.WriteLine("\nInvalid Date. Returning to menu...");
            return;
        }
        Console.Write("\nEnter new End Date (yyyy-MM-dd) ");
        if (!DateTime.TryParse(Console.ReadLine(), out var endDate))
        {
            Console.WriteLine("\nInvalid Date. Returning to menu...");
            return;
        }

        if (endDate <= startDate)
        {
            Console.WriteLine("\nEnd Date must be after Start Date. Returning to menu...");
            return;
        }

        var form = new ProjectUpdateForm
        {
           Id = projectId,
           Title = projectTitle,
           Description = projectDescription,
           StartDate = startDate,
           EndDate = endDate,
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
        var selectedService = await _servicesService.GetServiceEntityAsync(x => x.Id == form.ServiceId);
        if (selectedService == null)
        {
            Console.WriteLine("\nSelected service does not exist. Returning to menu...");
            return;
        }
        Console.WriteLine($"\nYou chose Service '{selectedService.Name}' with Unit: '{selectedService.Unit.Name}' / {selectedService.Price}");

        Console.Write($"\nHow many Units ({selectedService.Unit.Name}) do you want to apply to the Project? ('{selectedService.Unit.Name}' * Service price) ");
        if (!int.TryParse(Console.ReadLine(), out int unitCount) || unitCount <= 0)
        {
            Console.WriteLine("\nInvalid input. Returning to menu...");
            return;
        }
        form.Units = unitCount;

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

        string answer;
        do
        {
            Console.WriteLine("\n--Would you like to continue and save this project?--\n");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            answer = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(answer))
            {
                outputMethods.OutputDialog("\nAnswer cannot be empty. Please try again...");
                return;
            }

            if (answer == "1")
            {
                var result = await _projectService.UpdateProjectAsync(form);
                if (result != null)
                {
                    Console.WriteLine("\nProject successfully updated!");
                    DisplayProjectDetails(result);
                }

                else
                {
                    Console.WriteLine("\nFailed to create Project.");
                }
                break;
            }
            else if (answer == "2")
            {
                Console.WriteLine("\nReturning back to the menu...");
                return;
            }
            else
            {
                Console.WriteLine("\nInvalid input, try again.");
            }
        }
        while (true);
    }

    public async Task DeleteProjectDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--REMOVE PROJECT--\n");
        var projects = await _projectService.GetAllProjectsAsync();
        if (projects.Any())
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"ID:P-{project.Id} Title: {project.Title} ");
                Console.WriteLine("-----------------------------");
            }
        }
        else
        {
            Console.WriteLine("\nNo Projects available at the moment.");
        }

        Console.Write("\nEnter Project ID you want to remove (Enter numbers only): ");
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

    private void DisplayProjectDetails(ProjectModel project)
    {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Id: P-{project.Id}");
        Console.WriteLine($"Title: '{project.Title}'");
        Console.WriteLine($"Description: '{project.Description}'");
        Console.WriteLine($"Start Date: {project.StartDate:yyyy-MM-dd}");
        Console.WriteLine($"End Date: {project.EndDate:yyyy-MM-dd}");
        Console.WriteLine($"Project Manager: {project.ProjectManager} [{project.Role}]");
        Console.WriteLine($"Customer: {project.CustomerName}");
        Console.WriteLine($"Contact Person: {project.CustomerContact}");
        Console.WriteLine($"Service: {project.ServiceName} {project.ServicePrice} / {project.Unit}");
        Console.WriteLine($"Status: {project.StatusName}");
        Console.WriteLine($"Total Price: {project.TotalPrice}kr");
        Console.WriteLine("------------------------");
    }

}
