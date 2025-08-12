namespace Biblioteca.Application.DTOs.Requests
{

    public record DevolverLivroRequest(Guid LeitorId, Guid EmprestimoId);
}