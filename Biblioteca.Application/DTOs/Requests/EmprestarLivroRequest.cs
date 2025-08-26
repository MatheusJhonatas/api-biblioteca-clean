namespace Biblioteca.Application.DTOs.Requests;

public record EmprestarLivroRequest(Guid LeitorId, Guid LivroId);

