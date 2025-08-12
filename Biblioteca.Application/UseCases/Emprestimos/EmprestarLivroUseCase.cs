using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;

namespace Biblioteca.Application.UseCases.Emprestimos
{
    public class EmprestarLivroUseCase
    {
        private readonly ILeitorRepository _leitorRepo;
        private readonly ILivroRepository _livroRepo;
        private readonly IEmprestimoRepository _emprestimoRepo;
        private readonly EmprestimoService _emprestimoService;

        public EmprestarLivroUseCase(ILeitorRepository leitorRepo, ILivroRepository livroRepo, IEmprestimoRepository emprestimoRepo, EmprestimoService emprestimoService)
        {
            _leitorRepo = leitorRepo;
            _livroRepo = livroRepo;
            _emprestimoRepo = emprestimoRepo;
            _emprestimoService = emprestimoService;
        }

        public EmprestimoResponse Execute(EmprestarLivroRequest request)
        {
            var leitor = _leitorRepo.ObterPorId(request.LeitorId) ?? throw new ArgumentException("Leitor não encontrado.");
            var livro = _livroRepo.ObterPorId(request.LivroId) ?? throw new ArgumentException("Livro não encontrado.");

            var emprestimo = _emprestimoService.RealizarEmprestimo(leitor, livro);
            _emprestimoRepo.Salvar(emprestimo);

            return new EmprestimoResponse(emprestimo.Id, emprestimo.DataEmprestimo, emprestimo.DataPrevistaDevolucao);
        }
    }
}