using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

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
}
