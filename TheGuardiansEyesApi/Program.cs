using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;
using TheGuardiansEyesBusiness;
using Microsoft.OpenApi.Models;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
app.MapControllers();
app.Run();