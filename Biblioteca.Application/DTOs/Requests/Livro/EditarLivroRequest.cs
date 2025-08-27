namespace Biblioteca.Application.DTOs.Requests.Livro;

public record EditarLivroRequest(string? NovoTitulo, int? NovoAnoPublicacao, int? NovoNumeroPaginas);
