using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class CustomerContactService(ICustomerContactRepository customerContactRepository) : ICustomerContactService
{
    private readonly ICustomerContactRepository _customerContactRepository = customerContactRepository;

    public async Task<CustomerContactModel> CreateCustomerContactAsync(CustomerContactRegistrationForm form)
    {
        var existingCustomerContact = await _customerContactRepository.GetAsync(x => x.Email == form.Email);
        if (existingCustomerContact != null)
            return null!;

        var entity = await _customerContactRepository.CreateAsync(CustomerContactFactory.Create(form));
        var customerContact = CustomerContactFactory.Create(entity);
        return customerContact ?? null!;
    }

    public async Task<IEnumerable<CustomerContactModel>> GetAllCustomerContactsAsync()
    {
        var entities = await _customerContactRepository.GetAllAsync();
        var customerContacts = entities.Select(CustomerContactFactory.Create);
        return customerContacts;
    }

    public async Task<CustomerContactEntity?> GetCustomerContactEntityAsync(Expression<Func<CustomerContactEntity, bool>> expression)
    {
        var customer = await _customerContactRepository.GetAsync(expression);
        return customer;
    }

    public async Task<CustomerContactModel?> UpdateCustomerContactAsync(CustomerContactUpdateForm form)
    {
        try
        {
            var existingEntity = await GetCustomerContactEntityAsync(x => x.Id == form.Id);

            if (existingEntity == null)
                return null!;

            existingEntity.Name = string.IsNullOrWhiteSpace(form.Name) ? existingEntity.Name : form.Name;
            existingEntity.Email = string.IsNullOrWhiteSpace(form.Email) ? existingEntity.Email : form.Email;
            existingEntity.PhoneNumber = string.IsNullOrWhiteSpace(form.PhoneNumber) ? existingEntity.PhoneNumber : form.PhoneNumber;

            var updatedEntity = await _customerContactRepository.UpdateAsync(x => x.Id == form.Id, existingEntity);
            return CustomerContactFactory.Create(updatedEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }

    }

    public async Task<bool> DeleteCustomerContactAsync(int id)
    {
        var result = await _customerContactRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
