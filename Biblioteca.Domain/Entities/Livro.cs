using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Entities
{
    public sealed class Livro : Entity
    {
        #region Propriedades
        public string Titulo { get; private set; }
        public Autor Autor { get; private set; }
        public ISBN ISBN { get; private set; }
        public int AnoPublicacao { get; private set; }
        public int NumeroPaginas { get; private set; }
        public bool Disponivel { get; private set; }

        // Encapsulamento correto para coleção de navegação
        private readonly List<Categoria> _categorias;
        public IReadOnlyCollection<Categoria> Categorias => _categorias.AsReadOnly();
        #endregion

        #region Contrutores
        public Livro() : base(Guid.NewGuid()) { }
        public Livro(string titulo, Autor autor, ISBN isbn, int numeroPaginas, int anoPublicacao, List<Categoria> categorias) : base(Guid.NewGuid())
        {
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            AnoPublicacao = anoPublicacao;
            NumeroPaginas = numeroPaginas;
            _categorias = categorias ?? new List<Categoria>();
            Disponivel = true;
        }
        #endregion

        #region Metodos
        public void Emprestar()
        {
            if (!Disponivel)
                throw new InvalidOperationException("O livro já está emprestado.");

            Disponivel = false;
        }
        public void Devolver()
        {
            if (Disponivel)
                throw new InvalidOperationException("O livro já está disponível.");

            Disponivel = true;
        }
        public bool VerificaDisponibilidade()
        {
            return Disponivel;
        }
        public void AdicionarCategoria(Categoria categoria)
        {
            if (categoria == null)
                throw new ArgumentNullException(nameof(categoria));

            if (_categorias.Any(c => c.Id == categoria.Id))
                return; // Categoria já adicionada, evitar duplicidade
            _categorias.Add(categoria);
        }
        public void AlterarTitulo(string novoTitulo)
        {
            if (string.IsNullOrWhiteSpace(novoTitulo))
                throw new ArgumentException("Título inválido.");
            Titulo = novoTitulo;
        }

        public void AlterarAnoPublicacao(int novoAno)
        {
            if (novoAno < 1800 || novoAno > DateTime.Now.Year)
                throw new ArgumentException("Ano inválido.");
            AnoPublicacao = novoAno;
        }
        #endregion
    }
}