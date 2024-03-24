using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using System;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.UpdateCategory;
public class UpdateCategoryTestFixture : BaseFixture
{
  public string GetValidCategoryName()
  {
    string categoryName = "";

    while (categoryName.Length < 3)
    {
      categoryName = this.Faker.Commerce.Categories(1)[0];
    }

    if (categoryName.Length > 255)
    {
      categoryName = categoryName[..255];
    }

    return categoryName;
  }

  public string GetValidCategoryDescription()
  {
    string categoryDescription = this.Faker.Commerce.ProductDescription();

    if (categoryDescription.Length > 10_000)
    {
      categoryDescription = categoryDescription[..10_000];
    }

    return categoryDescription;
  }

  public bool GetRandomBoolean()
  {
    return new Random().NextDouble() < 0.5;
  }

  public Category GetCategory()
  {
    return new(this.GetValidCategoryName(), this.GetValidCategoryDescription(), this.GetRandomBoolean());
  }

  public Mock<ICategoryRepository> GetRepositoryMock()
  {
    return new();
  }

  public Mock<IUnitOfWork> GetUnitOfWorkMock()
  {
    return new();
  }

  public UpdateCategoryInput GetValidInput(Guid? id = null)
  {
    return new(
      id ?? Guid.NewGuid(),
      this.GetValidCategoryName(),
      this.GetValidCategoryDescription(),
      this.GetRandomBoolean()
    );
  }

  public UpdateCategoryInput GetInvalidInputShortName()
  {
    UpdateCategoryInput invalidInputShortName = this.GetValidInput();
    invalidInputShortName.Name = invalidInputShortName.Name[..2];
    return invalidInputShortName;
  }

  public UpdateCategoryInput GetInvalidInputLongName()
  {
    UpdateCategoryInput invalidInputLongName = this.GetValidInput();
    string longName = this.Faker.Commerce.ProductName();
    while (longName.Length <= 255)
    {
      longName = $"{longName} {this.Faker.Commerce.ProductName()}";
    }
    invalidInputLongName.Name = longName;
    return invalidInputLongName;
  }

  public UpdateCategoryInput GetInvalidInputLongDescription()
  {
    UpdateCategoryInput invalidInputLongDescription = this.GetValidInput();
    string longDescription = this.Faker.Commerce.ProductDescription();
    while (longDescription.Length <= 10_000)
    {
      longDescription = $"{longDescription} {this.Faker.Commerce.ProductDescription()}";
    }
    invalidInputLongDescription.Description = longDescription;
    return invalidInputLongDescription;
  }
}

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture> { }
