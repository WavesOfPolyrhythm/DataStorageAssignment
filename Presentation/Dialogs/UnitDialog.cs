using Business.Interfaces;
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

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Creates Unit...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("View Unit...");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Update all Units...");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Delete Unit...");
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
