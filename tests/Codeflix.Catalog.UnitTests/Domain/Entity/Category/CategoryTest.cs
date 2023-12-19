using Codeflix.Catalog.Domain.Exceptions;
using System;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;
public class CategoryTest
{
  [Fact(DisplayName = nameof(InstantiateWithDefaultValues))]
  [Trait("Domain", "Category - Aggregates")]
  public void InstantiateWithDefaultValues()
  {
    var validData = new
    {
      Name = "category name",
      Description = "category description"
    };
    DateTime dateTimeBefore = DateTime.Now;
    DomainEntity.Category category = new(validData.Name, validData.Description);
    DateTime dateTimeAfter = DateTime.Now;

    Assert.NotNull(category);
    Assert.Equal(validData.Name, category.Name);
    Assert.Equal(validData.Description, category.Description);
    Assert.NotEqual(default, category.Id);
    Assert.NotEqual(default, category.CreatedAt);
    Assert.True(category.CreatedAt < dateTimeAfter);
    Assert.True(category.CreatedAt > dateTimeBefore);
    Assert.True(category.IsActive);
  }

  [Theory(DisplayName = nameof(InstantiateWithIsActive))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData(true)]
  [InlineData(false)]
  public void InstantiateWithIsActive(bool isActive)
  {
    var validData = new
    {
      Name = "category name",
      Description = "category description"
    };
    DateTime dateTimeBefore = DateTime.Now;
    DomainEntity.Category category = new(validData.Name, validData.Description, isActive);
    DateTime dateTimeAfter = DateTime.Now;

    Assert.NotNull(category);
    Assert.Equal(validData.Name, category.Name);
    Assert.Equal(validData.Description, category.Description);
    Assert.NotEqual(default, category.Id);
    Assert.NotEqual(default, category.CreatedAt);
    Assert.True(category.CreatedAt < dateTimeAfter);
    Assert.True(category.CreatedAt > dateTimeBefore);
    Assert.Equal(isActive, category.IsActive);
  }

  [Theory(DisplayName = nameof(ThrowWhenNameIsEmpty))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData(" ")]
  public void ThrowWhenNameIsEmpty(string? name)
  {
    Action action = () =>
    {
      _ = new DomainEntity.Category(name!, "Category Description");
    };
    EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
    Assert.Equal("Name should not be empty or null", exception.Message);
  }
}
