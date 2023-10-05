using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ETicaretAPI.Persistence;

// Dotnet CLI üzerinden komutla Db oluşturmak için, CLI'da uygulamayı ayağa kaldırmadığı için böyle bir yol göstericiye ihtiyaç duyuyor.
// CLI kullanmadığım için benim normalde ihtiyacım yok fakat öğrenmek niyetiyle yapıyorum.  
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretAPIDbContext>
{
    public ETicaretAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ETicaretAPIDbContext> dbContextOptionsBuilder = new();
        //Persistence'a JSON eklenebilirdi fakat halihazırda json olduğu için oraya kullanıyourum.
        dbContextOptionsBuilder.UseMySQL(Configuration.ConnectionString);
        return new ETicaretAPIDbContext(dbContextOptionsBuilder.Options);
    }
}