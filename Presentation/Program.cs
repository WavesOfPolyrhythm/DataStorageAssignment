using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\DataStorage_Assignment\Data\Databases\database_assignment.mdf;Integrated Security=True;Connect Timeout=30"));


serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var projectRepository = serviceProvider.GetRequiredService<IProjectRepository>();

var projects = await projectRepository.GetAllAsync();

Console.WriteLine("==== Projects ====");
foreach (var project in projects)
{
    Console.WriteLine($"ID: P-{project.Id}");
    Console.WriteLine($"Title: {project.Title}");
    Console.WriteLine($"Status: {project.Status?.StatusName}");
    Console.WriteLine($"Customer: {project.Customer?.CustomerName}");
    Console.WriteLine($"Manager: {project.Employee?.Name} ({project.Employee?.Email})");
}

Console.ReadLine();