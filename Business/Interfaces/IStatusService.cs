using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IStatusService
{
    Task<StatusModel> CreateStatusesAsync(StatusRegistrationForm form);
    Task<IEnumerable<StatusModel>> GetAllStatusesAsync();
    Task<StatusEntity?> GetStatusEntityAsync(Expression<Func<StatusEntity, bool>> expression);
    Task<StatusModel?> UpdateStatusAsync(StatusUpdateForm form);
    Task<bool> DeleteStatusAsync(int id);
}