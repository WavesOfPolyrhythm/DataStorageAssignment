using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Data;
using System.Linq.Expressions;

namespace Business.Services;

public class CustomerContactService(ICustomerContactRepository customerContactRepository, ICustomerService customerService) : ICustomerContactService
{
    private readonly ICustomerContactRepository _customerContactRepository = customerContactRepository;
    private readonly ICustomerService _customerService = customerService;

    public async Task<CustomerContactModel> CreateCustomerContactAsync(CustomerContactRegistrationForm form)
    {
        await _customerContactRepository.BeginTransactionAsync();
        try
        {
            var existingCustomerContact = await _customerContactRepository.GetAsync(x => x.Email == form.Email);
            if (existingCustomerContact != null)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                return null!;
            }

            var entity = await _customerContactRepository.CreateAsync(CustomerContactFactory.Create(form));
            if (entity == null)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                return null!;
            }

            var customerContact = CustomerContactFactory.Create(entity);
            if (customerContact == null)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                return null!;
            }

            await _customerContactRepository.CommitTransactionAsync();
            return customerContact;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _customerContactRepository.RollbackTransactionAsync();
            return null!;
        }
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
        await _customerContactRepository.BeginTransactionAsync();
        try
        {
            var existingEntity = await GetCustomerContactEntityAsync(x => x.Id == form.Id);
            if (existingEntity == null)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                return null!;
            }

            var customer = await _customerService.GetCustomerEntityAsync(x => x.Id == form.CustomerId);
            if (customer == null)
            {
                Console.WriteLine("\nInvalid Customer ID. Rolling back transaction.");
                await _customerContactRepository.RollbackTransactionAsync();
                return null!;
            }

            var updatedEntity = CustomerContactFactory.Update(form, existingEntity);
            updatedEntity = await _customerContactRepository.UpdateAsync(x => x.Id == form.Id, updatedEntity);
            if (updatedEntity == null)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                return null!;
            }

            var contact = CustomerContactFactory.Create(updatedEntity);
            if (contact == null)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                return null!;
            }

            await _customerContactRepository.CommitTransactionAsync();
            return contact;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating customer contact: {ex.Message}");
            await _customerContactRepository.RollbackTransactionAsync();
            return null!;
        }
    }


    public async Task<bool> DeleteCustomerContactAsync(int id)
    {
        var result = await _customerContactRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
