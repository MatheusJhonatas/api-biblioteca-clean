using System;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Services;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;
namespace Biblioteca.Domain.Test.Services;

public class BibliotecarioServiceTest
{
    [Fact]
    public void Deve_Criar_Livro_Com_Sucesso()
    {
        var service = new BibliotecarioService();

        var autor = new Autor(new NomeCompleto("João", "Victor"), new Email("joaookbk@gmail.com"), new DateTime(1999, 1, 2));

        var categorias = new List<Categoria> { new Categoria("Romance", ETipoCategoria.Romance) };

        var livro = service.CadastrarLivro("Samsung Essentials", autor, new ISBN("1234567800"), 2020, categorias);
        livro.Should().NotBeNull();
        livro.Titulo.Should().Be("Samsung Essentials");
        livro.Autor.Should().Be(autor);

    }
}