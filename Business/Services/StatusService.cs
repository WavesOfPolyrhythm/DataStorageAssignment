using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusModel> CreateStatusesAsync(StatusRegistrationForm form)
    {
        var existingStatus = await _statusRepository.GetAsync(x => x.StatusName == form.StatusName);
        if (existingStatus != null)
            return null!;

        var entity = await _statusRepository.CreateAsync(StatusFactory.Create(form));
        if (entity == null)
            return null!;

        return StatusFactory.Create(entity);
    }

    public async Task<IEnumerable<StatusModel>> GetAllStatusesAsync()
    {
        var entities = await _statusRepository.GetAllAsync();
        return entities.Select(StatusFactory.Create);
    }

    public async Task<StatusEntity?> GetStatusEntityAsync(Expression<Func<StatusEntity, bool>> expression)
    {
        var status = await _statusRepository.GetAsync(expression);
        return status;
    }

    public async Task<StatusModel?> UpdateStatusAsync(StatusUpdateForm form)
    {
        try
        {
            var existingEntity = await GetStatusEntityAsync(x => x.Id == form.Id);

            if (existingEntity == null)
                return null!;

            existingEntity.StatusName = string.IsNullOrWhiteSpace(form.StatusName) ? existingEntity.StatusName : form.StatusName;

            var updatedEntity = await _statusRepository.UpdateAsync(x => x.Id == form.Id, existingEntity);
            return StatusFactory.Create(existingEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> DeleteStatusAsync(int id)
    {
        var result = await _statusRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
