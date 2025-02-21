using Business.Dtos;
using Business.Interfaces;
using Business.Services;
using Presentation.Interfaces;
using System.Data;

namespace Presentation.Dialogs;

public class CustomerContactDialogs(ICustomerContactService customerContactService, ICustomerService customerService) : ICustomerContactDialogs

{
    private readonly ICustomerContactService _customerContactService = customerContactService;
    private readonly ICustomerService _customerService = customerService;
    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Customer Contacts ===");
            Console.WriteLine("1. Add Customer Contact");
            Console.WriteLine("2. View all Customer Contacts");
            Console.WriteLine("3. Update Customer Contact");
            Console.WriteLine("4. Delete Customer Contact");
            Console.WriteLine("0. Back to Customer Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateCustomerContactDialog();
                    break;
                case "2":
                    await ViewAllCustomerContactsDialog();
                    break;
                case "3":
                    await UpdateCustomerContact();
                    break;
                case "4":
                    await DeleteCustomerContact();
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

    public async Task CreateCustomerContactDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--ADD CUSTOMER CONTACT--\n");

        var form = new CustomerContactRegistrationForm();

        var customers = await _customerService.GetAllCustomersAsync();
        Console.WriteLine();
        if (customers.Any())
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id}. {customer.CustomerName}");
            }
        else
        {
            Console.WriteLine("\nNo available Customers right now.");
            return;
        }

        Console.WriteLine("\nTo add a Customer Contact, enter Customer Id: ");
        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Customer Contact menu...");
            return;
        }

        form.CustomerId = customerId;

        Console.Write("Enter First and Last name: ");
        form.Name = Console.ReadLine()!;
        Console.Write("Enter Phonenumber: ");
        form.PhoneNumber = Console.ReadLine()!;
        Console.Write("Enter Email: ");
        form.Email = Console.ReadLine()!;


        var result = await _customerContactService.CreateCustomerContactAsync(form);
        if (result != null)
        {
            Console.WriteLine($"\nCustomer Contact was successfully created for Customer -{result.CustomerName}-: {result.Name} - [{result.Email}] - {result.PhoneNumber}");
        }
        else
        {
            Console.WriteLine("\nFailed to create Customer Contact.");
        }

    }

    public async Task ViewAllCustomerContactsDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--ALL CUSTOMER CONTACTS--\n");
        var customerContacts = await _customerContactService.GetAllCustomerContactsAsync();
        if (customerContacts.Any())
        {
            foreach (var customerContact in customerContacts)
            {
                Console.WriteLine($"\n{customerContact.CustomerId}. {customerContact.CustomerName}");
                Console.WriteLine($"Contact: {customerContact.Name} - {customerContact.Email} - {customerContact.PhoneNumber}");
                Console.WriteLine("-------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("\nNo Customer Contacts available right now.");
        }
    }

    public async Task UpdateCustomerContact()
    {
        Console.Clear();
        Console.WriteLine("\n--UPDATE CUSTOMER CONTACT--\n");
        var customerContacts = await _customerContactService.GetAllCustomerContactsAsync();
        if (customerContacts.Any())
        {
            foreach (var customerContact in customerContacts)
            {
                Console.WriteLine($"\n-{customerContact.CustomerName}-");
                Console.WriteLine($"Contact: Id-{customerContact.Id}. Name: {customerContact.Name} - Email: {customerContact.Email} - Phone: {customerContact.PhoneNumber}");
                Console.WriteLine("-------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("\nNo Customer Contacts available right now.");
        }

        Console.Write("\nEnter Id of Customer Contact you want to update: ");
        if (!int.TryParse(Console.ReadLine(), out int customerContactId))
        {
            Console.WriteLine("\nInvalid Unit Id. Returning to menu...");
            return;
        }

        Console.Write("\nEnter first Name and last Name of Customer Contact - (leave blank to keep current): ");
        var contactName = Console.ReadLine()!;
        Console.Write("\nEnter Email of Customer Contact - (leave blank to keep current): ");
        var contactEmail = Console.ReadLine()!;
        Console.Write("\nEnter Phone number of Customer Contact - (leave blank to keep current): ");
        var contactPhone = Console.ReadLine()!;
        var contactId = customerContactId;

        Console.WriteLine("\n--Change Customer--\n");
        var customers = await _customerService.GetAllCustomersAsync();
        if (customers != null)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id}. {customer.CustomerName}");
            }
        }
        else
        {
            Console.WriteLine("\nNo Customer found, please add Customer in Customer menu to continue with this action.");
            return;
        }

        int customerId;
        while (true)
        {
            Console.Write("\nEnter new Customer ID for the updated Customer Contact: ");
            if (int.TryParse(Console.ReadLine(), out customerId) && customers.Any(r => r.Id == customerId))
            {
                break;
            }
            Console.WriteLine("\nInvalid Customer Id. Try again!");
        }

        var updateCustomerContact = new CustomerContactUpdateForm
        {
            Id = customerContactId,
            Name = contactName,
            Email = contactEmail,
            PhoneNumber = contactPhone,
        };

        updateCustomerContact.CustomerId = customerId;

        var updatedContact = await _customerContactService.UpdateCustomerContactAsync(updateCustomerContact);
        if (updatedContact != null)
        {
            Console.WriteLine($"\nCustomer Contact successfully updated - Name: {updatedContact.Name} - Email: [{updatedContact.Email}] - Phone: {updatedContact.PhoneNumber}");
        }
        else
        {
            Console.WriteLine("\nFailed to update Customer Contact.");
        }
    }

    public async Task DeleteCustomerContact()
    {
        Console.Clear();
        Console.WriteLine("\n--REMOVE CUSTOMER CONTACT--\n");
        var customerContacts = await _customerContactService.GetAllCustomerContactsAsync();
        if (customerContacts.Any())
        {
            foreach (var customerContact in customerContacts)
            {
                Console.WriteLine($"\n-{customerContact.CustomerName}-");
                Console.WriteLine($"Contact: Id-{customerContact.Id}. Name: {customerContact.Name} - Email: {customerContact.Email} - Phone: {customerContact.PhoneNumber}");
                Console.WriteLine("-------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("\nNo Customer Contacts available right now.");
        }

        Console.Write("\nEnter Id of Customer Contact you want to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int customerContactId))
        {
            Console.WriteLine("\nInvalid Unit Id. Returning to menu...");
            return;
        }

        var result = await _customerContactService.DeleteCustomerContactAsync(customerContactId);
        if (result)
        {
            Console.WriteLine("\nContact was successfully removed from Customer.");
        }
        else
        {
            Console.WriteLine("\nFailed to remove Contact.");
        }
    }
}
