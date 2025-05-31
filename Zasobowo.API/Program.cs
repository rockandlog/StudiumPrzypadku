using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zasobowo.API.Data;
using Zasobowo.API.Services;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
// Baza danych SQLite
builder.Services.AddDbContext<ZasobowoContext>(options =>
    options.UseSqlite("Data Source=Zasobowo.db"));

// Dodaj kontrolery, Swaggera i inne
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zasobowo API", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Description = "Wpisz: Bearer <token>",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, new string[] { } }
    });
});
=======
// Dodaj kontekst bazy danych (SQLite)
builder.Services.AddDbContext<ZasobowoContext>(options =>
    options.UseSqlite("Data Source=Zasobowo.db"));

// Dodaj kontrolery, Swaggera i inne usługi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
>>>>>>> develop

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
<<<<<<< HEAD
            IssuerSigningKey = new SymmetricSecurityKey(key)
=======
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("TwojSuperSekretnyKljucz123!")) // ← Upewnij się, że pasuje do kontrolera Auth
>>>>>>> develop
        };
    });

builder.Services.AddAuthorization();

<<<<<<< HEAD
// Serwisy aplikacyjne
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

=======
>>>>>>> develop
var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
