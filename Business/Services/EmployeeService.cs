using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<EmployeeModel> CreateEmployeeAsync(EmployeeRegistrationForm form)
    {
        var existingEmployee = await _employeeRepository.GetAsync(x => x.Email == form.Email);
        if (existingEmployee != null)
            return null!;

        var entity = await _employeeRepository.CreateAsync(EmployeeFactory.Create(form));
        var employee = EmployeeFactory.Create(entity);

        return employee ?? null!;
    }

    public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
    {
        var entities = await _employeeRepository.GetAllAsync();
        var employees = entities.Select(EmployeeFactory.Create);
        return employees;
    }
}
