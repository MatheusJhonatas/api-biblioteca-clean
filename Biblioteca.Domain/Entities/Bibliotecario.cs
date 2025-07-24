// Atributos: Nome completo (VO), Email (VO), Matrícula, Cargo
// Regras: Pode cadastrar livros, excluir, atualizar cadastros, processar devoluções
// Métodos: Cadastrar livro, Atualizar estoque, Visualizar empréstimos ativos
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Entities;

public class Bibliotecario
{
    #region Propriedades
    public NomeCompleto NomeCompleto { get; private set; }
    public Email Email { get; private set; }
    public string Matricula { get; private set; }
    public string Cargo { get; private set; }
    #endregion

    #region Construtores
    // Construtor para inicializar as propriedades do bibliotecário.
    public Bibliotecario(string nomeCompleto, string email, string matricula, string cargo)
    {
        Matricula = matricula;
        Cargo = cargo;
    }
    #endregion
    #region Métodos
    public void CadastrarLivro(Livro livro)
    {
        // Lógica para cadastrar livro
    }

    public void AtualizarEstoque(Livro livro, int quantidade)
    {
        // Lógica para atualizar estoque do livro
    }

    public void VisualizarEmprestimosAtivos()
    {
        // Lógica para visualizar empréstimos ativos
    }
    #endregion
}