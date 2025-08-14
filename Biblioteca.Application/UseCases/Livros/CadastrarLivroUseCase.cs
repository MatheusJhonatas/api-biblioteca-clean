using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;
using Biblioteca.Domain.ValueObjects;
namespace Biblioteca.Application.UseCases.Livros
{
    public class CadastrarLivroUseCase
    {
        private readonly ILivroRepository _livroRepo;
        private readonly BibliotecarioService _bibliotecarioService;

        public CadastrarLivroUseCase(ILivroRepository livroRepo, BibliotecarioService bibliotecarioService)
        {
            _livroRepo = livroRepo;
            _bibliotecarioService = bibliotecarioService;
        }

        public async Task<ResultResponse<LivroResponse>> Execute(CadastrarLivroRequest request)
        {
            try
            {
                var autor = new Autor(
                    new NomeCompleto(
                        request.Autor.NomeCompleto.Split(' ')[0],
                        request.Autor.NomeCompleto.Split(' ').Skip(1).DefaultIfEmpty("").FirstOrDefault()
                    ),
                    new Email(request.Autor.Email),
                    request.Autor.DataNascimento
                );

                var categorias = request.Categorias
                    .Select(c => new Categoria(c.Nome, (Domain.Enums.ETipoCategoria)c.Tipo))
                    .ToList();

                var isbn = new ISBN(request.ISBN);

                var livro = _bibliotecarioService.CadastrarLivro(
                    request.Titulo,
                    autor,
                    isbn,
                    request.AnoPublicacao,
                    categorias
                );

                _livroRepo.Salvar(livro);

                var response = new LivroResponse(
                    livro.Id,
                    livro.Titulo,
                    livro.Autor.NomeCompleto.ToString(),
                    livro.AnoPublicacao,
                    livro.Disponivel
                );

                return ResultResponse<LivroResponse>.Ok(response, "Livro cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return ResultResponse<LivroResponse>.Fail($"Erro ao cadastrar livro: {ex.Message}");
            }
        }
    }
}
