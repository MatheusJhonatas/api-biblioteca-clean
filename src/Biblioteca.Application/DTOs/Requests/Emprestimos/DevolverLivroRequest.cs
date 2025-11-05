namespace Biblioteca.Application.DTOs.Requests.Emprestimos;

public class DevolverLivroRequest
{
    public Guid LeitorId { get; }
    public Guid EmprestimoId { get; }
    public DevolverLivroRequest(Guid leitorId, Guid emprestimoId)
    {
        LeitorId = leitorId;
        EmprestimoId = emprestimoId;
    }
}