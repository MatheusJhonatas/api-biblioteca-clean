namespace Biblioteca.Application.DTOs.Requests
{
    public record EditarLivroRequest(string? NovoTitulo, int? NovoAnoPublicacao, int? NovoNumeroPaginas);
}