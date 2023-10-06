using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersisteneServices();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    policy.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyOrigin()));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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