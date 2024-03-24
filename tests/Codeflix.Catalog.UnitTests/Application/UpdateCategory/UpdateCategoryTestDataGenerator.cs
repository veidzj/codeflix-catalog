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
      UpdateCategoryInput input = fixture.GetValidInput(category.Id);
      yield return new object[]
      {
        category,
        input
      };
    }
  }
}
