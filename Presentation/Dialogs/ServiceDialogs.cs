﻿using Business.Dtos;
using Business.Interfaces;
using Presentation.Interfaces;
using System;

namespace Presentation.Dialogs;

public class ServiceDialogs(IServicesService servicesService, IUnitService unitService) : IServiceDialogs
{
    private readonly IServicesService _servicesService = servicesService;
    private readonly IUnitService _unitService = unitService;

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
                    await CreateServicesDialog();
                    break;
                case "2":
                    await ViewAllServicesDialog();
                    break;
                case "3":
                    await UpdateServiceDialog();
                    break;
                case "4":
                    await DeleteServiceDialog();
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

    public async Task CreateServicesDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--CREATE SERVICE--\n");

        var form = new ServicesRegistrationForm();
        Console.Write("Enter name of Service: ");
        form.Name = Console.ReadLine()!;
        Console.Write("Enter Price: ");
        form.Price = decimal.Parse(Console.ReadLine()!);
        Console.WriteLine("\nSelect Unit for the Service: ");
        var units = await _unitService.GetAllUnitsAsync();

        if (units != null)
        {
            foreach (var unit in units)
            {
                Console.WriteLine($"\n{unit.Id}. {unit.Name}");
            }
        }
        else
        {
            Console.WriteLine("\nNo Units available. You need to create a Unit first.");
            return;
        }
        Console.Write("\nEnter Unit ID: ");
        if (!int.TryParse(Console.ReadLine(), out int unitId))
        {
            Console.WriteLine("\nInvalid Unit Id. Returning to menu...");
            return;
        }

        form.UnitId = unitId;

        var result = await _servicesService.CreateServicesAsync(form);

        if (result != null)
        {
            Console.WriteLine($"\nService created successfully, Name: '{result.Name}' - Price: {result.Price} / {result.UnitName}");
        }
        else
        {
            Console.WriteLine("\nFailed to create Service.");
        }
    }

    public async Task ViewAllServicesDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--ALL SERVICES--\n");

        var services = await _servicesService.GetAllServicesAsync();
        if (services.Any())
        {
            foreach (var service in services)
            {
                Console.WriteLine($"{service.Id}. {service.Name} - Price: {service.Price} / {service.UnitName}");
            }
        }
        else
        {
            Console.WriteLine("\nNo available services.");
        }
    }

    public async Task UpdateServiceDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--UPDATE SERVICE--\n");
        var services = await _servicesService.GetAllServicesAsync();
        if(services.Any())
        {
            foreach (var service in services)
            {
                Console.WriteLine($"{service.Id}. {service.Name}");

            }
        }
        else
        {
            Console.WriteLine("No Services available.");
        }

        Console.Write("\nEnter Id of Service you want to update: ");
        if (!int.TryParse(Console.ReadLine(), out var serviceId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Service menu...");
            return;
        }
        Console.Write("\nName of the Role - (leave blank to keep current): ");
        var serviceName = Console.ReadLine()!;
        Console.Write("\nEnter Price of Service - (leave blank to keep current): ");
        var servicePrice = decimal.Parse(Console.ReadLine()!);
     
        var updateService = new ServicesUpdateForm
        {
            Id = serviceId,
            Name = serviceName,
            Price = servicePrice,
        };

        var result = await _servicesService.UpdateServiceAsync(updateService);
        if(result != null)
        {
            Console.WriteLine($"\nService was successfully updated to: {result.Name} - {result.Price} / {result.UnitName}");
        }
        else
        {
            Console.WriteLine("\nFailed to update Service.");
        }
    }

    public async Task DeleteServiceDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--REMOVE SERVICE--\n");
        var services = await _servicesService.GetAllServicesAsync();
        if(services.Any())
        {
            foreach (var service in services)
            {
                Console.WriteLine($"{service.Id}. {service.Name}");
            }
        }
        else
        {
            Console.WriteLine("No available Services. Returning to the menu.");
            return;
        }

        Console.Write("\nEnter Id of Service you want to remove: ");
        if (!int.TryParse(Console.ReadLine(), out var serviceId))
        {
            Console.Clear();
            Console.WriteLine("\nInvalid ID. Returning to Service menu...");
            return;
        }

        var result = await _servicesService.DeleteServiceAsync(serviceId);

        if(result)
        {
            Console.WriteLine($"\nSuccessfully removed Service.");
        }
        else
        {
            Console.WriteLine("Failed to remove service.");
        }

    }

}
