using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

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
}
