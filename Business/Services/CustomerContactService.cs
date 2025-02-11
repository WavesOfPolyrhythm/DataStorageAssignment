using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

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
}
