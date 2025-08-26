using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.UseCases.Reservas
{
    public class ReservarLivroUseCase
    {
        private readonly ILeitorRepository _leitorRepo;
        private readonly ILivroRepository _livroRepo;
        private readonly IReservaRepository _reservaRepo;

        public ReservarLivroUseCase(ILeitorRepository leitorRepo, ILivroRepository livroRepo, IReservaRepository reservaRepo)
        {
            _leitorRepo = leitorRepo;
            _livroRepo = livroRepo;
            _reservaRepo = reservaRepo;
        }

        public async Task<ReservaResponse> ExecuteAsync(ReservarLivroRequest request)
        {
            var leitor = await _leitorRepo.ObterPorIdAsync(request.LeitorId) ?? throw new ArgumentException("Leitor não encontrado.");
            var livro = await _livroRepo.ObterPorIdAsync(request.LivroId) ?? throw new ArgumentException("Livro não encontrado.");

            if (livro.Disponivel)
                throw new InvalidOperationException("Livro disponível não pode ser reservado.");

            var reserva = new Reserva(leitor, livro);
            _reservaRepo.Salvar(reserva);

            return new ReservaResponse(reserva.Id, reserva.DataReserva, reserva.Status.ToString());
        }
    }
}