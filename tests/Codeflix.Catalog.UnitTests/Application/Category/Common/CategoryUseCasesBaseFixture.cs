using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using System;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Application.Category.Common;
public abstract class CategoryUseCasesBaseFixture : BaseFixture
{
  public Mock<ICategoryRepository> GetRepositoryMock()
  {
    return new();
  }

  public Mock<IUnitOfWork> GetUnitOfWorkMock()
  {
    return new();
  }

  public string GetCategoryName()
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

  public string GetCategoryDescription()
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

  public DomainEntity.Category GetCategory()
  {
    return new(this.GetCategoryName(), this.GetCategoryDescription(), this.GetRandomBoolean());
  }
}
