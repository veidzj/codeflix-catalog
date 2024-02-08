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
  public string MakeValidCategoryName()
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

  public string MakeValidCategoryDescription()
  {
    string categoryDescription = this.Faker.Commerce.ProductDescription();

    if (categoryDescription.Length > 10_000)
    {
      categoryDescription = categoryDescription[..10_000];
    }

    return categoryDescription;
  }

  public bool MakeRandomBoolean()
  {
    return new Random().NextDouble() < 0.5;
  }

  public CreateCategoryInput MakeInput()
  {
    return new(this.MakeValidCategoryName(), this.MakeValidCategoryDescription(), this.MakeRandomBoolean());
  }

  public Mock<ICategoryRepository> MakeRepositoryMock()
  {
    return new();
  }

  public Mock<IUnitOfWork> MakeUnitOfWorkMock()
  {
    return new();
  }
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }

