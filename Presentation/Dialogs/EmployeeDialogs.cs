using Business.Dtos;
using Business.Interfaces;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class EmployeeDialogs(IEmployeeService employeeService, IRoleService roleService) : IEmployeeDialogs
{
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IRoleService _roleService = roleService;

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
                    await CreateEmployeeDialog();
                    break;
                case "2":
                    await ViewAllEmployees();
                    break;
                case "3":
                    await UpdateEmployeeDialog();
                    break;
                case "4":
                    await DeleteEmployeeDialog();
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

    public async Task CreateEmployeeDialog()
    {
        var outputMethods = new OutputMethodsDialog();
        var form = new EmployeeRegistrationForm();
        Console.Clear();
        Console.WriteLine("\n--ADD EMPLOYEE--\n");
        Console.Write("Enter first and last Name of Employee: ");
        form.Name = Console.ReadLine()!;
        if (string.IsNullOrWhiteSpace(form.Name))
        {
            outputMethods.OutputDialog("\nName cannot be empty. Please try again...");
            return;
        }
        Console.Write("\nEnter Email of Employee: ");
        form.Email = Console.ReadLine()!;
        if (string.IsNullOrWhiteSpace(form.Email))
        {
            outputMethods.OutputDialog("\nEmail cannot be empty. Please try again...");
            return;
        }
        Console.WriteLine("\nSelect a Role for the Employee.\n");
        var roles = await _roleService.GetAllRolesAsync();
        if (roles != null)
        {
            foreach (var role in roles)
            {
                Console.WriteLine($"{role.Id}. {role.RoleName}");
            }
        }
        else
        {
            Console.WriteLine("No Roles found, please add Roles in Role menu to continue.");
            return;
        }
        Console.Write("\nEnter Role ID: ");
        if (!int.TryParse(Console.ReadLine(), out int roleId))
        {
            Console.WriteLine("\nInvalid Unit Id. Returning to menu...");
            return;
        }

        form.RoleId = roleId;

        var result = await _employeeService.CreateEmployeeAsync(form);
        if (result != null)
        {
            Console.WriteLine($"\nEmployee was successfully added: {result.RoleName} - {result.Name} [ {result.Email} ]");
        }
        else
        {
            Console.WriteLine("\nFailed to add Employee.");
        }
    }

    public async Task ViewAllEmployees()
    {
        Console.Clear();
        Console.WriteLine("\n--ALL EMPLOYEES--\n");
        var employees = await _employeeService.GetAllEmployeesAsync();
        if (employees != null)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id}. -{employee.RoleName}- {employee.Name} [ {employee.Email} ]");
            }
        }
        else
        {
            Console.WriteLine($"No Employees available right now.");
        }

    }
    public async Task UpdateEmployeeDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--UPDATE EMPLOYEE INFORMATION--\n");
        var employees = await _employeeService.GetAllEmployeesAsync();
        if (employees != null)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id}. -{employee.RoleName}- {employee.Name} [ {employee.Email} ]");
            }
        }
        else
        {
            Console.WriteLine($"\nNo Employees available right now.");
        }

        Console.Write("\nEnter Id of Employee you want to update: ");

        if (!int.TryParse(Console.ReadLine(), out var employeeId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Service menu...");
            return;
        }

        Console.Write("\nEnter first and last name of Employee - (leave blank to keep current): ");
        var employeeName = Console.ReadLine()!;
        Console.Write("\nEnter Email of Employee - (leave blank to keep current): ");
        var employeeEmail = Console.ReadLine()!;

        var updateEmployee = new EmployeeUpdateForm
        {
            Id = employeeId,
            Name = employeeName,
            Email = employeeEmail
        };

        Console.WriteLine("\n--Available Roles--\n");
        var roles = await _roleService.GetAllRolesAsync();
        if (roles != null)
        {
            foreach (var role in roles)
            {
                Console.WriteLine($"{role.Id}. {role.RoleName}");
            }
        }
        else
        {
            Console.WriteLine("\nNo Roles found, please add Roles in Role menu to continue with this action.");
            return;
        }

        /// <summary>
        /// Chat GPT helped with generating a while loop, ensuring user can have unlimited retrys selecting a role,
        /// for the updated Employee. Loop continues until a valid roleId is provided, at which point it breaks.
        /// </summary>
        int roleId;
        while(true)
        {
            Console.Write("\nEnter Role ID for the updated Employee: ");
            if (int.TryParse(Console.ReadLine(), out roleId) && roles.Any(r => r.Id == roleId))
            {
                break;
            }
            Console.WriteLine("\nInvalid Role Id. Try again!");
        }

        updateEmployee.RoleId = roleId;

        var result = await _employeeService.UpdateEmployeeAsync(updateEmployee);

        if(result != null)
        {
            Console.WriteLine($"\nSuccessfully updated Employee: -{result.RoleName}- {result.Name} [ {result.Email} ]");
        }
        else
        {
            Console.WriteLine("\nFailed to update Employee information.");
        }
    }

    public async Task DeleteEmployeeDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--REMOVE EMPLOYEE--\n");
        var employees = await _employeeService.GetAllEmployeesAsync();
        if(employees != null)
        {
            foreach(var employee in employees)
            {
                Console.WriteLine($"{employee.Id}. -{employee.RoleName}- {employee.Name} [ {employee.Email} ]");
            }
        }
        else
        {
            Console.WriteLine("\nNo Employees available right now.");
        }

        Console.Write("\nEnter Id of Employee you want to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int employeeId))
        {
            Console.WriteLine("\nInvalid Employee Id. Returning to menu...");
            return;
        }

        var result = await _employeeService.DeleteEmployeeAsync(employeeId);
        if(result)
        {
            Console.WriteLine("\nSuccessfully Removed Employee.");
        }
        else
        {
            Console.WriteLine("\nFailed to remove Employee.");
        }
    }
}
