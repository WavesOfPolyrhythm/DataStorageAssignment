using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class UnitDialogs(IUnitService unitService) : IUnitDialogs
{
    private readonly IUnitService _unitService = unitService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Units ===");
            Console.WriteLine("1. Create Unit");
            Console.WriteLine("2. View all Units");
            Console.WriteLine("3. Update Unit");
            Console.WriteLine("4. Delete Unit");
            Console.WriteLine("0. Back to Main Menu");

            Console.Write("Select a number of choice: \n");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateUnitDialog();
                    break;
                case "2":
                    await ShowAllUnitsDialog();
                    break;
                case "3":
                    await UpdateUnitDialog();
                    break;
                case "4":
                    await DeleteUnitDialog();
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

    public async Task CreateUnitDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--Creating new Unit--\n");

        var form = new UnitRegistrationForm();
        Console.Write("Unit-Title: ");
        form.Name = Console.ReadLine()!;
        Console.Write("Unit Description: ");
        form.Description = Console.ReadLine()!;

        var result = await _unitService.CreateUnitsAsync(form);

        if (result != null)
        {
            Console.WriteLine($"\nUnit was created with values: {result.Name} - {result.Description}");
        }
        else
        {
            Console.WriteLine("\nFailed to create Unit.");
        }
    }

    public async Task ShowAllUnitsDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--All Units--\n");
        Console.WriteLine();
        var units = await _unitService.GetAllUnitsAsync();
        if(units != null)
        foreach (var unit in units)
        {
            Console.WriteLine($"-{unit.Name}");
            Console.WriteLine($"Description: {unit.Description}");
            Console.WriteLine("-------------------------------");
        }
        else
        {
            Console.WriteLine("There are no available Units right now.");
        }
    }

    public async Task UpdateUnitDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--Update Unit--\n");
        Console.WriteLine("Enter the Unit-Id you want to update below:  ");
        var units = await _unitService.GetAllUnitsAsync();
        foreach(var unit in units)
        {
            Console.WriteLine($"{unit.Id}. {unit.Name}");
        }
        if (!int.TryParse(Console.ReadLine(), out var unitId))
        {
            Console.WriteLine("\nInvalid ID.");
            return;
        }

        Console.Write("\nNew Unit Name (leave blank to keep current): ");
        var unitName = Console.ReadLine()!;
        Console.Write("\nNew Description (leave blank to keep current): ");
        var description = Console.ReadLine()!;

        var updateForm = new UnitUpdateForm
        {
            Id = unitId,
            Name = unitName,
            Description = description,
        };

        var result = await _unitService.UpdateUnitAsync(updateForm);
        if (result != null)
        {
            Console.WriteLine($"\nUnit was successfully updated to: {result.Name}, {result.Description}. ");
        }
        else
        {
            Console.WriteLine("\nFailed to update unit.");
        }
    }

    public async Task DeleteUnitDialog()
    {
        Console.Clear(); 
        Console.WriteLine("\n--Remove unit--\n");
        Console.WriteLine("Enter the Unit-Id you want to remove below:  ");
        var units = await _unitService.GetAllUnitsAsync();
        foreach (var unit in units)
        {
            Console.WriteLine($"{unit.Id}. {unit.Name}");
        }
        if (!int.TryParse(Console.ReadLine(), out var unitId))
        {
            Console.WriteLine("\nInvalid ID.");
            return;
        }

        var result = await _unitService.DeleteUnitAsync(unitId);

        if (result != false)
        {
            Console.WriteLine($"\nUnit was successfully removed.");
        }
        else
        {
            Console.WriteLine("\nFailed to remove unit.");
        }
    }
}

