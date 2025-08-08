namespace Biblioteca.Application.DTOs.Responses
{
    public record EmprestimoResponse(Guid EmprestimoId, DateTime DataEmprestimo, DateTime DataPrevistaDevolucao);
}
