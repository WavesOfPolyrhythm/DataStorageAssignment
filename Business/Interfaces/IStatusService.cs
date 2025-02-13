using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IStatusService
{
    Task<StatusModel> CreateStatusesAsync(StatusRegistrationForm form);
    Task<IEnumerable<StatusModel>> GetAllStatusesAsync();
}