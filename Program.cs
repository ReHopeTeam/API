using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ReHope.Applications.ContentSafety;
using ReHope.Applications.Services;
using ReHope.Contexts;
using ReHope.Interfaces;
using ReHope.Repository;

var builder = WebApplication.CreateBuilder(args);

// carregando o .env
Env.Load();

// pegando a connection string 
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// conexao com o banco 
builder.Services.AddDbContext<ReHopeContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

// Produto
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();

// IA
builder.Services.AddScoped<IContentSafetyRepository, ContentSafetyService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
