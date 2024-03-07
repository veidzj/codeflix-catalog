using Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using Codeflix.Catalog.Domain.Entity;
using System.Collections.Generic;

namespace Codeflix.Catalog.UnitTests.Application.UpdateCategory;
public class UpdateCategoryTestDataGenerator
{
  public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
  {
    UpdateCategoryTestFixture fixture = new();
    for (int index = 0; index < times; index++)
    {
      Category category = fixture.GetCategory();
      UpdateCategoryInput input = new(
        category.Id,
        fixture.GetValidCategoryName(),
        fixture.GetValidCategoryDescription(),
        fixture.GetRandomBoolean()
      );
      yield return new object[]
      {
        category,
        input
      };
    }
  }
}
