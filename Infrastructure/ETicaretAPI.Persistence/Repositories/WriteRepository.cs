using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T: BaseEntity
{
    readonly private ETicaretAPIDbContext _context;
    public WriteRepository(ETicaretAPIDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();
    
    public async Task<bool> AddAsync(T model)
    {
     var entityEntry =  await Table.AddAsync(model);
     return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> model)
    {
        await Table.AddRangeAsync(model);
        return true;
    }

    public bool Remove(T model)
    {
        var entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool RemoveRange(List<T> models)
    {
        Table.RemoveRange(models);
        return true;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var entity= await Table.FirstOrDefaultAsync(entity => entity.Id.ToString() == id);
        return Remove(entity);
        
    }

    public bool Update(T model)
    {
        var entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
}