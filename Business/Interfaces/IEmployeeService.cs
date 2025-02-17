using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeModel> CreateEmployeeAsync(EmployeeRegistrationForm form);
    Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();
    Task<EmployeeEntity?> GetEmployeeEntityAsync(Expression<Func<EmployeeEntity, bool>> expression);
    Task<EmployeeModel?> UpdateEmployeeAsync(EmployeeUpdateForm form);
    Task<bool> DeleteEmployeeAsync(int id);
}