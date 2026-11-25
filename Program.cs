using System.Security.Cryptography.X509Certificates;
using Microsoft.OpenApi.Models;
using System.Text;
using ECAPI.AccountService;
using ECAPI.AuthenticationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        //aqui explica como autenticar aqui como o bearer no cabeçalho
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        //pede pro swagger usar essa configuração nas chamadas
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

byte[] securityKey = Encoding.UTF8.GetBytes("Symphony-Pouncing3-Ice-Unplowed-Uphold-Starlet");

SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(securityKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{   
   // AQUI: configurações gerais do JWT Bearer]
   // encoda e pega so o negocio em bytes
    // a funcçao aqui e configurar como um token deve ser conferido

    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = symmetricKey,
        // AQUI: regras específicas de validação do token
        // o negocio vai ser pauleado com ISSO daqui TEM QUE TER
        ValidateIssuer = true,
        ValidIssuer = "ECAPI",
        ValidateAudience = true,
        ValidAudience = "ECAPI",
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
// faz o middleware conferir o token em cada requisicao

app.UseAuthorization();
// UseAuthorization + [Authorize](la nos controller)
//    decide se a rota pode ser acessada
app.UseHttpsRedirection();

app.MapControllers();

app.Run();