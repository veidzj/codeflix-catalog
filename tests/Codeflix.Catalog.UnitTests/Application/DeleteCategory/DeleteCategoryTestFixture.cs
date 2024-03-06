using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.DeleteCategory;
public class DeleteCategoryTestFixture : BaseFixture
{
  public Category GetValidCategory()
  {
    return new(this.GetValidCategoryName(), this.GetValidCategoryDescription());
  }

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

  public Mock<ICategoryRepository> GetRepositoryMock()
  {
    return new();
  }

  public Mock<IUnitOfWork> GetUnitOfWorkMock()
  {
    return new();
  }
}

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture> { }
