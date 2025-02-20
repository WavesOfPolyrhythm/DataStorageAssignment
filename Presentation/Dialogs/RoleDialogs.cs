using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
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
                    await UpdateRoleAsync();
                    break;
                case "4":
                    await DeleteRoleasync();
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

    public async Task UpdateRoleAsync()
    {
        Console.Clear();
        Console.WriteLine("\n--UPDATE ROLE MENU--");
        Console.WriteLine("\nCurrently available Roles: \n");

        var roles = await _roleService.GetAllRolesAsync();
        if (roles.Any())
        {
            foreach(var role in roles)
            {
                Console.WriteLine($"{role.Id}. {role.RoleName}");
            }
        }
        else
        {
            Console.WriteLine("\nNo Roles available right now.");
            return;
        }

        Console.Write("\nEnter the Role Id you want to update: ");

        if (!int.TryParse(Console.ReadLine(), out var roleId))
        {
            Console.Clear();
            Console.WriteLine("Invalid ID. Returning to Status menu...");
            return;
        }

        Console.Write("\nName of the Role - (leave blank to keep current): ");
        var roleName = Console.ReadLine()!;
        var updateRoleName = new RolesUpdateForm
        {
            Id = roleId,
            RoleName = roleName,
        };

        var result = await _roleService.UpdateRolesAsync(updateRoleName);
        if(result != null)
        {
            Console.WriteLine($"\nRole was successfully updated to: '{result.RoleName}'");
        }
        else
        {
            Console.WriteLine("\nFailed to update Role.");
        }
    }

    public async Task DeleteRoleasync()
    {
        Console.Clear();
        Console.WriteLine("\n--REMOVE ROLES MENU--\n");
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
            return;
        }

        Console.Write("\nEnter Id of the Role you want to remove: ");
        if (!int.TryParse(Console.ReadLine(), out var roleId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Role menu...");
            return;
        }

        var result = await _roleService.DeleteRoleAsync(roleId);
        if (result != false)
        {
            Console.WriteLine("\nRole was successfully removed.");
        }
        else
        {
            Console.WriteLine("\nFailed to remove Role.");
        }
    }
   
}
