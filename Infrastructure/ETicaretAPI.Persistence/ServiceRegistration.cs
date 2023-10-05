using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Persistence;

public static class ServiceRegistration
{
    // Program.cs'de yapılan dependency injection yapmamıza yarayan "Services.(...)" şeklinde kullanılan
    // sınıfa erişmek için bu sınıfa extension yazmamız gerekiyordu. Bunu da this IServiceCollection kullanarak yazabiliriz.
    // Bu arayüze erişmek için Microsoft.Extensions.DependencyInjection'a ihtiyacımız var elbette.
    public static void AddPersisteneServices(this IServiceCollection services)
    {
        services.AddDbContext<ETicaretAPIDbContex>(options => options.UseMySQL(Configuration.ConnectionString));
    }
    
}