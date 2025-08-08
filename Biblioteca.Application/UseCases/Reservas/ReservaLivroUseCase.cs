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

        public ReservaResponse Execute(ReservarLivroRequest request)
        {
            var leitor = _leitorRepo.ObterPorId(request.LeitorId) ?? throw new ArgumentException("Leitor não encontrado.");
            var livro = _livroRepo.ObterPorId(request.LivroId) ?? throw new ArgumentException("Livro não encontrado.");

            if (livro.Disponivel)
                throw new InvalidOperationException("Livro disponível não pode ser reservado.");

            var reserva = new Reserva(leitor, livro);
            _reservaRepo.Salvar(reserva);

            return new ReservaResponse(reserva.Id, reserva.DataReserva, reserva.Status.ToString());
        }
    }
}