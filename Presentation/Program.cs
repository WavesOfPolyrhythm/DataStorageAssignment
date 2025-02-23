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

serviceCollection.AddScoped<IProjectService, ProjectService>();
serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
serviceCollection.AddScoped<ICustomerService, CustomerService>();
serviceCollection.AddScoped<ICustomerContactService, CustomerContactService>();
serviceCollection.AddScoped<IRoleService, RoleService>();
serviceCollection.AddScoped<IServicesService, ServicesService>();
serviceCollection.AddScoped<IStatusService, StatusService>();
serviceCollection.AddScoped<IUnitService, UnitService>();

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