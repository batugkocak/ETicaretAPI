using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Contexts;
using ETicaretAPI.Persistence.Repositories;
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
        services.AddDbContext<ETicaretAPIDbContext>(options => options.UseMySQL(Configuration.ConnectionString));

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        
        
        //Context Lifetime'ı default olarak Scoped'dır. 
        //Scoped => Her request için oluşturulan nesne, iş bitiminde dispose edilir.
        
        //İlk örnekte yapılan hata, Get() işleminin dönüş tipinin void olmasıdır.
        //AddRangeAsync çağırıldıktan sonra iş bittiği için nesnemiz, dispose edilir fakat
        //SaveAsync de hemen ardından çağırıldığı için problem yaşanmaktadır.
        
        //Singleton yaptığımızda düzeliyordu çünkü nesneler asla dispose edilmiyordu.
        
        // services.AddDbContext<ETicaretAPIDbContext>(options => options.UseMySQL(Configuration.ConnectionString), ServiceLifetime.Singleton);
        //
        // services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
        // services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
        // services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
        // services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
        // services.AddSingleton<IProductReadRepository, ProductReadRepository>();
        // services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();

    }
    
}