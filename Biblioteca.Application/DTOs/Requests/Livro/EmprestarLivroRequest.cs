namespace Biblioteca.Application.DTOs.Requests.Livro;

public record EmprestarLivroRequest(Guid LeitorId, Guid LivroId);

