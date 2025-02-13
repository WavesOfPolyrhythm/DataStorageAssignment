using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services;

public class UnitService(IUnitRepository unitRepository) : IUnitService
{
    private readonly IUnitRepository _unitRepository = unitRepository;
}
