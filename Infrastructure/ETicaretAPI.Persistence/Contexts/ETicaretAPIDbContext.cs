using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Contexts;

public class ETicaretAPIDbContext: DbContext
{
    public ETicaretAPIDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //ChangeTracker: Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir.
        // Track edilen verileri yakalayıp elde etmemizi sağlar.

        var datas = ChangeTracker
            .Entries<BaseEntity>();

        foreach (var data in datas)
        {
            if (data.State == EntityState.Added)
            {
                data.Entity.CreatedDate = DateTime.Now;
            }
            if (data.State == EntityState.Modified)
            {
                data.Entity.UpdatedDate = DateTime.Now;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}