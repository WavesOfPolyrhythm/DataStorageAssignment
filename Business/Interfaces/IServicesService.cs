using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IServicesService
{
    Task<ServicesModel> CreateServicesAsync(ServicesRegistrationForm form);
    Task<IEnumerable<ServicesModel>> GetAllServicesAsync();
    Task<ServiceEntity?> GetServiceEntityAsync(Expression<Func<ServiceEntity, bool>> expression);
    Task<ServicesModel?> UpdateServiceAsync(ServicesUpdateForm form);
    Task<bool> DeleteServiceAsync(int id);
}