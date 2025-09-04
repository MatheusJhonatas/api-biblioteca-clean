using Biblioteca.Application.UseCases;
using Biblioteca.Application.UseCases.Livros;
using Biblioteca.Application.UseCases.Emprestimos;
using Biblioteca.Application.UseCases.Reservas;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Infrastructure.Persistence;
using Biblioteca.Infrastructure.Repositories;
using Biblioteca.Application.UseCases.Leitores;


var builder = WebApplication.CreateBuilder(args);


// // Banco de dados (exemplo SQL Server)
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
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
builder.Services.AddScoped<ObterLivroPorIdUseCase>();
builder.Services.AddScoped<EmprestarLivroUseCase>();
builder.Services.AddScoped<DevolverLivroUseCase>();
builder.Services.AddScoped<ReservarLivroUseCase>();
builder.Services.AddScoped<CadastrarLeitorUseCase>();
// builder.Services.AddScoped<EditarLeitorUseCase>();
// builder.Services.AddScoped<RemoverLeitorUseCase>();
builder.Services.AddScoped<ListarLeitoresUseCase>();
builder.Services.AddScoped<ObterLeitorPorIdUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

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
