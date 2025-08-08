using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Application.DTOs.Requests
{
    public record CadastrarLivroRequest(
        string Titulo, Autor Autor, ISBN ISBN, int AnoPublicacao, List<Categoria> Categorias);
}