namespace Biblioteca.Application.DTOs.Requests
{
    public record ReservarLivroRequest(Guid LeitorId, Guid LivroId);
}