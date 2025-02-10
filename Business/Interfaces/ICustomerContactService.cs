using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface ICustomerContactService
{
    Task<CustomerContactModel> CreateCustomerContactAsync(CustomerContactRegistrationForm form);
    Task<IEnumerable<CustomerContactModel>> GetAllCustomerContactsAsync();
}