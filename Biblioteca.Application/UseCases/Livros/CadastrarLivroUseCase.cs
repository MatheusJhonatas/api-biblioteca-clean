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
                // Verificar duplicidade
                var livroExistente = await _livroRepo.ObterPorTituloEAutorAsync(request.Titulo, request.Autor.NomeCompleto);
                if (livroExistente != null)
                {
                    return ResultResponse<LivroResponse>.Fail("Já existe um livro cadastrado com este título e autor.");
                }

                // 🔹 Validações básicas
                if (string.IsNullOrWhiteSpace(request.Titulo))
                    return ResultResponse<LivroResponse>.Fail("O título do livro é obrigatório.");

                if (request.NumeroPaginas <= 0)
                    return ResultResponse<LivroResponse>.Fail("O número de páginas deve ser maior que zero.");

                if (request.Autor == null)
                    return ResultResponse<LivroResponse>.Fail("As informações do autor são obrigatórias.");

                //criando o autor, e dado o value object Nome completo, estamos garantindo que o nome esteja sempre em um formato válido, que é "Nome Sobrenome"
                var autor = new Autor(
                    new NomeCompleto(
                        request.Autor.NomeCompleto.Split(' ')[0],
                        request.Autor.NomeCompleto.Split(' ').Skip(1).DefaultIfEmpty("").FirstOrDefault()
                    ),
                    new Email(request.Autor.Email),
                    request.Autor.DataNascimento
                );
                //criando as categorias e dado o value object TipoCategoria, estamos garantindo que o tipo esteja sempre em um formato válido. Então fazemos o select criando uma nova categoria e batento nos Enums cadastrados.
                var categorias = request.Categorias
                    .Select(c => new Categoria(c.Nome, (Domain.Enums.ETipoCategoria)c.Tipo))
                    .ToList();

                var isbn = new ISBN(request.ISBN);
                //CRIANDO LIVRO VIA SERVICE DE DOMINIO
                var livro = _bibliotecarioService.CadastrarLivro(
                    request.Titulo,
                    autor,
                    isbn,
                    request.AnoPublicacao,
                    request.NumeroPaginas,
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
