using Business.Interfaces;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class StatusDialogs(IStatusService statusService) : IStatusDialogs
{
    private readonly IStatusService _statusService = statusService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Status ===");
            Console.WriteLine("1. Create Status");
            Console.WriteLine("2. View all Status");
            Console.WriteLine("3. Update Status");
            Console.WriteLine("4. Delete Status");
            Console.WriteLine("0. Back to Main Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Creates Status...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("View Status...");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Update all Status...");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Delete Status...");
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
