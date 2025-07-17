// Atributos: Usuário, Livro, Data de Empréstimo, Data Prevista de Devolução, Data Real de Devolução, Status
// Regras: Um empréstimo só pode ser criado se o usuário não estiver inadimplente e o livro estiver disponível
// Métodos: Finalizar empréstimo (com devolução), Calcular multa por atraso

namespace Biblioteca.Domain.Entities;

public class Emprestimo
{
    public Usuario Usuario { get; private set; }
    public Livro Livro { get; private set; }
    public DateTime DataEmprestimo { get; private set; }
    public DateTime DataPrevistaDevolucao { get; private set; }
    public DateTime? DataRealDevolucao { get; private set; }
    public string Status { get; private set; } // Ex: "Ativo", "Finalizado", "Atrasado"
}