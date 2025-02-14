using Business.Interfaces;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

internal class CustomerDialogs(ICustomerService customerService, ICustomerContactDialogs customerContactDialogs) : ICustomerDialogs
{
    private readonly ICustomerService _customerService = customerService;
    private readonly ICustomerContactDialogs _customerContactDialogs = customerContactDialogs;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Customers ===");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View all Customers");
            Console.WriteLine("3. Update Customer");
            Console.WriteLine("4. Delete Customer");
            Console.WriteLine("5. Manage Customer Contacts");
            Console.WriteLine("0. Back to Main Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Creates Customer...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("View Customers...");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Update Customer...");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Delete Customer...");
                    break;
                case "5":
                    Console.Clear();
                    await _customerContactDialogs.MenuOptions();
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
