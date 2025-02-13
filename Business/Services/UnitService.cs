using Data.Interfaces;

namespace Business.Services;

public class UnitService(IUnitRepository unitRepository)
{
    private readonly IUnitRepository _unitRepository = unitRepository;
}
