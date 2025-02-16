using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class RoleDialogs(IRoleService roleService) : IRoleDialogs
{
    private readonly IRoleService _roleService = roleService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n=== Manage Roles ===");
            Console.WriteLine("1. Create Role");
            Console.WriteLine("2. View all Roles");
            Console.WriteLine("3. Update Role");
            Console.WriteLine("4. Delete Role");
            Console.WriteLine("0. Back to Main Menu");

            Console.Write("\nSelect a number of choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateRoleAsync();
                    break;
                case "2":
                    await ViewAllRolesDialog();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Update Role...");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Delete Role...");
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

    public async Task CreateRoleAsync()
    {
        Console.Clear();
        Console.WriteLine("\n--CREATE ROLE MENU--");
        Console.Write("\nEnter name of the Role you want to create: ");

        var form = new RolesRegistrationForm();
        form.RoleName = Console.ReadLine()!;

        var result = await _roleService.CreateRolesAsync(form);

        if (result != null)
        {
            Console.WriteLine($"\nRole was successfully created with name: '{result.RoleName}'");
        }
        else
        {
            Console.WriteLine("\nFailed to create role. Returning to Role Menu.");
        }
    }

    public async Task ViewAllRolesDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--CURRENT ROLES--\n");
        var roles = await _roleService.GetAllRolesAsync();
        if (roles.Any())
        {
            foreach (var role in roles)
            {
                Console.WriteLine($"{role.Id}. {role.RoleName}");
            }
        }
        else
        {
            Console.WriteLine("\nNo Roles available right now.");
        }
    }
   
}
