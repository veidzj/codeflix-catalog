using Codeflix.Catalog.UnitTests.Common;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;
public class CategoryTestFixture : BaseFixture
{
  public CategoryTestFixture() : base() { }

  public DomainEntity.Category MakeValidCategory()
  {
    return new(this.MakeValidCategoryName(), this.MakeValidCategoryDescription());
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
    string categoryDescription = this.Faker.Commerce.ProductDescription();

    if (categoryDescription.Length > 10_000)
    {
      categoryDescription = categoryDescription[..10_000];
    }

    return categoryDescription;
  }
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }
