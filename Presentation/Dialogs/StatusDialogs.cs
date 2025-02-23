using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Presentation.Interfaces;
using System.Net.NetworkInformation;

namespace Presentation.Dialogs;

public class StatusDialogs(IStatusService statusService) : IStatusDialogs
{
    private readonly IStatusService _statusService = statusService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n=== MANAGE STATUSES ===");
            Console.WriteLine("1. Create Status");
            Console.WriteLine("2. View all Status");
            Console.WriteLine("3. Update Status");
            Console.WriteLine("4. Delete Status");
            Console.WriteLine("0. Back to Main Menu");

            Console.Write("\nSelect a number of choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateStatusDialog();
                    break;
                case "2":
                    await ViewAllStatusesDialog();
                    break;
                case "3":
                    await UpdateStatusesAsync();
                    break;
                case "4":
                    await DeleteStatusDialog();
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

    public async Task CreateStatusDialog()
    {
        var outputMethods = new OutputMethodsDialog();
        var form = new StatusRegistrationForm();
        Console.Clear();
        Console.WriteLine("\n--CREATING NEW STATUS--");
        Console.WriteLine("Enter name of the Status, Example: 'In progress'");
        form.StatusName = Console.ReadLine()!;
        if (string.IsNullOrWhiteSpace(form.StatusName))
        {
            outputMethods.OutputDialog("\nName cannot be empty. Please try again...");
            return;
        }

        var result = await _statusService.CreateStatusesAsync(form);

        if(result != null)
        {
            Console.WriteLine($"Status was created with name: '{result.StatusName}'.");
        }
        else
        {
            Console.WriteLine("Failed to create new status.");
        }
    }

    public async Task ViewAllStatusesDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--VIEWING ALL STATUSES--\n");

        var statuses = await _statusService.GetAllStatusesAsync();
        if(statuses != null) 
        foreach( var status in statuses)
        {
            Console.WriteLine($"{status.Id}: {status.StatusName}");
        }
        else
        {
            Console.WriteLine("There are no statuses available right now.");
        }
    }

    public async Task UpdateStatusesAsync()
    {
        Console.Clear();
        Console.WriteLine("\n--UPDATE STATUS MENU--\n");
        Console.WriteLine("\nCurrently available Statuses: \n");

        var statuses = await _statusService.GetAllStatusesAsync();
        if (statuses != null)
            foreach (var status in statuses)
            {
                Console.WriteLine($"{status.Id}: {status.StatusName}");
            }
            else
            {
                Console.WriteLine("There are no statuses available right now.");
            }
        Console.Write("\nSelect the Id of the Status you want to update: ");
        
        if (!int.TryParse(Console.ReadLine(), out var statusId))
        {
            Console.Clear();
            Console.WriteLine("Invalid ID. Returning to Status menu...");
            return;
        }

        Console.Write("\nEnter new name for Status - (leave blank to keep current): ");
        var statusName = Console.ReadLine()!;
        var updateStatus = new StatusUpdateForm()
        {
            Id = statusId,
            StatusName = statusName,
        };

        var result = await _statusService.UpdateStatusAsync(updateStatus);
        if (result != null)
        {
            Console.WriteLine($"\nStatus was successfully updated to: '{result.StatusName}'");
        }
        else
        {
            Console.WriteLine("\nFailed to update Status.");
        }
    }

    public async Task DeleteStatusDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--REMOVE STATUS MENU--\n");
   
        var statuses = await _statusService.GetAllStatusesAsync();
        if (statuses != null)
            foreach (var status in statuses)
            {
                Console.WriteLine($"{status.Id}: {status.StatusName}");
            }
        else
        {
            Console.WriteLine("There are no statuses available right now.");
        }

        Console.Write("\nEnter Id of the Status you want to remove: ");
        if (!int.TryParse(Console.ReadLine(), out var statusId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Status menu...");
            return;
        }

        var result = await _statusService.DeleteStatusAsync(statusId);
        if(result != false)
        {
            Console.WriteLine("\nStatus was successfully removed.");
        }
        else
        {
            Console.WriteLine("\nFailed to remove status.");
        }
    }

}
