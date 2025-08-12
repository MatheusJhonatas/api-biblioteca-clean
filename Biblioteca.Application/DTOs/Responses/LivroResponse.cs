namespace Biblioteca.Application.DTOs.Responses
{
    public record LivroResponse(Guid Id, string Titulo, string Autor, int AnoPublicacao, bool Disponivel);
}