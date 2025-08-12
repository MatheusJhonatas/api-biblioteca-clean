using Biblioteca.Application.UseCases;
using Biblioteca.Application.UseCases.Livros;
using Biblioteca.Application.UseCases.Emprestimos;
using Biblioteca.Application.UseCases.Reservas;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Infrastructure.Persistense;


var builder = WebApplication.CreateBuilder(args);

// // Banco de dados (exemplo SQL Server)
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILeitorRepository, LeitorRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IBibliotecarioRepository, BibliotecarioRepository>();

// // Serviços de domínio
builder.Services.AddScoped<BibliotecarioService>();
builder.Services.AddScoped<EmprestimoService>();

// // Use Cases
builder.Services.AddScoped<CadastrarLivroUseCase>();
builder.Services.AddScoped<EditarLivroUseCase>();
builder.Services.AddScoped<RemoverLivroUseCase>();
builder.Services.AddScoped<ListarLivrosDisponiveisUseCase>();
builder.Services.AddScoped<EmprestarLivroUseCase>();
builder.Services.AddScoped<DevolverLivroUseCase>();
builder.Services.AddScoped<ReservarLivroUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
