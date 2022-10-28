using AutoMapper;
using Courses.Data.Contexts;
using Courses.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Courses.Data.Services;

public class DbService : IDbService
{
    private readonly CourseContext _db;
    private readonly IMapper _mapper;

    public DbService(CourseContext db, IMapper mapper) => (_db, _mapper) = (db, mapper);

    public async Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class, IEntity where TDto : class
    {
        var entities = await _db.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }
    private async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity =>
        await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
    public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity where TDto : class
    {
        var entity = await SingleAsync(expression);
        return _mapper.Map<TDto>(entity);
    }
    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, IEntity =>
        await _db.Set<TEntity>().AnyAsync(expression);
    public async Task<bool> SaveChangesAsync() => await _db.SaveChangesAsync() >= 0;
    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _db.Set<TEntity>().AddAsync(entity);
        return entity;
    }
    public void Update<TEntity, TDto>(int id, TDto dto) where TEntity : class, IEntity where TDto : class
    {
        var entity = _mapper.Map<TEntity>(dto);
        entity.Id = id;
        _db.Set<TEntity>().Update(entity);
    }
    public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
    {
        try
        {
            var entity = await SingleAsync<TEntity>(e => e.Id.Equals(id));
            if (entity is null) return false;
            _db.Remove(entity);
        }
        catch { throw; }

        return true;
    }
    public bool Delete<TReferenceEntity, TDto>(TDto dto) where TReferenceEntity : class where TDto : class
    {
        try
        {
            var entity = _mapper.Map<TReferenceEntity>(dto);
            if (entity is null) return false;
            _db.Remove(entity);
        }
        catch { throw; }

        return true;
    }
}
