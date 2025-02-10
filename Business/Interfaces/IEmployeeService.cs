using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeModel> CreateEmployeeAsync(EmployeeRegistrationForm form);
    Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();
}