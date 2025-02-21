using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerModel> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var existingCustomer = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName);
        if (existingCustomer != null)
            return null!;

        var entity = await _customerRepository.CreateAsync(CustomerFactory.Create(form));
        var customer = CustomerFactory.Create(entity);
        return customer ?? null!;
    }

    public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.Create);
        return customers;
    }

    public async Task<CustomerEntity?> GetCustomerEntityAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var customer = await _customerRepository.GetAsync(expression);
        return customer;
    }

    public async Task<CustomerModel?> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        try
        {
            var existingEntity = await GetCustomerEntityAsync(x => x.Id == form.Id);

            if (existingEntity == null)
                return null!;

            var updatedEntity = CustomerFactory.Update(form, existingEntity);

            updatedEntity = await _customerRepository.UpdateAsync(x => x.Id == form.Id, updatedEntity);
            if (updatedEntity == null) 
                return null!;

            return CustomerFactory.Create(updatedEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }

    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var result = await _customerRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
