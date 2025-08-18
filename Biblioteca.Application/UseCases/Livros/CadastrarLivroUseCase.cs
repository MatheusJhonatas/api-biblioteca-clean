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
        private readonly IAutorRepository _autorRepo;
        private readonly BibliotecarioService _bibliotecarioService;

        public CadastrarLivroUseCase(
            ILivroRepository livroRepo,
            IAutorRepository autorRepo,
            BibliotecarioService bibliotecarioService)
        {
            _livroRepo = livroRepo;
            _autorRepo = autorRepo;
            _bibliotecarioService = bibliotecarioService;
        }

        public async Task<ResultResponse<LivroResponse>> Execute(CadastrarLivroRequest request)
        {
            try
            {
                // 🔹 1. Validações de entrada
                if (string.IsNullOrWhiteSpace(request.Titulo))
                    return ResultResponse<LivroResponse>.Fail("O título do livro é obrigatório.");

                if (request.NumeroPaginas <= 0)
                    return ResultResponse<LivroResponse>.Fail("O número de páginas deve ser maior que zero.");

                if (request.Autor == null)
                    return ResultResponse<LivroResponse>.Fail("As informações do autor são obrigatórias.");

                if (string.IsNullOrWhiteSpace(request.ISBN))
                    return ResultResponse<LivroResponse>.Fail("O ISBN é obrigatório.");

                // 🔹 2. Verificar duplicidade de livro (ISBN deve ser único)
                var livroExistente = await _livroRepo.ObterPorISBNAsync(request.ISBN);
                if (livroExistente != null)
                    return ResultResponse<LivroResponse>.Fail("Já existe um livro cadastrado com este ISBN.");

                // 🔹 3. Verificar se o autor já existe (por e-mail, chave natural)
                var autorExistente = await _autorRepo.ObterPorEmailAsync(request.Autor.Email);

                Autor autor;
                if (autorExistente != null)
                {
                    autor = autorExistente;
                }
                else
                {
                    autor = new Autor(
                        new NomeCompleto(
                            request.Autor.NomeCompleto.Split(' ')[0],
                            request.Autor.NomeCompleto.Split(' ').Skip(1).DefaultIfEmpty("").FirstOrDefault()
                        ),
                        new Email(request.Autor.Email),
                        request.Autor.DataNascimento
                    );
                }

                // 🔹 4. Mapear categorias
                var categorias = request.Categorias
                    .Select(c => new Categoria(c.Nome, (Domain.Enums.ETipoCategoria)c.Tipo))
                    .ToList();

                var isbn = new ISBN(request.ISBN);

                // 🔹 5. Criar livro via domínio
                var livro = _bibliotecarioService.CadastrarLivro(
                    request.Titulo,
                    autor,
                    isbn,
                    request.AnoPublicacao,
                    request.NumeroPaginas,
                    categorias
                );

                _livroRepo.Salvar(livro);

                // 🔹 6. Montar resposta
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
                // 🔹 Só cai aqui erros inesperados (ex: banco offline)
                return ResultResponse<LivroResponse>.Fail($"Erro interno ao cadastrar livro: {ex.Message}");
            }
        }
    }
}
