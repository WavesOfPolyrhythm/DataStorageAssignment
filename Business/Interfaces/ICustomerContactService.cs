using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface ICustomerContactService
{
    Task<CustomerContactModel> CreateCustomerContactAsync(CustomerContactRegistrationForm form);
    Task<IEnumerable<CustomerContactModel>> GetAllCustomerContactsAsync();
    Task<CustomerContactEntity?> GetCustomerContactEntityAsync(Expression<Func<CustomerContactEntity, bool>> expression);
    Task<CustomerContactModel?> UpdateCustomerContactAsync(CustomerContactUpdateForm form);
    Task<bool> DeleteCustomerContactAsync(int id);
}