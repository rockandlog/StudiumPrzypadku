using Microsoft.EntityFrameworkCore;
using Zasobowo.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ZasobowoContext>(options =>
    options.UseSqlite("Data Source=zasobowo.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
