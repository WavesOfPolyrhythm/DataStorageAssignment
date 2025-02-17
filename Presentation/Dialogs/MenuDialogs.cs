using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class MenuDialogs(IProjectService projectService, IUnitService unitService, IRoleService roleService, IStatusService statusService, IServicesService servicesService, ICustomerService customerService, IEmployeeService employeeService, ICustomerContactDialogs customerContactDialogs) : IMenuDialogs
{
    private readonly IProjectService _projectService = projectService;
    private readonly IUnitService _unitService = unitService;
    private readonly IRoleService _roleService = roleService;
    private readonly IStatusService _statusService = statusService;
    private readonly IServicesService _servicesService = servicesService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly ICustomerContactDialogs _customerContactDialogs = customerContactDialogs;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== MAIN MENU ===\n");
            Console.WriteLine("1. Manage Projects");
            Console.WriteLine("2. Manage Customers");
            Console.WriteLine("3. Manage Employees");
            Console.WriteLine("4. Manage Roles");
            Console.WriteLine("5. Manage Services");
            Console.WriteLine("6. Manage Status");
            Console.WriteLine("7. Manage Units");
            Console.WriteLine("\n0. Exit");

            Console.Write("\nSelect a number of choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    var projectDialogs = new ProjectDialogs(_projectService);
                    await projectDialogs.MenuOptions();
                    break;
                case "2":
                    Console.Clear();
                    var customerDialogs = new CustomerDialogs(_customerService, _customerContactDialogs);
                    await customerDialogs.MenuOptions();
                    break;
                case "3":
                    Console.Clear();
                    var employeeDialogs = new EmployeeDialogs(_employeeService, _roleService);
                    await employeeDialogs.MenuOptions();
                    break;
                case "4":
                    Console.Clear();
                    var roleDialogs = new RoleDialogs(_roleService);
                    await roleDialogs.MenuOptions();
                    break;
                case "5":
                    Console.Clear();
                    Console.Clear();
                    var serviceDialogs = new ServiceDialogs(_servicesService, _unitService);
                    await serviceDialogs.MenuOptions();
                    break;
                case "6":
                    Console.Clear();
                    var statusDialogs = new StatusDialogs(_statusService);
                    await statusDialogs.MenuOptions();
                    break;
                case "7":
                    Console.Clear();
                    var unitDialogs = new UnitDialogs(_unitService);
                    await unitDialogs.MenuOptions();
                    break;
                case "0":
                    Console.WriteLine("\nExiting the application...");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    break;
            }
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
        
    }

}
