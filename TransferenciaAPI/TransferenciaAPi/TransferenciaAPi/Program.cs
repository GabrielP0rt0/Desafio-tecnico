using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TransferenciaAPi.Constans;
using TransferenciaAPi.Data;
using TransferenciaAPi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//configuração doc Swagger
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new()
    {   
        Title = "Teste",
        Version = "v2",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });
    
});
builder.Services.AddScoped<AutorizacaoServices>();
builder.Services.AddScoped<TransferenciaService>();
//configurção do banco de dados, no meu caso estou utilizando Mysql
builder.Services.AddDbContext<DBContext>(options =>
    options.UseMySQL("server=localhost;port=3306;user=root;password=root;database=desafiotecnico;")
);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
    x.TokenValidationParameters = new() { 
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeySecret.secret)),
        ValidateAudience = false,
        ValidateIssuer = false
    }
);
var app = builder.Build();


app.UseRouting();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste V2");
    c.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
