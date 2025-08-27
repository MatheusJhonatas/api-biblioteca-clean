namespace Biblioteca.Application.DTOs.Requests.Livro;

public record DevolverLivroRequest(Guid LeitorId, Guid EmprestimoId);