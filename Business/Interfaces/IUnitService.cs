using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IUnitService
{
    Task<UnitModel> CreateUnitsAsync(UnitRegistrationForm form);
    Task<IEnumerable<UnitModel>> GetAllUnitsAsync();
}