using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ServicesService(IServiceRepository serviceRepository) : IServicesService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<ServicesModel> CreateServicesAsync(ServicesRegistrationForm form)
    {
        var existingService = await _serviceRepository.GetAsync(x => x.Name == form.Name);
        if (existingService != null)
            return null!;

        var entity = await _serviceRepository.CreateAsync(ServicesFactory.Create(form));
        if (entity == null)
            return null!;

        return ServicesFactory.Create(entity);
    }

    public async Task<IEnumerable<ServicesModel>> GetAllServicesAsync()
    {
        var entities = await _serviceRepository.GetAllAsync();
        return entities.Select(ServicesFactory.Create);
    }
}
