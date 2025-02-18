using Business.Dtos;
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
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. View all Customers");
            Console.WriteLine("3. Update Customer");
            Console.WriteLine("4. Remove Customer");
            Console.WriteLine("5. Manage Customer Contacts");
            Console.WriteLine("0. Back to Main Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateCustomerDialog();
                    break;
                case "2":
                    await ViewAllCustomerDialog();
                    break;
                case "3":
                    await UpdateCustomerDialog();
                    break;
                case "4":
                    await DeleteCustomerDialog();
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

    public async Task CreateCustomerDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--ADD CUSTOMER--\n");
        var form = new CustomerRegistrationForm();

        Console.Write("Enter Customer Company Name: ");
        form.CustomerName = Console.ReadLine()!;

        var result = await _customerService.CreateCustomerAsync(form);
        if (result != null)
        {
            Console.WriteLine($"\nCustomer was added: '{result.CustomerName}'");
        }
        else
        {
            Console.WriteLine("Failed to add Customer.");
        }
    }

    public async Task ViewAllCustomerDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--ALL CUSTOMERS--\n");
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
            Console.WriteLine("\nNo Customers available right now.");
        }
    }

    public async Task UpdateCustomerDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--UPDATE CUSTOMER--\n");
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
            Console.WriteLine("\nNo Customers available right now.");
        }

        Console.Write("\nEnter Customer Id you want to update: ");
        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Customer menu...");
            return;
        }

        Console.Write("\nEnter new Company name of Customer - (leave blank to keep current): ");
        var customerName = Console.ReadLine()!;

        var updateCustomer = new CustomerUpdateForm
        {
            Id = customerId,
            CustomerName = customerName,
        };

        var result = await _customerService.UpdateCustomerAsync(updateCustomer);

        if(result != null)
        {
            Console.WriteLine($"\nCustomer was successfully updated: '{result.CustomerName}'");
        }
        else
        {
            Console.WriteLine("\nFailed to update Customer.");
        }
    }

    public async Task DeleteCustomerDialog()
    {
        Console.Clear(); 
        Console.WriteLine("\n--REMOVE CUSTOMER--\n");
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
            Console.WriteLine("\nNo Customers available right now. Returning to menu");
            return;
        }

        Console.Write("\nEnter Id of Customer you want to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int customerId))
        {
            Console.WriteLine("\nInvalid Customer Id. Returning to menu...");
            return;
        }

        var result = await _customerService.DeleteCustomerAsync(customerId);

        if(result)
        {
            Console.WriteLine("\nCustomer was successfully removed.");
        }
        else
        {
            Console.WriteLine("\nFailed to remove customer.");
        }
    }
}
