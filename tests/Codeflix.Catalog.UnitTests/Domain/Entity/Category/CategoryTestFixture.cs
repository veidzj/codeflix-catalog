using Codeflix.Catalog.UnitTests.Common;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;
public class CategoryTestFixture : BaseFixture
{
  public CategoryTestFixture() : base() { }

  public DomainEntity.Category MakeValidCategory()
  {
    return new(this.Faker.Commerce.Categories(1)[0], this.Faker.Commerce.ProductDescription());
  }
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }
