using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersisteneServices();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyOrigin()));

// Normalde, Asp.NET'in kendi filtresi devreye giriyordu. Bu filtre, bizim eklediğimiz FluentValidation'a uyumlu
// olmasından dolayı validate edilemeyen veriler için "post" fonksiyonunu bile oluşturmuyorudu.
// "options.SuppressModelStateInvalidFilter = true" diyerek default filter'ı devre dışı bıraktık. 
// Sonra ValidationFilter adında kendi filtremizi yazdık ve programımıza entegre ettik.
// Hatta, kendi filtremizde (ValidationFilter), dönen hatalar dizi olarak döneceği için çok daha iyi bir şekilde dönüyor.
builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
    })
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//???
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//Çünkü her fonksiyonun içinde SaveChanges'ı çağırmak demek lüzumsuz yere her işlem sonunda bir transaction başlatmak demektir.
//Haliyle işlemleri toplu olarak yapıp tek bir transaction içerisinde veritabanına yansıtabilmek için SaveChanges'ı farklı
//bir fonksiyona almış bulunmaktayız