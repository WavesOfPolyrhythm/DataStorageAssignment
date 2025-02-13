using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services;

public class ServicesService(IServiceRepository repository) : IServicesService
{
    private readonly IServiceRepository _repository = repository;
}
