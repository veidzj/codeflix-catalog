using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using System;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTestFixture : BaseFixture
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

  public CreateCategoryInput GetInput()
  {
    return new(this.GetValidCategoryName(), this.GetValidCategoryDescription(), this.GetRandomBoolean());
  }

  public CreateCategoryInput GetInvalidInputShortName()
  {
    CreateCategoryInput invalidInputShortName = this.GetInput();
    invalidInputShortName.Name = invalidInputShortName.Name[..2];
    return invalidInputShortName;
  }

  public CreateCategoryInput GetInvalidInputLongName()
  {
    CreateCategoryInput invalidInputLongName = this.GetInput();
    string longName = this.Faker.Commerce.ProductName();
    while (longName.Length <= 255)
    {
      longName = $"{longName} {this.Faker.Commerce.ProductName()}";
    }
    invalidInputLongName.Name = longName;
    return invalidInputLongName;
  }

  public CreateCategoryInput GetInvalidInputNullDescription()
  {
    CreateCategoryInput invalidInputNullDescription = this.GetInput();
    invalidInputNullDescription.Description = null!;
    return invalidInputNullDescription;
  }

  public CreateCategoryInput GetInvalidInputLongDescription()
  {
    CreateCategoryInput invalidInputLongDescription = this.GetInput();
    string longDescription = this.Faker.Commerce.ProductDescription();
    while (longDescription.Length <= 10_000)
    {
      longDescription = $"{longDescription} {this.Faker.Commerce.ProductDescription()}";
    }
    invalidInputLongDescription.Description = longDescription;
    return invalidInputLongDescription;
  }

  public Mock<ICategoryRepository> GetRepositoryMock()
  {
    return new();
  }

  public Mock<IUnitOfWork> GetUnitOfWorkMock()
  {
    return new();
  }
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }

