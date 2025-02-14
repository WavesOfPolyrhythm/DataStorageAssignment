using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class UnitService(IUnitRepository unitRepository) : IUnitService
{
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<UnitModel> CreateUnitsAsync(UnitRegistrationForm form)
    {
        var existingUnit = await _unitRepository.GetAsync(x => x.Name == form.Name);
        if (existingUnit != null)
            return null!;

        var entity = await _unitRepository.CreateAsync(UnitFactory.Create(form));
        if (entity == null)
            return null!;

        return UnitFactory.Create(entity);
    }

    public async Task<IEnumerable<UnitModel>> GetAllUnitsAsync()
    {
        var entities = await _unitRepository.GetAllAsync();
        return entities.Select(UnitFactory.Create);
    }
}
