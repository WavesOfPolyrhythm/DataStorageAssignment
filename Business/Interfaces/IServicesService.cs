using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IServicesService
{
    Task<ServicesModel> CreateServicesAsync(ServicesRegistrationForm form);
    Task<IEnumerable<ServicesModel>> GetAllServicesAsync();
}