using System.Threading.Tasks;
using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;

namespace Biblioteca.Application.UseCases.Emprestimos
{
    public class DevolverLivroUseCase
    {
        private readonly ILeitorRepository _leitorRepo;
        private readonly IEmprestimoRepository _emprestimoRepo;
        private readonly EmprestimoService _emprestimoService;

        public DevolverLivroUseCase(
            ILeitorRepository leitorRepo,
            IEmprestimoRepository emprestimoRepo,
            EmprestimoService emprestimoService)
        {
            _leitorRepo = leitorRepo;
            _emprestimoRepo = emprestimoRepo;
            _emprestimoService = emprestimoService;
        }

        public async Task Execute(DevolverLivroRequest request)
        {
            var leitor = await _leitorRepo.ObterPorIdAsync(request.LeitorId)
                ?? throw new ArgumentException("Leitor não encontrado.");

            var emprestimo = _emprestimoRepo.ObterPorId(request.EmprestimoId)
                ?? throw new ArgumentException("Empréstimo não encontrado.");

            _emprestimoService.DevolverLivro(leitor, emprestimo.Id);

            _emprestimoRepo.Atualizar(emprestimo);
        }
    }
}
