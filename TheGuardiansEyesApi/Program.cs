using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;
using TheGuardiansEyesBusiness;

using Oracle.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));


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
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();
app.Run();

