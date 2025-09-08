using Biblioteca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class BibliotecaDbContextFactory : IDesignTimeDbContextFactory<BibliotecaDbContext>
{
    public BibliotecaDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Biblioteca.API");
        // Configura builder para ler o appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // caminho atual
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<BibliotecaDbContext>();

        // Usa a connection string do appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(connectionString);

        return new BibliotecaDbContext(builder.Options);
    }
}
