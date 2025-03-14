﻿using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Business.Services;

public class UnitService(IUnitRepository unitRepository) : IUnitService
{
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<UnitModel> CreateUnitsAsync(UnitRegistrationForm form)
    {
        await _unitRepository.BeginTransactionAsync();
        try
        {
            var existingUnit = await _unitRepository.GetAsync(x => x.Name == form.Name);
            if (existingUnit != null)
            {
                await _unitRepository.RollbackTransactionAsync();
                return null!;
            }

            var entity = await _unitRepository.CreateAsync(UnitFactory.Create(form));
            if (entity == null)
            {
                await _unitRepository.RollbackTransactionAsync();
                return null!;
            }

            var unit = UnitFactory.Create(entity);
            if (unit == null)
            {
                await _unitRepository.RollbackTransactionAsync();
                return null!;
            }

            await _unitRepository.CommitTransactionAsync();
            return unit;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _unitRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<IEnumerable<UnitModel>> GetAllUnitsAsync()
    {
        var entities = await _unitRepository.GetAllAsync();
        return entities.Select(UnitFactory.Create);
    }

    public async Task<UnitEntity?> GetUnitEntityAsync(Expression<Func<UnitEntity, bool>> expression)
    {
        var unit = await _unitRepository.GetAsync(expression);
        return unit;
    }

    public async Task<UnitModel?> UpdateUnitAsync(UnitUpdateForm form)
    {
        try
        {
            var existingEntity = await GetUnitEntityAsync(x => x.Id == form.Id);

            if (existingEntity == null)
                return null!;


            var updatedEntity = UnitFactory.Update(form, existingEntity);
            updatedEntity = await _unitRepository.UpdateAsync(x => x.Id == form.Id, updatedEntity);

            if (updatedEntity == null)
                return null!;

            return UnitFactory.Create(updatedEntity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }

    }

    public async Task<bool> DeleteUnitAsync(int id)
    {
        var result = await _unitRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
