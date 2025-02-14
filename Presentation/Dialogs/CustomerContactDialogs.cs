using Business.Interfaces;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class CustomerContactDialogs(ICustomerContactService customerContactService) : ICustomerContactDialogs

{
    private readonly ICustomerContactService _customerContactService = customerContactService;
    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Customer Contacts ===");
            Console.WriteLine("1. Create Customer Contact");
            Console.WriteLine("2. View all Customer Contacts");
            Console.WriteLine("3. Update Customer Contact");
            Console.WriteLine("4. Delete Customer Contact");
            Console.WriteLine("0. Back to Customer Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Creating Customer Contact...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Viewing all Customer Contacts...");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Updating Customer Contact...");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Deleting Customer Contact...");
                    break;
                case "0":
                    return; 
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
