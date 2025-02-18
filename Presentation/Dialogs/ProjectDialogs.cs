using Business.Interfaces;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class ProjectDialogs(IProjectService projectService) : IProjectDialogs
{
    private readonly IProjectService _projectService = projectService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Manage Projects ===");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. View all Projects");
            Console.WriteLine("3. Update Project");
            Console.WriteLine("4. Delete Project");
            Console.WriteLine("0. Back to Main Menu");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Creates Projects...");
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("View Projects...");
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Update Projects...");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Delete Project...");
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

    public async Task CreateProjectDialog()
    {
        Console.Clear();
        Console.WriteLine("\n--CREATE PROJECT--\n");
    }
}
