using Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
  private readonly CategoryTestFixture categoryTestFixture = new();

  [Fact(DisplayName = nameof(InstantiateWithDefaultValues))]
  [Trait("Domain", "Category - Aggregates")]
  public void InstantiateWithDefaultValues()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    DateTime dateTimeBefore = DateTime.Now;
    DomainEntity.Category category = new(validCategory.Name, validCategory.Description);
    DateTime dateTimeAfter = DateTime.Now.AddSeconds(1);

    category.Should().NotBeNull();
    category.Name.Should().Be(validCategory.Name);
    category.Description.Should().Be(validCategory.Description);
    category.Id.Should().NotBeEmpty();
    category.CreatedAt.Should().NotBeSameDateAs(default);
    (category.CreatedAt <= dateTimeAfter).Should().BeTrue();
    (category.CreatedAt >= dateTimeBefore).Should().BeTrue();
    category.IsActive.Should().BeTrue();
  }

  [Theory(DisplayName = nameof(InstantiateWithIsActive))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData(true)]
  [InlineData(false)]
  public void InstantiateWithIsActive(bool isActive)
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    DateTime dateTimeBefore = DateTime.Now;
    DomainEntity.Category category = new(validCategory.Name, validCategory.Description, isActive);
    DateTime dateTimeAfter = DateTime.Now.AddSeconds(1);

    category.Should().NotBeNull();
    category.Name.Should().Be(validCategory.Name);
    category.Description.Should().Be(validCategory.Description);
    category.Id.Should().NotBeEmpty();
    category.CreatedAt.Should().NotBeSameDateAs(default);
    (category.CreatedAt <= dateTimeAfter).Should().BeTrue();
    (category.CreatedAt >= dateTimeBefore).Should().BeTrue();
    category.IsActive.Should().Be(isActive);
  }

  [Theory(DisplayName = nameof(InstantiateThrowWhenNameIsEmpty))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData(" ")]
  public void InstantiateThrowWhenNameIsEmpty(string? name)
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();

    Action action = () =>
    {
      _ = new DomainEntity.Category(name!, validCategory.Description);
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Name should not be empty or null");
  }

  [Fact(DisplayName = nameof(InstantiateThrowWhenDescriptionIsNull))]
  [Trait("Domain", "Category - Aggregates")]
  public void InstantiateThrowWhenDescriptionIsNull()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();

    Action action = () =>
    {
      _ = new DomainEntity.Category(validCategory.Name, null!);
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Description should not be null");
  }

  [Theory(DisplayName = nameof(InstantiateThrowWhenNameIsLessThan3Characters))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("1")]
  [InlineData("12")]
  [InlineData("a")]
  [InlineData("ab")]
  public void InstantiateThrowWhenNameIsLessThan3Characters(string invalidName)
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();

    Action action = () =>
    {
      _ = new DomainEntity.Category(invalidName, validCategory.Description);
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Name should be at least 3 characters long");
  }

  [Fact(DisplayName = nameof(InstantiateThrowWhenNameIsGreaterThan255Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void InstantiateThrowWhenNameIsGreaterThan255Characters()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    string invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

    Action action = () =>
    {
      _ = new DomainEntity.Category(invalidName, validCategory.Description);
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters long");
  }

  [Fact(DisplayName = nameof(InstantiateThrowWhenDescriptionIsGreaterThan10_000Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void InstantiateThrowWhenDescriptionIsGreaterThan10_000Characters()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    string invalidDescription = string.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());

    Action action = () =>
    {
      _ = new DomainEntity.Category(validCategory.Name, invalidDescription);
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Description should be less or equal 10.000 characters long");
  }

  [Fact(DisplayName = nameof(Activate))]
  [Trait("Domain", "Category - Aggregates")]
  public void Activate()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    DomainEntity.Category category = new(validCategory.Name, validCategory.Description, false);

    category.Activate();

    category.IsActive.Should().BeTrue();
  }

  [Fact(DisplayName = nameof(Deactivate))]
  [Trait("Domain", "Category - Aggregates")]
  public void Deactivate()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    DomainEntity.Category category = new(validCategory.Name, validCategory.Description, true);

    category.Deactivate();

    category.IsActive.Should().BeFalse();
  }

  [Fact(DisplayName = nameof(Update))]
  [Trait("Domain", "Category - Aggregates")]
  public void Update()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    DomainEntity.Category categoryWithNewValues = this.categoryTestFixture.MakeValidCategory();

    validCategory.Update(categoryWithNewValues.Name, categoryWithNewValues.Description);

    validCategory.Name.Should().Be(categoryWithNewValues.Name);
    validCategory.Description.Should().Be(categoryWithNewValues.Description);
  }

  [Fact(DisplayName = nameof(UpdateOnlyName))]
  [Trait("Domain", "Category - Aggregates")]
  public void UpdateOnlyName()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    string newName = this.categoryTestFixture.MakeValidCategoryName();
    string currentDescription = validCategory.Description;

    validCategory.Update(newName);

    validCategory.Name.Should().Be(newName);
    validCategory.Description.Should().Be(currentDescription);
  }

  [Theory(DisplayName = nameof(UpdateThrowWhenNameIsEmpty))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData(" ")]
  public void UpdateThrowWhenNameIsEmpty(string? name)
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();

    Action action = () => validCategory.Update(name!);

    action.Should().Throw<EntityValidationException>().WithMessage("Name should not be empty or null");
  }

  [Theory(DisplayName = nameof(UpdateThrowWhenNameIsLessThan3Characters))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("1")]
  [InlineData("12")]
  [InlineData("a")]
  [InlineData("ab")]
  public void UpdateThrowWhenNameIsLessThan3Characters(string invalidName)
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();

    Action action = () => validCategory.Update(invalidName);

    action.Should().Throw<EntityValidationException>().WithMessage("Name should be at least 3 characters long");
  }

  [Fact(DisplayName = nameof(UpdateThrowWhenNameIsGreaterThan255Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void UpdateThrowWhenNameIsGreaterThan255Characters()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    string invalidName = this.categoryTestFixture.Faker.Lorem.Letter(256);

    Action action = () => validCategory.Update(invalidName);

    action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters long");
  }

  [Fact(DisplayName = nameof(UpdateThrowWhenDescriptionIsGreaterThan10_000Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void UpdateThrowWhenDescriptionIsGreaterThan10_000Characters()
  {
    DomainEntity.Category validCategory = this.categoryTestFixture.MakeValidCategory();
    string invalidDescription = this.categoryTestFixture.Faker.Commerce.ProductDescription();
    while (invalidDescription.Length <= 10_000)
    {
      invalidDescription = $"{invalidDescription} {this.categoryTestFixture.Faker.Commerce.ProductDescription()}";
    }

    Action action = () => validCategory.Update("New Name", invalidDescription);

    action.Should().Throw<EntityValidationException>().WithMessage("Description should be less or equal 10.000 characters long");
  }
}
