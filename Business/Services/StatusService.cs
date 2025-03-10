﻿using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{
    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusModel> CreateStatusesAsync(StatusRegistrationForm form)
    {
        await _statusRepository.BeginTransactionAsync();
        try
        {
            var existingStatus = await _statusRepository.GetAsync(x => x.StatusName == form.StatusName);
            if (existingStatus != null)
            {
                await _statusRepository.RollbackTransactionAsync();
                return null!;
            }

            var entity = await _statusRepository.CreateAsync(StatusFactory.Create(form));
            if (entity == null)
            {
                await _statusRepository.RollbackTransactionAsync();
                return null!;
            }
            var status = StatusFactory.Create(entity);
            if (status == null)
            {
                await _statusRepository.RollbackTransactionAsync();
                return null!;
            }
            await _statusRepository.CommitTransactionAsync();
            return status;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _statusRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<IEnumerable<StatusModel>> GetAllStatusesAsync()
    {
        var entities = await _statusRepository.GetAllAsync();
        return entities.Select(StatusFactory.Create);
    }

    public async Task<StatusEntity?> GetStatusEntityAsync(Expression<Func<StatusEntity, bool>> expression)
    {
        var status = await _statusRepository.GetAsync(expression);
        return status;
    }

    public async Task<StatusModel?> UpdateStatusAsync(StatusUpdateForm form)
    {
        try
        {
            var existingEntity = await GetStatusEntityAsync(x => x.Id == form.Id);

            if (existingEntity == null)
                return null!;

            var updatedEntity = StatusFactory.Update(form, existingEntity);
            updatedEntity = await _statusRepository.UpdateAsync(x => x.Id == form.Id, updatedEntity);
            if (updatedEntity == null)
                return null!;

            return StatusFactory.Create(updatedEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> DeleteStatusAsync(int id)
    {
        var result = await _statusRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
