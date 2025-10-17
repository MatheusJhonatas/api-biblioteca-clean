using Biblioteca.Application.UseCases;
using Biblioteca.Application.UseCases.Livros;
using Biblioteca.Application.UseCases.Emprestimos;
using Biblioteca.Application.UseCases.Reservas;
using Biblioteca.Application.UseCases.Leitores;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;
using Biblioteca.Infrastructure.Persistence;
using Biblioteca.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Banco de dados
            services.AddDbContext<BibliotecaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositórios
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<ILeitorRepository, LeitorRepository>();
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<IBibliotecarioRepository, BibliotecarioRepository>();

            // Serviços de domínio
            services.AddScoped<BibliotecarioService>();
            services.AddScoped<EmprestimoService>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Use Cases
            services.AddScoped<CadastrarLivroUseCase>();
            services.AddScoped<EditarLivroUseCase>();
            services.AddScoped<RemoverLivroUseCase>();
            services.AddScoped<ListarLivrosDisponiveisUseCase>();
            services.AddScoped<ObterLivroPorIdUseCase>();
            services.AddScoped<EmprestarLivroUseCase>();
            services.AddScoped<DevolverLivroUseCase>();
            services.AddScoped<ReservarLivroUseCase>();

            services.AddScoped<CadastrarLeitorUseCase>();
            services.AddScoped<EditarLeitorUseCase>();
            services.AddScoped<DeletarLeitorUseCase>();
            services.AddScoped<ListarLeitoresUseCase>();
            services.AddScoped<ObterLeitorPorIdUseCase>();
            services.AddScoped<ListarEmprestimoUseCase>();
            services.AddScoped<ListarLivrosEmprestadosUseCase>();

            return services;
        }
    }
}
