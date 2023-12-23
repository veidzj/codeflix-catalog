using Codeflix.Catalog.UnitTests.Common;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;
public class CategoryTestFixture : BaseFixture
{
  public CategoryTestFixture() : base() { }

  public DomainEntity.Category MakeValidCategory()
  {
    return new(this.MakeValidCategoryName(), this.Faker.Commerce.ProductDescription());
  }

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
    string categoryName = "";
    while (categoryName.Length < 3)
    {
      categoryName = this.Faker.Commerce.Categories(1)[0];
    }

    return categoryName;
  }
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }
