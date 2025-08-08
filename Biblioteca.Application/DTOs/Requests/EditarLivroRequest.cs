namespace Biblioteca.Application.DTOs.Requests
{
    public record EditarLivroRequest(Guid LivroId, string NovoTitulo, int NovoAnoPublicacao);
}