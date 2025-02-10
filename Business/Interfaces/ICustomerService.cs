using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<CustomerModel> CreateCustomerAsync(CustomerRegistrationForm form);
    Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
}