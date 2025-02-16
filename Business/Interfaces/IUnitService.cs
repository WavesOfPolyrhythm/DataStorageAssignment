using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface IUnitService
{
    Task<UnitModel> CreateUnitsAsync(UnitRegistrationForm form);
    Task<IEnumerable<UnitModel>> GetAllUnitsAsync();
    Task<UnitEntity?> GetUnitEntityAsync(Expression<Func<UnitEntity, bool>> expression);
    Task<UnitModel?> UpdateUnitAsync(UnitUpdateForm form);
    Task<bool> DeleteUnitAsync(int id);
}