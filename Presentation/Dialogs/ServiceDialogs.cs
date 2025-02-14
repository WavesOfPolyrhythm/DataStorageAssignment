using Business.Interfaces;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class ServiceDialogs(IServicesService servicesService) : IServiceDialogs
{
    private readonly IServicesService _servicesService = servicesService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Services ===");
            Console.WriteLine("1. Create Service");
            Console.WriteLine("2. View all Services");
            Console.WriteLine("3. Update Service");
            Console.WriteLine("4. Delete Servíce");
            Console.WriteLine("0. Back to Main Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Creates Service...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("View Services...");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Update Service...");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Delete Service...");
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
