﻿using Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
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

    category.Should().NotBeNull();
    category.Name.Should().Be(validData.Name);
    category.Description.Should().Be(validData.Description);
    category.Id.Should().NotBeEmpty();
    category.CreatedAt.Should().NotBeSameDateAs(default);
    (category.CreatedAt < dateTimeAfter).Should().BeTrue();
    (category.CreatedAt > dateTimeBefore).Should().BeTrue();
    category.IsActive.Should().BeTrue();
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

    category.Should().NotBeNull();
    category.Name.Should().Be(validData.Name);
    category.Description.Should().Be(validData.Description);
    category.Id.Should().NotBeEmpty();
    category.CreatedAt.Should().NotBeSameDateAs(default);
    (category.CreatedAt < dateTimeAfter).Should().BeTrue();
    (category.CreatedAt > dateTimeBefore).Should().BeTrue();
    category.IsActive.Should().Be(isActive);
  }

  [Theory(DisplayName = nameof(InstantiateThrowWhenNameIsEmpty))]
  [Trait("Domain", "Category - Aggregates")]
  [InlineData("")]
  [InlineData(null)]
  [InlineData(" ")]
  public void InstantiateThrowWhenNameIsEmpty(string? name)
  {
    Action action = () =>
    {
      _ = new DomainEntity.Category(name!, "Category Description");
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Name should not be empty or null");
  }

  [Fact(DisplayName = nameof(InstantiateThrowWhenDescriptionIsNull))]
  [Trait("Domain", "Category - Aggregates")]
  public void InstantiateThrowWhenDescriptionIsNull()
  {
    Action action = () =>
    {
      _ = new DomainEntity.Category("Category Name", null!);
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
    Action action = () =>
    {
      _ = new DomainEntity.Category(invalidName, "Category Description");
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Name should be at least 3 characters long");
  }

  [Fact(DisplayName = nameof(InstantiateThrowWhenNameIsGreaterThan255Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void InstantiateThrowWhenNameIsGreaterThan255Characters()
  {
    string invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
    Action action = () =>
    {
      _ = new DomainEntity.Category(invalidName, "Category Description");
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters long");
  }

  [Fact(DisplayName = nameof(InstantiateThrowWhenDescriptionIsGreaterThan10_000Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void InstantiateThrowWhenDescriptionIsGreaterThan10_000Characters()
  {
    string invalidDescription = string.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());
    Action action = () =>
    {
      _ = new DomainEntity.Category("Category Name", invalidDescription);
    };

    action.Should().Throw<EntityValidationException>().WithMessage("Description should be less or equal 10.000 characters long");
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

    category.IsActive.Should().BeTrue();
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

    category.IsActive.Should().BeFalse();
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

    category.Name.Should().Be(newValues.Name);
    category.Description.Should().Be(newValues.Description);
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

    category.Name.Should().Be(newValues.Name);
    category.Description.Should().Be(currentDescription);
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
    DomainEntity.Category category = new("Category Name", "Category Description");
    Action action = () => category.Update(invalidName);

    action.Should().Throw<EntityValidationException>().WithMessage("Name should be at least 3 characters long");
  }

  [Fact(DisplayName = nameof(UpdateThrowWhenNameIsGreaterThan255Characters))]
  [Trait("Domain", "Category - Aggregates")]
  public void UpdateThrowWhenNameIsGreaterThan255Characters()
  {
    DomainEntity.Category category = new("Category Name", "Category Description");
    string invalidName = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
    Action action = () => category.Update(invalidName);

    action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters long");
  }
}
