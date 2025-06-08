using TheGuardiansEyesData;
using TheGuardiansEyesData.Repositories;
using Microsoft.EntityFrameworkCore;
using TheGuardiansEyesBusiness;
using Microsoft.OpenApi.Models;
using Oracle.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseUrls("http://0.0.0.0:5193");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TheGuardiansEyes API",
        Version = "v1",
        Description = "API .NET desenvolvida para a Global Solution da FIAP, voltada ao monitoramento inteligente de riscos ambientais com sensores IoT e visão computacional, auxiliando a Defesa Civil."
    });
});


// Configurações do Token
var jwtSecret = builder.Configuration["JwtSettings:SecretKey"];

if (string.IsNullOrEmpty(jwtSecret))
{
    throw new Exception("JWT Secret Key não configurada.");
}
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<PessoaLocalizadaService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<ImagensCapturadasService>();
builder.Services.AddScoped<SensoresService>();
builder.Services.AddScoped<DroneService>();
builder.Services.AddScoped<DesastreService>();
builder.Services.AddScoped<SubGrupoDesastreService>();
builder.Services.AddScoped<GrupoDesastreService>();
builder.Services.AddScoped<ImpactoService>();
builder.Services.AddScoped<ImpactoClassificacaoService>();
builder.Services.AddScoped<TerrenoGeograficoService>();
builder.Services.AddScoped<LocalService>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheGuardiansEyes API v1");
    });
}


app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();




app.MapControllers();
app.Run();