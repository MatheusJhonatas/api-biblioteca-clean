using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Xunit;

namespace Biblioteca.Domain.Test.Entites;

public class CategoriaTest
{
    #region Factory Methods
    private Categoria CriarCategoriaValida(string nome, ETipoCategoria tipo)
    {
        return new Categoria(nome, tipo);
    }
    #endregion
    #region Testes Unitários
    [Fact]
    public void Dado_ValoresValidos_Deve_CriarCategoria()
    {
        var categoria = CriarCategoriaValida("Ficção Científica", ETipoCategoria.Literatura);

        Assert.Equal("Ficção Científica", categoria.Nome);
        Assert.Equal(ETipoCategoria.Literatura, categoria.Tipo);
        Assert.NotEqual(Guid.Empty, categoria.Id);
    }
    #endregion


}