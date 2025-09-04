using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Entities;

public class Leitor : Entity
{
    private readonly List<Reserva> _reservas = new();
    private readonly List<Emprestimo> _emprestimos = new();
    #region Propriedades
    public NomeCompleto NomeCompleto { get; private set; }
    public Email Email { get; private set; }
    public CPF CPF { get; private set; }
    public Endereco Endereco { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public IReadOnlyCollection<Emprestimo> Emprestimos { get { return _emprestimos.AsReadOnly(); } }
    public IReadOnlyCollection<Reserva> Reservas { get { return _reservas.AsReadOnly(); } }
    public bool EstaInadimplente => _emprestimos.Any(e => e.EstaAtrasado());
    public int LimiteEmprestimosAtivos { get; private set; } = 5;
    #endregion
    #region Construtores
    public Leitor() : base(Guid.NewGuid())
    {
        _emprestimos = new List<Emprestimo>();
        _reservas = new List<Reserva>();
    }
    public Leitor(NomeCompleto nomeCompleto, Email email, CPF cPF, Endereco endereco, DateTime dataCadastro) : base(Guid.NewGuid())
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        CPF = cPF;
        Endereco = endereco;
        DataCadastro = dataCadastro;
        _emprestimos = new List<Emprestimo>();
        _reservas = new List<Reserva>();
    }
    #endregion
    #region Metodos
    public void AtualizarEndereco(Endereco novoEndereco)
    {
        if (novoEndereco == null)
            throw new ArgumentNullException(nameof(novoEndereco));

        Endereco = novoEndereco;
    }
    public void RealizarEmprestimo(Emprestimo emprestimo)
    {
        if (emprestimo is null) throw new ArgumentNullException(nameof(emprestimo));
        // evita duplicidade do mesmo livro enquanto houver um empréstimo ativo dele
        bool jaTemMesmoLivroAtivo = _emprestimos.Any(e => e.Livro.Id == emprestimo.Livro.Id && e.EmprestimoEmAndamento());
        if (jaTemMesmoLivroAtivo) throw new InvalidOperationException("Leitor já possui empréstimo ativo deste livro.");
        _emprestimos.Add(emprestimo);
    }
    public void RealizarDevolucao(Guid emprestimoId)
    {
        var emp = _emprestimos.FirstOrDefault(e => e.Id == emprestimoId)
                ?? throw new InvalidOperationException("Empréstimo não encontrado para o leitor.");

        emp.FinalizarEmprestimo(DateTime.Now); // garante que Emprestimo/ Livro executem suas regras (marca livro disponível, set data devolução)
    }
    // public void RealizarReserva(Reserva reserva)
    // {
    //     //regra para que se for null, não permita a reserva
    //     if (reserva is null) throw new ArgumentNullException(nameof(reserva));
    //     // evita duplicidade do mesmo livro enquanto houver uma reserva ativa dele
    //     bool jaTemMesmoLivroAtivo = _reservas.Any(r => r.Livro.Id == reserva.Livro.Id && r.ReservaAtiva());
    //     if (jaTemMesmoLivroAtivo) throw new InvalidOperationException("Leitor já possui reserva ativa deste livro.");
    //     _reservas.Add(reserva);
    // }
    internal Emprestimo ObterEmprestimoPorId(Guid id)
    {
        return _emprestimos.FirstOrDefault(e => e.Id == id);
    }
    #endregion
}