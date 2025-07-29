using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Domain.Enums;

namespace Biblioteca.Domain.Entities;
//Classe está como sealed para evitar herança, fazendo assim com que a Categoria seja uma entidade final.
// Isso é útil para garantir que a estrutura da categoria não seja alterada por heranças indesejadas.


public sealed class Categoria : Entity
{
    #region Propriedades
    public string Nome { get; private set; }
    public ETipoCategoria Tipo { get; private set; }
    #endregion
    #region Construtores
    // Construtores para inicializar a categoria com nome, descrição e tipo, podendo receber um Id
    public Categoria(string nome, ETipoCategoria tipo)
        : base(Guid.NewGuid())
    {
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Tipo = tipo;
    }
    #endregion
    #region Métodos
    // Regras: Um livro pode ter uma ou mais categorias.

    #endregion
}

