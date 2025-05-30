using Microsoft.EntityFrameworkCore;
using Zasobowo.API.Data;
using Zasobowo.Sync;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dodanie DbContext
builder.Services.AddDbContext<ZasobowoContext>(options =>
    options.UseSqlite("Data Source=zasobowo.db"));

// Dodanie kontrolerów + Swagger + SignalR
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// Konfiguracja JWT (jeœli ju¿ masz rejestracjê/logowanie)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "ZasobowoAPI",
            ValidAudience = "ZasobowoClient",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Developmentowe narzêdzia
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// SignalR - dodane w kroku 2
app.MapHub<SyncHub>("/synchub");

app.Run();
