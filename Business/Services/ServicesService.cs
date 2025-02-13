using Data.Interfaces;

namespace Business.Services;

public class ServicesService(IServiceRepository repository)
{
    private readonly IServiceRepository _repository = repository;
}
