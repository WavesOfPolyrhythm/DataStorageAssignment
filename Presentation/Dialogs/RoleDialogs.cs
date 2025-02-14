using Business.Interfaces;
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
            Console.WriteLine("=== Manage Roles ===");
            Console.WriteLine("1. Create Role");
            Console.WriteLine("2. View all Roles");
            Console.WriteLine("3. Update Role");
            Console.WriteLine("4. Delete Role");
            Console.WriteLine("0. Back to Main Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Creates Role...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("View Roles...");
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
}
