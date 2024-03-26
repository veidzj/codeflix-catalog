using Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using Codeflix.Catalog.UnitTests.Application.Category.Common;
using System;
using System.Collections.Generic;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

public class ListCategoriesTestFixture : CategoryUseCasesBaseFixture
{
  public List<DomainEntity.Category> GetCategoriesList(int length = 10)
  {
    List<DomainEntity.Category> list = [];
    for (int i = 0; i < length; i++)
    {
      list.Add(this.GetCategory());
    }
    return list;
  }

  public ListCategoriesInput GetListCategoriesInput()
  {
    Random random = new();
    return new(
      page: random.Next(1, 10),
      perPage: random.Next(15, 100),
      search: this.Faker.Commerce.ProductName(),
      sort: this.Faker.Commerce.ProductName(),
      dir: random.Next(0, 10) > 5 ? SearchOrder.Asc : SearchOrder.Desc
    );
  }
}

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture> { }
