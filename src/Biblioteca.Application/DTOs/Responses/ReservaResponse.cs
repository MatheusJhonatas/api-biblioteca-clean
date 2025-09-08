namespace Biblioteca.Application.DTOs.Responses;

public record ReservaResponse(Guid ReservaId, DateTime DataReserva, string Status);
