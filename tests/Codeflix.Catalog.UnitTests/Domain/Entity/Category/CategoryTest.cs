using Codeflix.Catalog.Domain.Exceptions;
using System;
using System.Linq;
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

  [Fact(DisplayName = nameof(ThrowWhenDescriptionIsNull))]
  [Trait("Domain", "Category - Aggregates")]
  public void ThrowWhenDescriptionIsNull()
  {
    Action action = () =>
    {
      _ = new DomainEntity.Category("Category Name", null!);
    };
    EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
    Assert.Equal("Description should not be null", exception.Message);
  }

  [Theory(DisplayName = nameof(ThrowWhenNameIsLessThan3Characters))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("1")]
  [InlineData("12")]
  [InlineData("a")]
  [InlineData("ab")]
  public void ThrowWhenNameIsLessThan3Characters(string invalidName)
  {
    Action action = () =>
    {
      _ = new DomainEntity.Category(invalidName, "Category Description");
    };
    EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
    Assert.Equal("Name should be at least 3 characters long", exception.Message);
  }

  [Fact(DisplayName = nameof(ThrowWhenNameIsGreaterThan255Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void ThrowWhenNameIsGreaterThan255Characters()
  {
    string invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
    Action action = () =>
    {
      _ = new DomainEntity.Category(invalidName, "Category Description");
    };
    EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
    Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
  }

  [Fact(DisplayName = nameof(ThrowWhenDescriptionIsGreaterThan10_000Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void ThrowWhenDescriptionIsGreaterThan10_000Characters()
  {
    string invalidDescription = string.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
    Action action = () =>
    {
      _ = new DomainEntity.Category("Category Name", invalidDescription);
    };
    EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
    Assert.Equal("Description should be less or equal 10.000 characters long", exception.Message);
  }

  [Fact(DisplayName = nameof(Activate))]
  [Trait("Domain", "Category - Aggregates")]
  public void Activate()
  {
    var validData = new
    {
      Name = "category name",
      Description = "category description"
    };

    DomainEntity.Category category = new(validData.Name, validData.Description, false);
    category.Activate();

    Assert.True(category.IsActive);
  }

  [Fact(DisplayName = nameof(Deactivate))]
  [Trait("Domain", "Category - Aggregates")]
  public void Deactivate()
  {
    var validData = new
    {
      Name = "category name",
      Description = "category description"
    };

    DomainEntity.Category category = new(validData.Name, validData.Description, true);
    category.Deactivate();

    Assert.False(category.IsActive);
  }

  [Fact(DisplayName = nameof(Update))]
  [Trait("Domain", "Category - Aggregates")]
  public void Update()
  {
    DomainEntity.Category category = new("Category Name", "Category Description");
    var newValues = new
    {
      Name = "New Name",
      Description = "New Description"
    };

    category.Update(newValues.Name, newValues.Description);

    Assert.Equal(newValues.Name, category.Name);
    Assert.Equal(newValues.Description, category.Description);
  }

  [Fact(DisplayName = nameof(UpdateOnlyName))]
  [Trait("Domain", "Category - Aggregates")]
  public void UpdateOnlyName()
  {
    DomainEntity.Category category = new("Category Name", "Category Description");
    string currentDescription = category.Description;
    var newValues = new
    {
      Name = "New Name"
    };

    category.Update(newValues.Name);

    Assert.Equal(newValues.Name, category.Name);
    Assert.Equal(currentDescription, category.Description);
  }

  [Theory(DisplayName = nameof(UpdateThrowWhenNameIsEmpty))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData(" ")]
  public void UpdateThrowWhenNameIsEmpty(string? name)
  {
    DomainEntity.Category category = new("Category Name", "Category Description");
    Action action = () => category.Update(name!);
    EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
    Assert.Equal("Name should not be empty or null", exception.Message);
  }

  [Theory(DisplayName = nameof(UpdateThrowWhenNameIsLessThan3Characters))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("1")]
  [InlineData("12")]
  [InlineData("a")]
  [InlineData("ab")]
  public void UpdateThrowWhenNameIsLessThan3Characters(string invalidName)
  {
    DomainEntity.Category category = new("Category Name", "Category Description");
    Action action = () => category.Update(invalidName);
    EntityValidationException exception = Assert.Throws<EntityValidationException>(action);
    Assert.Equal("Name should be at least 3 characters long", exception.Message);
  }
}
