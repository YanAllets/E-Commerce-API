using System.Security.Cryptography.X509Certificates;
using System.Text;
using ECAPI.AccountService;
using ECAPI.AuthenticationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

byte[] securityKey = Encoding.UTF8.GetBytes("minha chave secreta");

SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(securityKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{   
   // AQUI: configurações gerais do JWT Bearer]
   // criptografa e pega so o negocio em bytes
    

    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = symmetricKey,
        // AQUI: regras específicas de validação do token
        // o negocio vai ser pauleado com ISSO daqui TEM QUE TER
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddSingleton(symmetricKey);
builder.Services.AddSingleton<JwtService>();
builder.Services.AddScoped<AccountService>();
//// Scoped cria uma instância por requisição HTTP.
builder.Services.AddAuthorization();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();