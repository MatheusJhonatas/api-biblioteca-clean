// Atributos: Nome completo (VO), Email (VO), CPF (VO), Endereço (VO), Data de Cadastro, Lista de Empréstimos
// Regras: Pode emprestar livros, possui um limite de empréstimos ativos, pode estar inadimplente (ex: se há empréstimos atrasados)
// Métodos: Realizar empréstimo, Devolver livro, Verificar inadimplência, Atualizar endereço
namespace Biblioteca.Domain.Entities;
public class Leitor
{
    #region Propriedades
    public string NomeCompleto { get; private set; }
    public Email Email { get; private set; }
    public CPF CPF { get; private set; }
    public Endereco Endereco { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public List<Emprestimo> Emprestimos { get; private set; }
    #endregion
}