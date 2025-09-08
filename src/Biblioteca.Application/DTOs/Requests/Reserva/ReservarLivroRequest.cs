namespace Biblioteca.Application.DTOs.Requests.Reserva;

public record ReservarLivroRequest(Guid LeitorId, Guid LivroId);
