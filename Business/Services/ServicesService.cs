using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class ServicesService(IServiceRepository serviceRepository, IUnitService unitService) : IServicesService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IUnitService _unitService = unitService;

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

    public async Task<ServiceEntity?> GetServiceEntityAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        var services = await _serviceRepository.GetAsync(expression);
        return services;
    }

    public async Task<ServicesModel?> UpdateServiceAsync(ServicesUpdateForm form)
    {
        try
        {
            var existingEntity = await GetServiceEntityAsync(x => x.Id == form.Id);

            if (existingEntity == null)
                return null!;

            existingEntity.Name = string.IsNullOrWhiteSpace(form.Name) ? existingEntity.Name : form.Name;
            existingEntity.Price = form.Price ?? existingEntity.Price;

            var unit = await _unitService.GetUnitEntityAsync(x => x.Id == form.UnitId);
            if (unit == null)
            {
                Console.WriteLine("\nInvalid Unit ID. Cannot update employee.");
                return null!;
            }

            existingEntity.UnitId = form.UnitId;
            existingEntity.Unit = unit;

            var updatedEntity = await _serviceRepository.UpdateAsync(x => x.Id == form.Id, existingEntity);
            return ServicesFactory.Create(existingEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> DeleteServiceAsync(int id)
    {
        var result = await _serviceRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
