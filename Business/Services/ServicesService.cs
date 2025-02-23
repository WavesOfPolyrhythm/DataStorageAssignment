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
        await _serviceRepository.BeginTransactionAsync();
        try
        {
            var existingService = await _serviceRepository.GetAsync(x => x.Name == form.Name);
            if (existingService != null)
            {
                await _serviceRepository.RollbackTransactionAsync();
                return null!;
            }

            var entity = await _serviceRepository.CreateAsync(ServicesFactory.Create(form));
            if (entity == null)
            {
                await _serviceRepository.RollbackTransactionAsync();
                return null!;
            }

            var service = ServicesFactory.Create(entity);
            if (service == null)
            {
                await _serviceRepository.RollbackTransactionAsync();
                return null!;
            }

            await _serviceRepository.CommitTransactionAsync();
            return service;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _serviceRepository.RollbackTransactionAsync();
            return null!;
        }
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
    await _serviceRepository.BeginTransactionAsync();
    try
    {
        var existingEntity = await GetServiceEntityAsync(x => x.Id == form.Id);
        if (existingEntity == null)
        {
            await _serviceRepository.RollbackTransactionAsync();
            return null!;
        }

        var unit = await _unitService.GetUnitEntityAsync(x => x.Id == form.UnitId);
        if (unit == null)
        {
            Console.WriteLine("\nInvalid Unit ID. Rolling back transaction.");
            await _serviceRepository.RollbackTransactionAsync();
            return null!;
        }

        var updatedEntity = ServicesFactory.Update(form, existingEntity);
        updatedEntity.Unit = unit;
        updatedEntity = await _serviceRepository.UpdateAsync(x => x.Id == form.Id, updatedEntity);
        if (updatedEntity == null)
        {
            await _serviceRepository.RollbackTransactionAsync();
            return null!;
        }

        var service = ServicesFactory.Create(updatedEntity);
        if (service == null)
        {
            await _serviceRepository.RollbackTransactionAsync();
            return null!;
        }

        await _serviceRepository.CommitTransactionAsync();
        return service;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        await _serviceRepository.RollbackTransactionAsync();
        return null!;
    }
}


    public async Task<bool> DeleteServiceAsync(int id)
    {
        var result = await _serviceRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
