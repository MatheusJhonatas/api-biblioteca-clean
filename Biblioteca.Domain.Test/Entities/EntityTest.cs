using System;
using Biblioteca.Domain.Entities;
using Xunit;
using FluentAssertions;

public class EntityFake : Entity
{
    public EntityFake(Guid id) : base(id) { }
}

public class EntityTest
{
    [Fact]
    public void Entities_With_Same_Id_Should_Be_Equal()
    {
        var id = Guid.NewGuid();
        var entity1 = new EntityFake(id);
        var entity2 = new EntityFake(id);

        entity1.Equals(entity2).Should().BeTrue();
        (entity1 == entity2).Should().BeTrue();
        entity1.Equals(id).Should().BeTrue();
        entity1.Equals((object)entity2).Should().BeTrue();
        entity1.GetHashCode().Should().Be(entity2.GetHashCode());
    }

    [Fact]
    public void Entities_With_Different_Ids_Should_Not_Be_Equal()
    {
        var entity1 = new EntityFake(Guid.NewGuid());
        var entity2 = new EntityFake(Guid.NewGuid());

        entity1.Equals(entity2).Should().BeFalse();
        (entity1 == entity2).Should().BeFalse();
        entity1.Equals(entity2.Id).Should().BeFalse();
        entity1.Equals((object)entity2).Should().BeFalse();
        entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
    }

    [Fact]
    public void Entity_Should_Not_Be_Equal_To_Null()
    {
        var entity = new EntityFake(Guid.NewGuid());

        entity.Equals((Entity)null).Should().BeFalse();
        (entity == null).Should().BeFalse();
        (null == entity).Should().BeFalse();
        entity.Equals((object)null).Should().BeFalse();
    }

    [Fact]
    public void Null_Entities_Should_Be_Equal()
    {
        EntityFake entity1 = null;
        EntityFake entity2 = null;

        (entity1 == entity2).Should().BeTrue();
        (entity1 != entity2).Should().BeFalse();
    }

    [Fact]
    public void Entity_Should_Be_Equal_To_Itself()
    {
        var entity = new EntityFake(Guid.NewGuid());

        entity.Equals(entity).Should().BeTrue();
        (entity == entity).Should().BeTrue();
        entity.Equals((object)entity).Should().BeTrue();
    }
}