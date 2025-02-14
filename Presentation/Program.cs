using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Business.Services;
using Business.Interfaces;
using Business.Dtos;
using Presentation.Dialogs;
using Presentation.Interfaces;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\DataStorage_Assignment\Data\Databases\database_assignment.mdf;Integrated Security=True;Connect Timeout=30"));


serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
serviceCollection.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
serviceCollection.AddScoped<IRolesRepository, RolesRepository>();
serviceCollection.AddScoped<IServiceRepository, ServiceRepository>();
serviceCollection.AddScoped<IStatusRepository, StatusRepository>();
serviceCollection.AddScoped<IUnitRepository, UnitRepository>();


//Service
serviceCollection.AddScoped<IProjectService, ProjectService>();
serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
serviceCollection.AddScoped<ICustomerService, CustomerService>();
serviceCollection.AddScoped<ICustomerContactService, CustomerContactService>();
serviceCollection.AddScoped<IRoleService, RoleService>();
serviceCollection.AddScoped<IServicesService, ServicesService>();
serviceCollection.AddScoped<IStatusService, StatusService>();
serviceCollection.AddScoped<IUnitService, UnitService>();

//Menu
serviceCollection.AddScoped<IMenuDialogs, MenuDialogs>();
serviceCollection.AddScoped<IUnitDialogs, UnitDialogs>();
serviceCollection.AddScoped<IRoleDialogs, RoleDialogs>();
serviceCollection.AddScoped<IStatusDialogs, StatusDialogs>();
serviceCollection.AddScoped<IServiceDialogs,  ServiceDialogs>();
serviceCollection.AddScoped<ICustomerDialogs, CustomerDialogs>();
serviceCollection.AddScoped<IEmployeeDialogs, EmployeeDialogs>();
serviceCollection.AddScoped<IProjectDialogs, ProjectDialogs>();
serviceCollection.AddScoped<ICustomerContactDialogs, CustomerContactDialogs>();


var serviceProvider = serviceCollection.BuildServiceProvider();

var menuDialogs = serviceProvider.GetRequiredService<IMenuDialogs>();
await menuDialogs.MenuOptions();




/*
//READ

//CustomerService
var customerService = serviceProvider.GetRequiredService<ICustomerService>();
var customers = await customerService.GetAllCustomersAsync();

Console.WriteLine("==== Customers ====");
foreach (var customer in customers)
{
    Console.WriteLine($"ID: {customer.Id} | Name: {customer.CustomerName} |");
    Console.WriteLine("----------------------------------------");
}
Console.ReadLine();

//CustomerContactService
var customerContactService = serviceProvider.GetRequiredService<ICustomerContactService>();
var customerContacts = await customerContactService.GetAllCustomerContactsAsync();

Console.WriteLine("==== Customer Contacts ====");
foreach (var customerContact in customerContacts)
{
    Console.WriteLine($"ID: {customerContact.Id} | Name: {customerContact.Name} | Email: {customerContact.Email} | Phone number: {customerContact.PhoneNumber} | CustomerId: {customerContact.CustomerId}");
    Console.WriteLine("----------------------------------------");
}
Console.ReadLine();

//EmployeeService
var employeeService = serviceProvider.GetRequiredService<IEmployeeService>();
var employees = await employeeService.GetAllEmployeesAsync();

Console.WriteLine("==== Employees ====");
foreach (var employee in employees)
{
    Console.WriteLine($"ID: {employee.Id} | Name: {employee.Name} | Email: {employee.Email}");
    Console.WriteLine("----------------------------------------");
}
Console.ReadLine();


//ProjectService
var projectService = serviceProvider.GetRequiredService<IProjectService>();
var projects = await projectService.GetAllProjectsAsync();

Console.WriteLine("==== Projects ====");
foreach (var project in projects)
{
    Console.WriteLine($"ID: P-{project.Id}");
    Console.WriteLine($"Title: {project.Title}");
    Console.WriteLine($"Description: {project.Description}");
    Console.WriteLine($"Start: {project.StartDate:yyyy-MM-dd} | End: {project.EndDate:yyyy-MM-dd}");
    Console.WriteLine("----------------------------------------");
}
Console.ReadLine();
*/