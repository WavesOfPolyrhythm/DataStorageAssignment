using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Business.Services;
using Business.Interfaces;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\DataStorage_Assignment\Data\Databases\database_assignment.mdf;Integrated Security=True;Connect Timeout=30"));


serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();

//Service
serviceCollection.AddScoped<IProjectService, ProjectService>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var projectService = serviceProvider.GetRequiredService<IProjectService>();
var projects = await projectService.GetAllProjectsAsync();

Console.WriteLine("==== Projects ====");
foreach (var project in projects)
{
    Console.WriteLine($"ID: P-{project.Id}");
    Console.WriteLine($"Titel: {project.Title}");
    Console.WriteLine($"Beskrivning: {project.Description}");
    Console.WriteLine($"Start: {project.StartDate:yyyy-MM-dd} | Slut: {project.EndDate:yyyy-MM-dd}");
    Console.WriteLine("----------------------------------------");
}

Console.ReadLine();