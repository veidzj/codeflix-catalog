using Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.ListCategories;

public class ListCategoriesTestFixture : BaseFixture
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

  public List<Category> GetCategoriesList(int length = 10)
  {
    List<Category> list = [];
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

  public Mock<ICategoryRepository> GetRepositoryMock()
  {
    return new();
  }
}

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture> { }
