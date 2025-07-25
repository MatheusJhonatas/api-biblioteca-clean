// Atributos: Título, Autor, ISBN (VO), Ano de Publicação, Categoria(s), Disponibilidade
// Regras: Pode estar disponível ou emprestado, ISBN deve ser válido
// Métodos: Emprestar, Devolver, Verificar disponibilidade
using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Domain.ValueObjects;
namespace Biblioteca.Domain.Entities
{
    public sealed class Livro
    {
        #region Propriedades
        public string Titulo { get; private set; }
        public Autor Autor { get; private set; }
        public ISBN ISBN { get; private set; }
        public int AnoPublicacao { get; private set; }
        public int CategoriaId { get; private set; }
        public bool Disponivel { get; private set; }
        public List<Categoria> Categorias { get; private set; } = new();
        #endregion
        #region Contrutores
        public Livro(string titulo, Autor autor, ISBN isbn, int anoPublicacao, int categoriaId, List<Categoria> categorias)
        {
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            AnoPublicacao = anoPublicacao;
            CategoriaId = categoriaId;
            Categorias = categorias ?? new List<Categoria>();
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

            if (Categorias.Any(c => c.Id == categoria.Id))
                return;// Categoria já adicionada, evitar duplicidade
            Categorias.Add(categoria);
        }
        #endregion
    }
}
