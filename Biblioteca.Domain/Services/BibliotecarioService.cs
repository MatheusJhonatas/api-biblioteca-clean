using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Biblioteca.Domain.Services
{
    public class BibliotecarioService
    {
        public Livro CadastrarLivro(
            string titulo,
            Autor autor,
            ISBN isbn,
            int anoPublicacao,
            List<Categoria> categorias)
        {
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título inválido.");
            if (autor == null) throw new ArgumentNullException(nameof(autor));
            if (isbn == null) throw new ArgumentNullException(nameof(isbn));
            if (anoPublicacao < 1800 || anoPublicacao > DateTime.Now.Year)
                throw new ArgumentException("Ano de publicação inválido.");
            if (categorias == null || categorias.Count == 0)
                throw new ArgumentException("O livro deve ter ao menos uma categoria.");

            // Supondo que o parâmetro int seja 'numeroPaginas', defina um valor apropriado, por exemplo 0 ou solicite como argumento do método
            int numeroPaginas = 0; // ajuste conforme necessário
            return new Livro(titulo, autor, isbn, numeroPaginas, anoPublicacao, categorias);
        }

        public void EditarLivro(
            Livro livro,
            string novoTitulo,
            int novoAnoPublicacao)
        {
            if (livro == null) throw new ArgumentNullException(nameof(livro));
            if (!string.IsNullOrWhiteSpace(novoTitulo))
                livro.AlterarTitulo(novoTitulo);

            if (novoAnoPublicacao > 1800 && novoAnoPublicacao <= DateTime.Now.Year)
                livro.AlterarAnoPublicacao(novoAnoPublicacao);
        }

        public void AdicionarCategoriaAoLivro(Livro livro, Categoria categoria)
        {
            if (livro == null) throw new ArgumentNullException(nameof(livro));
            if (categoria == null) throw new ArgumentNullException(nameof(categoria));

            livro.AdicionarCategoria(categoria);
        }

        public void RemoverLivro(Livro livro)
        {
            if (!livro.Disponivel)
                throw new InvalidOperationException("Não é possível remover um livro emprestado.");

            // Aqui você poderia invocar um repositório para excluir o livro
            // ex: livroRepository.Remover(livro);
        }
    }
}
